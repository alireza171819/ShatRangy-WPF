using Business;
using ShatRangyy.CustomControls;
using System;
using System.Windows.Input;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using VeiwModels;

namespace ShatRangyy
{
    public partial class ItemsManagement : UserControl
    {
        public ItemsManagement()
        {
            InitializeComponent();
        }

        #region Varibels And Objects

        PersianCalendar PersianCalendar = new PersianCalendar();
        Item_BL Item_BL = new Item_BL();
        public enum FilterType
        {
           Id, ItemName, Description, SellPrice, Cost, Number, Date, Existing, NameOrDescription
        }
        FilterType _FilterType;
        string ItemName, Description, CurrentDate, ContentSerchBox;
        decimal SellPrice, ProductionCost, FromAmount, ToAmount;
        int Id, Number, SearchYear, SearchMonth, SearchDay, Year, Month, Day, FromNumber, ToNumber;
        bool _Update = false;
        bool AutoSaveDoc = false;
        /// <summary>
        /// regex that matches disallowed text
        /// </summary>
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        #endregion

        #region Functions

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        public bool ParametersValidation()
        {
            if (String.IsNullOrEmpty(txtItemName.Text))
            {
                _ShowMessage("لطفا نام کالا را وارد کنید .", MessageBox_.enumType.Warning);
                txtItemName.Focus();
                return false;
            }
            if (txtItemName.Text.Length < 2)
            {
                _ShowMessage("نام کالا باید بیشتر از 1 حرف باشد .", MessageBox_.enumType.Warning);
                txtItemName.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtSellPrice.Text) && checkboxAutoSaveDoc.IsChecked == true)
            {
                _ShowMessage("لطفا قیمت را وارد کنید .", MessageBox_.enumType.Warning);
                txtSellPrice.Focus();
                return false;
            }
            if (txtSellPrice.Text.Length < 4 && checkboxAutoSaveDoc.IsChecked == true)
            {
                _ShowMessage("مقدار قیمت باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
                txtSellPrice.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtProductionCost.Text))
            {
                _ShowMessage("لطفا هزینه را وارد کنید .", MessageBox_.enumType.Warning);
                txtProductionCost.Focus();
                return false;
            }
            if (txtProductionCost.Text.Length < 4)
            {
                _ShowMessage("مقدار هزینه باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
                txtProductionCost.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtNumber.Text))
            {
                _ShowMessage("لطفا تعداد کالا را وارد کنید .", MessageBox_.enumType.Warning);
                txtNumber.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtShowYear.Text)
                || String.IsNullOrEmpty(txtShowMonth.Text)
                || String.IsNullOrEmpty(txtShowDay.Text))
            {
                _ShowMessage("لطفا تاریخ را وارد کنید .", MessageBox_.enumType.Warning);
                txtShowYear.Focus();
                return false;
            }

            if (Item_BL.ExistItem(ItemName) && _Update != true)
            {
                _ShowMessage("این نام کالا قبلا گرفته شده ، نام دیگری انتخاب کنید .", MessageBox_.enumType.Warning);
                txtItemName.Focus();
                return false;
            }

            return true;
        }
        public void GetParameters()
        {
            ItemName = txtItemName.Text;
            Description = txtItemDescription.Text;
            ContentSerchBox = txtSearch.Text;
            if (!String.IsNullOrEmpty(txtSellPrice.Text))
            {
                SellPrice = decimal.Parse(txtSellPrice.Text.Replace(",", ""));
            }
            if (!String.IsNullOrEmpty(txtProductionCost.Text))
            {
                ProductionCost = decimal.Parse(txtProductionCost.Text.Replace(",", ""));
            }
            if (!String.IsNullOrEmpty(txtNumber.Text))
            {
                Number = int.Parse(txtNumber.Text);
            } 
            if (!String.IsNullOrEmpty(txtSearchYear.Text))
            {
                SearchYear = int.Parse(txtSearchYear.Text);
            }
            else
            {
                SearchYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchMonth.Text))
            {
                SearchMonth = int.Parse(txtSearchMonth.Text);
            }
            else
            {
                SearchMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchDay.Text))
            {
                SearchDay = int.Parse(txtSearchDay.Text);
            }
            else
            {
                SearchDay = 0;
            }
            if (!String.IsNullOrEmpty(txtShowYear.Text))
            {
                Year = int.Parse(txtShowYear.Text);
            }
            if (!String.IsNullOrEmpty(txtShowMonth.Text))
            {
                Month = int.Parse(txtShowMonth.Text);
            }
            if (!String.IsNullOrEmpty(txtShowDay.Text))
            {
                Day = int.Parse(txtShowDay.Text);
            }
            if (!String.IsNullOrEmpty(txtSearchFromAmount.Text) && _FilterType != FilterType.Number)
            {
                FromAmount = decimal.Parse(txtSearchFromAmount.Text.Replace(",", ""));
            }
            else
            {
                FromAmount = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToAmount.Text) && _FilterType != FilterType.Number)
            {
                ToAmount = decimal.Parse(txtSearchToAmount.Text.Replace(",", ""));
            }
            else
            {
                ToAmount = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToAmount.Text) && _FilterType == FilterType.Number)
            {
                FromNumber = int.Parse(txtSearchFromAmount.Text);
            }
            else
            {
                FromNumber = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToAmount.Text) && _FilterType == FilterType.Number)
            {
                ToNumber = int.Parse(txtSearchToAmount.Text);
            }
            else
            {
                ToNumber = 0;
            }
            if (checkboxAutoSaveDoc.IsChecked == true)
            {
                AutoSaveDoc = true;
            }
            else
            {
                AutoSaveDoc = false;
            }
            CurrentDate = $"{Year}/{Month}/{Day}";
        }
        public void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        public void Clear()
        {
            txtItemName.Text = String.Empty;
            txtItemDescription.Text = String.Empty;
            txtNumber.Text = String.Empty;
            txtSearch.Text = String.Empty;
            txtSellPrice.Text = String.Empty;
            txtProductionCost.Text = String.Empty;
            _Update = false;
            FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            ClearDataGrid();
            txtItemName.Focus();
        }
        public void ClearDataGrid()
        {
            if (DGV.Items.Count > 1)
            {
                DGV.SelectedIndex = DGV.Items.Count - 1;
            }
            else if (DGV.Items.Count == 1)
            {
                DGV.SelectedIndex = 0;
            }
        }
        public void Insert()
        {
            GetParameters();
            if (ParametersValidation())
            {
                Item item = new Item();
                #region Get Data
                item.ItemName = ItemName;
                item.Description = Description;
                item.SellPrice = SellPrice;
                item.ProductionCost = ProductionCost;
                item.Number = Number;
                item.Date = CurrentDate;
                item.Year = Year;
                item.Month = Month;
                item.Day = Day;
                #endregion
                if (_Update)
                {
                    //Update
                    item.ID = Id;
                    if (Item_BL.UpdateItem(item))
                    {
                        _ShowMessage("کالا با موفقیت ویرایش شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("کالا ویرایش نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtItemName.Focus();
                    }
                }
                else
                {
                    //Insert
                    if (Item_BL.InsertItem(item, AutoSaveDoc))
                    {
                        _ShowMessage("کالا با موفقیت ذخیره شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("کالا ذخیره نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtItemName.Focus();
                    }
                }
            }
        }
        public void Update()
        {
            Item item = new Item();
            item = Item_BL.GetItemById(Id);
            txtItemName.Text = item.ItemName;
            txtItemDescription.Text = item.Description;
            txtNumber.Text = item.Number.ToString();
            txtSellPrice.Text = ThreeDigitSeparator(item.SellPrice.ToString());
            txtProductionCost.Text = ThreeDigitSeparator(item.ProductionCost.ToString());
            txtShowYear.Text = item.Year.ToString();
            txtShowMonth.Text = item.Month.ToString();
            txtShowDay.Text = item.Day.ToString();
            _Update = true;
            txtItemName.Focus();
        }
        public void Delete()
        {
            QuestionBox_ questionBox_ = new QuestionBox_();
            Item item;
            item = Item_BL.GetItemById(Id);
            questionBox_.Content = "آیا می خواهید این کالا را حذف کنید .";
            questionBox_.ShowDialog();
            if (questionBox_.Ok)
            {
                if (Item_BL.DeleteItem(item))
                {
                    _ShowMessage("کالا با موفقیت حذف شد .", MessageBox_.enumType.Success);
                }
                else
                {
                    _ShowMessage("حذف کالا با خطا مواجه شد ،" + "\n" +
                    "لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                }
            }
        }
        public void Search()
        {
            FilterBox filterBox = new FilterBox();
            filterBox.LabelContent1 = "سریال";
            filterBox.LabelContent2 = "نام کالا";
            filterBox.LabelContent3 = "قیمت فروش";
            filterBox.LabelContent4 = "هزینه خرید";
            filterBox.LabelContent5 = "شرح کالا";
            filterBox.LabelContent6 = "تعداد";
            filterBox.LabelContent7 = "تاریخ";
            filterBox.LabelContent8 = "....";
            filterBox.LabelContent9 = "....";
            if (_FilterType == FilterType.Number)
            {
                filterBox.CheckBox6.IsChecked = true;
            }
            filterBox.ShowDialog();
            switch (filterBox.selectedIndex)
            {
                case FilterBox.SelectedIndex.Index1:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس سریال");
                    _FilterType = FilterType.Id;
                    break;
                case FilterBox.SelectedIndex.Index2:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام کالا");
                   _FilterType = FilterType.ItemName;
                    break;
                case FilterBox.SelectedIndex.Index3:
                    txtSearchFromAmount.Visibility = System.Windows.Visibility.Visible;
                    txtSearchToAmount.Visibility = System.Windows.Visibility.Visible;
                    txtSearch.Visibility = System.Windows.Visibility.Hidden;
                   _FilterType = FilterType.SellPrice;
                    break;
                case FilterBox.SelectedIndex.Index4:
                    txtSearchFromAmount.Visibility = System.Windows.Visibility.Visible;
                    txtSearchToAmount.Visibility = System.Windows.Visibility.Visible;
                    txtSearch.Visibility = System.Windows.Visibility.Hidden;
                   _FilterType = FilterType.Cost;
                    break;
                case FilterBox.SelectedIndex.Index5:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس شرح کالا");
                    _FilterType = FilterType.Description;
                    break;
                case FilterBox.SelectedIndex.Index6:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس تعداد کالا");
                    _FilterType = FilterType.Description;
                    break;
                case FilterBox.SelectedIndex.Index7:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو");
                    _FilterType = FilterType.Date;
                    break;
                default:
                    break;
            }
        }
        public void FilterDataGrid(int year, int month, int day)
        {
            DGV.ItemsSource = Item_BL.GetItemsByDate(year, year, month, month, day, day);
            ClearDataGrid();
        }
        public void FilterDataGrid(FilterType filterType, int id, string description, string itemName)
        {
            var _list = Item_BL.GetItemsByDate(SearchYear, SearchYear, SearchMonth, SearchMonth,
                SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Id:
                    DGV.ItemsSource = null;
                    DGV.Items.Add(Item_BL.GetItemById(id));
                    break;
                case FilterType.ItemName:
                    DGV.ItemsSource = Item_BL.GetItemsByName(_list, itemName);
                    break;
                case FilterType.Description:
                    DGV.ItemsSource = Item_BL.GetItemsByDescription(_list, description);
                    break;
                case FilterType.Date:
                    DGV.ItemsSource = Item_BL.GetItemsByNameOrDescription(itemName);
                    break;
            }
            ClearDataGrid();
        }
        public void FilterDataGrid(FilterType filterType, decimal fromAmount, decimal toAmount
            , int fromNumber, int toNumber)
        {
            GetParameters();
            var _list = Item_BL.GetItemsByDate(SearchYear, SearchYear, SearchMonth, SearchMonth,
                SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.SellPrice:
                    DGV.ItemsSource = Item_BL.GetItemsBySellPrice(_list, fromAmount, toAmount);
                    break;
                case FilterType.Cost:
                    DGV.ItemsSource = Item_BL.GetItemsByCost(_list, fromAmount, toAmount);
                    break;
                case FilterType.Number:
                    DGV.ItemsSource = Item_BL.GetItemsByNumber(_list, fromNumber, toNumber);
                    break;
                default:
                    DGV.ItemsSource = null;
                    break;
            }
            ClearDataGrid();
        }
        public void FilterDataGrid()
        {
            DGV.ItemsSource = Item_BL.GetItems();
        }
        public void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }
        public string ThreeDigitSeparator(string input)
        {
            if (input != String.Empty)
            {
                try
                {
                    return decimal.Parse(input.Replace(",", "")).ToString("#,#");
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        #endregion

        #region Events

        #region ---KeyDown---

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtItemDescription.Focus();
            }
        }

        private void txtItemDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtNumber.Focus();
            }
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtProductionCost.Focus();
            }
        }

        private void txtProductionCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtSellPrice.Focus();
            }
        }

        private void txtSellPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSave.Focus();
            }
        }

        private void txtSearchDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            }
        }

        private void txtSearchMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            }
        }

        private void txtSearchYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
             
        }

        private void txtSearchToAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (FromAmount > 0 && ToAmount > 3)
            {
                GetParameters();
                if (e.Key == Key.Enter)
                {
                    switch (_FilterType)
                    {
                        case FilterType.SellPrice:
                            FilterDataGrid(FilterType.SellPrice, FromAmount, ToAmount, 0, 0);
                            break;
                        case FilterType.Cost:
                            FilterDataGrid(FilterType.Cost, FromAmount, ToAmount, 0, 0);
                            break;
                        case FilterType.Number:
                            FilterDataGrid(FilterType.Number, 0, 0, FromNumber, ToNumber);
                            break;
                        case FilterType.Date:
                            break;
                    }
                }
            }
        }

        private void txtSearchFromAmount_KeyDown(object sender, KeyEventArgs e)
        {
            txtSearchToAmount.Focus();
        }

        #endregion

        #region ---Click--- 

        private void btnExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void checkboxAutoSaveDoc_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Insert();
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Delete();
        }

        private void btnEdite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Update();
        }

        #endregion

        #region ---Data Grid---

        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV.SelectedItem != null)
            {
                Item obj = DGV.SelectedItem as Item;
                Id = obj.ID;
            }
        }

        #endregion

        #region ---User Control---

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            txtSearchYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtSearchDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            txtShowYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtShowMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtShowDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            _FilterType = FilterType.Date;
            Clear();
            FilterDataGrid();
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    Insert();
                    break;
                case Key.F2:
                    Update();
                    break;
                case Key.F3:
                    Delete();
                    break;
                case Key.F5:
                    Search();
                    break;
                case Key.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ---Preview Text Input---

        private void AllTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #region ---TextChanged---

        private void txtSellPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSellPrice.Text = ThreeDigitSeparator(txtSellPrice.Text);
            txtSellPrice.SelectionStart = txtSellPrice.Text.Length;
        }

        private void txtProductionCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtProductionCost.Text = ThreeDigitSeparator(txtProductionCost.Text);
            txtProductionCost.SelectionStart = txtProductionCost.Text.Length;
        }

        private void txtSearchDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchDay.Text == null)
            {
                SearchDay = 0;
            }
        }

        private void txtSearchMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchMonth.Text == null)
            {
                SearchMonth = 0;
            }
        }

        private void txtSearchYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchYear.Text == null)
            {
                SearchYear = 0;
            }
        }

        private void txtSearchFromAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_FilterType != FilterType.Number)
            {
                txtSearchFromAmount.Text = ThreeDigitSeparator(txtSearchFromAmount.Text);
                txtSearchFromAmount.SelectionStart = txtSearchFromAmount.Text.Length;
            }
        }

        private void txtSearchToAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_FilterType != FilterType.Number)
            {
                txtSearchToAmount.Text = ThreeDigitSeparator(txtSearchToAmount.Text);
                txtSearchToAmount.SelectionStart = txtSearchToAmount.Text.Length;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetParameters();
            switch (_FilterType)
            {
                case FilterType.Id:
                    try
                    {
                        FilterDataGrid(FilterType.Id, int.Parse(ContentSerchBox), String.Empty, String.Empty);
                    }
                    catch (Exception)
                    {
                        DGV.ItemsSource = null;
                    }
                    break;
                case FilterType.ItemName:
                    FilterDataGrid(FilterType.ItemName, 0, String.Empty, ContentSerchBox);
                    break;
                case FilterType.Description:
                    FilterDataGrid(FilterType.Id, 0, ContentSerchBox, String.Empty);
                    break;
                case FilterType.Date:
                    FilterDataGrid(FilterType.Date, 0, String.Empty, ContentSerchBox);
                    break;
            }
        }

        #endregion

        #endregion
    }
}
