using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using VeiwModels;
using Business;
using System.Windows.Input;
using ShatRangyy.CustomControls;
using System.Text.RegularExpressions;

namespace ShatRangyy
{
    public partial class SellManagement : UserControl
    {
        public SellManagement()
        {
            InitializeComponent();
        }

        #region Variables And Opjects

        PersianCalendar persianCalendar = new PersianCalendar();
        SellDocument_BL SellDocument_BL = new SellDocument_BL();
        Item_BL Item_BL = new Item_BL();
        Account_BL  Account_BL = new Account_BL();
        Item CurrentItem;
        Account CurrentAccount;
        public enum FilterType
        {
            Id, ItemName, BuyerName, Price, Costs, Number, TotalPrice, PayType, Date
        }
        FilterType _FilterType;
        string CurrntDate, BuyerName, ItemName, PayType, TextSearchContent;
        decimal Price, TotalPrice, Costs, FromAmount, ToAmount;
        int Id, BuyerAccountId, ItemId, Number, FromNumber, ToNumber, Year, Month, Day, SearchYear, SearchMonth, SearchDay;
        bool _Update = false;
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
        bool ParametersValidation()
        {
            if (String.IsNullOrEmpty(txtBuyerName.Text) && PayType == "نسیه")
            {
                _ShowMessage("لطفا نام خریدار را وارد کنید .", MessageBox_.enumType.Warning);
                txtBuyerName.Focus();
                return false;
            }
            if (txtBuyerName.Text.Length < 3 && PayType == "نسیه")
            {
                _ShowMessage("نام خربدار باید بیشتر از 2 حرف باشد .", MessageBox_.enumType.Warning);
                txtBuyerName.Focus();
                return false;
            }

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

            if (String.IsNullOrEmpty(txtPrice.Text))
            {
                _ShowMessage("لطفا قیمت را وارد کنید .", MessageBox_.enumType.Warning);
                txtPrice.Focus();
                return false;
            }
            if (txtPrice.Text.Length < 4)
            {
                _ShowMessage("مقدار قیمت باید بیشتر از 3 کاراکتر باشد .", MessageBox_.enumType.Warning);
                txtPrice.Focus();
                return false;
            }

            if (txtCosts.Text.Length < 3 && !String.IsNullOrEmpty(txtCosts.Text))
            {
                _ShowMessage("مبلغ مخارج باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
                txtCosts.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtTotalPrice.Text))
            {
                _ShowMessage("لطفا بهای را وارد کنید .", MessageBox_.enumType.Warning);
                txtTotalPrice.Focus();
                return false;
            }
            if (txtTotalPrice.Text.Length < 4)
            {
                _ShowMessage("مقدار بهای کل باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
                txtTotalPrice.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtNumber.Text))
            {
                _ShowMessage("لطفا تعداد کالا را وارد کنید .", MessageBox_.enumType.Warning);
                txtNumber.Focus();
                return false;
            }
            if (CurrentItem.Number < Number)
            {
                _ShowMessage("تعداد کالا های وارد شده بیشتر از کالاهای موجود است .", MessageBox_.enumType.Warning);
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

            if (SellDocument_BL.Exist(BuyerName, CurrntDate, ItemName, Price, Number, PayType) && _Update != true)
            {
                _ShowMessage("این سند قبلا ذخیره شده ،لطفا مشخصات را تغییر دهید .", MessageBox_.enumType.Warning);
                txtBuyerName.Focus();
                return false;
            }
            return true;
        }
        void GetParameters()
        {
            
            if (String.IsNullOrEmpty(txtBuyerName.Text))
            {
                BuyerAccountId = 0;
            }
            BuyerName = txtBuyerName.Text;
            ItemName = txtItemName.Text;
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
            if (!String.IsNullOrEmpty(txtNumber.Text))
            {
                Number = int.Parse(txtNumber.Text);
            }
            if (!String.IsNullOrEmpty(txtPrice.Text))
            {
                Price = decimal.Parse(txtPrice.Text.Replace(",", ""));
            }
            if (!String.IsNullOrEmpty(txtCosts.Text))
            {
                Costs = decimal.Parse(txtCosts.Text.Replace(",", ""));
            }
            if (!String.IsNullOrEmpty(txtTotalPrice.Text))
            {
                TotalPrice = decimal.Parse(txtTotalPrice.Text.Replace(",", ""));
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
            if (!String.IsNullOrEmpty(txtSearchFromAmount.Text) && _FilterType == FilterType.Number)
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
            if (SellCash.IsChecked == true)
            {
                PayType = "نقدی";
            }
            else
            {
                PayType = "نسیه";
            }
            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                TextSearchContent = txtSearch.Text;
            }
            if (CurrentItem != null)
            {
                ItemId = CurrentItem.ID;
            }
            CurrntDate = $"{Year}/{Month}/{Day}";
        }
        void Clear()
        {
            txtBuyerName.Text = String.Empty;
            txtItemName.Text = String.Empty;
            txtNumber.Text = String.Empty;
            txtPrice.Text = String.Empty;
            txtCosts.Text = String.Empty;
            txtTotalPrice.Text = String.Empty;
            SellCash.IsChecked = true;
            SellCredit.IsChecked = false;
            txtSearch.Text = String.Empty;
            txtSearchFromAmount.Text = String.Empty;
            txtSearchToAmount.Text = String.Empty;
            _Update = false;
            Item_Selector.Visibility = Visibility.Hidden;
            Account_Selector.Visibility = Visibility.Hidden;
            GetParameters();
            FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            ClearDataGridIndex();
            txtBuyerName.Focus();
        }
        void ClearDataGridIndex()
        {
            if (DGV.Items.Count > 1)
            {
                DGV.SelectedIndex = DGV.Items.Count - 1;
            }
            else if (DGV.Items.Count == 1)
            {
                DGV.SelectedIndex = 1;
            }
        }
        void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        void Insert()
        {
            GetParameters();
            if (ParametersValidation())
            {
                SellDocument document = new SellDocument();
                #region Get Data
                document.BuyerName = BuyerName;
                document.Date = CurrntDate;
                document.Year = Year;
                document.Month = Month;
                document.Day = Day;
                document.Costs = Costs;
                document.Number = Number;
                document.Price = Price;
                document.ItemName = ItemName;
                document.PayType = PayType;
                document.TotalPrice = TotalPrice;
                document.BuyerAccountId = BuyerAccountId;
                document.ItemId = ItemId;
                #endregion
                if (_Update)
                {
                    //Update document
                    document.ID = Id;
                    if (SellDocument_BL.UpdateSellDocument(Id, document))
                    {
                        _ShowMessage("سند با موفقیت ویرایش شد .", MessageBox_.enumType.Success);
                        Clear();
                        FilterDataGrid(SearchYear, SearchMonth, SearchDay);
                    }
                    else
                    {
                        _ShowMessage("سند ویرایش نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtBuyerName.Focus();
                    }
                }
                else
                {
                    //Insert document
                    if (SellDocument_BL.InsertSellDocument(document))
                    {
                        _ShowMessage("سند با موفقیت ثبت شد .", MessageBox_.enumType.Success);
                        Clear();
                        FilterDataGrid(SearchYear, SearchMonth, SearchDay);
                    }
                    else
                    {
                        _ShowMessage("سند ثبت نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtBuyerName.Focus();
                    }
                }
            }
        }
        void Update()
        {
            SellDocument sellDocument = new SellDocument();
            sellDocument = SellDocument_BL.GetSellDocumentById(Id);
            txtBuyerName.Text = sellDocument.BuyerName;
            txtItemName.Text = sellDocument.ItemName;
            txtNumber.Text = sellDocument.Number.ToString();
            txtPrice.Text = ThreeDigitSeparator(sellDocument.Price.ToString());
            txtTotalPrice.Text = ThreeDigitSeparator(sellDocument.TotalPrice.ToString());
            txtCosts.Text = ThreeDigitSeparator(sellDocument.Costs.ToString());
            txtShowYear.Text = sellDocument.Year.ToString();
            txtShowMonth.Text = sellDocument.Month.ToString();
            txtShowDay.Text = sellDocument.Day.ToString();
            BuyerAccountId = sellDocument.BuyerAccountId;
            if (sellDocument.PayType == "نقدی")
            {
                SellCash.IsChecked = true;
                SellCredit.IsChecked = false;
            }
            if (sellDocument.PayType == "نسیه")
            {
                SellCash.IsChecked = false;
                SellCredit.IsChecked = true;
            }
            txtBuyerName.Focus();
            Item_Selector.Visibility = Visibility.Hidden;
            Account_Selector.Visibility = Visibility.Hidden;
            _Update = true;
        }
        void Delete()
        {
            QuestionBox_ questionBox_ = new QuestionBox_();
            questionBox_.Content = "آیا می خواهید این سند را حذف کنید ؟";
            questionBox_.ShowDialog();
            if (questionBox_.Ok)
            {
                if (SellDocument_BL.DeleteSellDocument(Id))
                {
                    _ShowMessage("سند با موفقیت حذف شد .", MessageBox_.enumType.Success);
                    FilterDataGrid(SearchYear, SearchMonth, SearchDay);
                }
                else
                {
                    _ShowMessage("سند حذف نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Success);
                }
            }
        }
        void Search()
        {
            FilterBox filterBox = new FilterBox();
            filterBox.LabelContent1 = "سریال";
            filterBox.LabelContent2 = "نام کالا";
            filterBox.LabelContent3 = "نام خریدار";
            filterBox.LabelContent4 = "قیمت";
            filterBox.LabelContent5 = "مخارج";
            filterBox.LabelContent6 = "تعداد";
            filterBox.LabelContent7 = "بهای کل";
            filterBox.LabelContent8 = "نوع پرداخت";
            filterBox.LabelContent9 = "تاریخ";
            if (_FilterType == FilterType.Date)
            {
                filterBox.CheckBox9.IsChecked = true;
            }
            filterBox.ShowDialog();
            switch (filterBox.selectedIndex)
            {
                case FilterBox.SelectedIndex.Index1:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس سریال");
                    _FilterType = FilterType.Id;
                    break;
                case FilterBox.SelectedIndex.Index2:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام کالا");
                    _FilterType = FilterType.ItemName;
                    break;
                case FilterBox.SelectedIndex.Index3:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام فروشنده");
                    _FilterType = FilterType.BuyerName;
                    break;
                case FilterBox.SelectedIndex.Index4:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchFromAmount, "از مبلغ");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchToAmount, "تا مبلغ");
                    _FilterType = FilterType.Price;
                    break;
                case FilterBox.SelectedIndex.Index5:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchFromAmount, "از مبلغ");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchToAmount, "تا مبلغ");
                    _FilterType = FilterType.Costs;
                    break;
                case FilterBox.SelectedIndex.Index6:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchFromAmount, "از تعداد");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchToAmount, "تا تعداد");
                    _FilterType = FilterType.Number;
                    break;
                case FilterBox.SelectedIndex.Index7:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchFromAmount, "از مبلغ");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchToAmount, "تا مبلغ");
                    _FilterType = FilterType.TotalPrice;
                    break;
                case FilterBox.SelectedIndex.Index8:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نوع پرداخت");
                    _FilterType = FilterType.PayType;
                    break;
                case FilterBox.SelectedIndex.Index9:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو ");
                    _FilterType = FilterType.Date;
                    break;
                default:
                    break;
            }
        }
        void FilterDataGrid(int year, int month, int day)
        {
            DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByDate(year, year, month, month, day, day);
            ClearDataGridIndex();
        }
        void FilterDataGrid(FilterType filterType, decimal fromAmount, decimal toAmount
            , int fromNumber, int toNumber)
        {
            var _list = SellDocument_BL.GetSellDocumentsByDate(SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Price:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByPrice(_list, fromAmount, toAmount);
                    break;
                case FilterType.Costs:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByCosts(_list, fromAmount, toAmount);
                    break;
                case FilterType.TotalPrice:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByTotalPrice(_list, fromAmount, toAmount);
                    break;
                case FilterType.Number:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByItemNumber(_list, fromNumber, toNumber);
                    break;
                default:
                    break;
            }
            ClearDataGridIndex();
        }
        void FilterDataGrid(FilterType filterType, int id, string input)
        {
            var _list = SellDocument_BL.GetSellDocumentsByDate(SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Id:
                    DGV.ItemsSource = null;
                    DGV.Items.Add(SellDocument_BL.GetSellDocumentById(id));
                    break;
                case FilterType.ItemName:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByItemName(_list, input);
                    break;
                case FilterType.BuyerName:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByBuyerName(null, input);
                    break;
                case FilterType.PayType:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByPayType(_list, input);
                    break;
                case FilterType.Date:
                    DGV.ItemsSource = SellDocument_BL.GetSellDocumentsByName(input);
                    break;
            }
            ClearDataGridIndex();
        }
        void InvoiceShow()
        {
            Invoice invoice = new Invoice();
            invoice.ShowDialog();
        }
        void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }
        string ThreeDigitSeparator(string input)
        {
            if (input != String.Empty)
            {
                try
                {
                    return long.Parse(input.Replace(",", "")).ToString("#,#");
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

        #region ---User Control---
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearchYear.Text = persianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchMonth.Text = persianCalendar.GetMonth(DateTime.Now).ToString();
            txtSearchDay.Text = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            txtShowYear.Text = persianCalendar.GetYear(DateTime.Now).ToString();
            txtShowMonth.Text = persianCalendar.GetMonth(DateTime.Now).ToString();
            txtShowDay.Text = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            _FilterType = FilterType.Date;
            Clear();
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
                case Key.Up:
                    //if (Account_Selector.DGV.Items.Count > 1)
                    //{
                    //    Account_Selector.DGV.SelectedIndex += 1;
                    //}
                    break; 
                case Key.Down:
                    
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ---Text Changed---

        private void txtBuyerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Visible;
            Account_Selector.DGV.ItemsSource = Account_BL.GetAccountsByName(txtBuyerName.Text);
            if (Account_Selector.DGV.Items.Count != 0)
            {
                Account_Selector.DGV.SelectedIndex = 0;
            }
            if (!String.IsNullOrEmpty(txtBuyerName.Text))
            {
                if (Account_Selector.DGV.SelectedItem != null)
                {
                    CurrentAccount = Account_Selector.account;
                    BuyerAccountId = CurrentAccount.ID;
                }
                else
                {
                    CurrentAccount = null;
                    BuyerAccountId = 0;
                }
            }
            else
            {
                CurrentAccount = null;
                BuyerAccountId = 0;
            }
        }

        private void txtItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Item_Selector.Visibility = Visibility.Visible;
            Item_Selector.DGV.ItemsSource = Item_BL.GetItemsByName(null, txtItemName.Text);
            if (Item_Selector.DGV.Items.Count != 0)
            {
                Item_Selector.DGV.SelectedIndex = 0;
            }
            if (!String.IsNullOrEmpty(txtItemName.Text))
            {
                if (Item_Selector.DGV.SelectedItem != null)
                {
                    CurrentItem = Item_Selector.item;
                    ItemId = CurrentItem.ID;
                }
                else
                {
                    CurrentItem = null;
                    ItemId = 0;
                }
            }
            else
            {
                CurrentItem = null;
                ItemId = 0;
            }
        }

        private void txtTotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTotalPrice.Text = ThreeDigitSeparator(txtTotalPrice.Text);
            txtTotalPrice.SelectionStart = txtTotalPrice.Text.Length;
        }

        private void txtCosts_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCosts.Text = ThreeDigitSeparator(txtCosts.Text);
            txtCosts.SelectionStart = txtCosts.Text.Length;
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPrice.Text = ThreeDigitSeparator(txtPrice.Text);
            txtPrice.SelectionStart = txtPrice.Text.Length;
        }

        private void txtSearchToAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_FilterType != FilterType.Number)
            {
                txtSearchToAmount.Text = ThreeDigitSeparator(txtSearchToAmount.Text);
                txtSearchToAmount.SelectionStart = txtSearchToAmount.Text.Length;
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

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetParameters();
            switch (_FilterType)
            {
                case FilterType.Id:
                    try
                    {
                        FilterDataGrid(FilterType.Id, int.Parse(TextSearchContent), String.Empty);
                    }
                    catch (Exception)
                    {
                        DGV.ItemsSource = null;
                    }
                    break;
                case FilterType.ItemName:
                    FilterDataGrid(FilterType.ItemName, 0, TextSearchContent);
                    break;
                case FilterType.BuyerName:
                    FilterDataGrid(FilterType.BuyerName, 0, TextSearchContent);
                    break;
                case FilterType.PayType:
                    FilterDataGrid(FilterType.PayType, 0, TextSearchContent);
                    break;
                case FilterType.Date:
                    FilterDataGrid(FilterType.Date, 0, TextSearchContent);
                    break;
            }
        }

        #endregion

        #region ---Key Down---

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CurrentItem != null)
                {
                    txtItemName.Text = CurrentItem.ItemName;
                    txtPrice.Text = CurrentItem.SellPrice.ToString("#,#");
                    txtNumber.Text = CurrentItem.Number.ToString();
                }
                Item_Selector.Visibility = Visibility.Hidden;
                txtNumber.Focus();
            }
        }

        private void txtBuyerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CurrentAccount != null)
                {
                    txtBuyerName.Text = CurrentAccount.AccountName;
                }
                txtItemName.Focus();
                Account_Selector.Visibility = Visibility.Hidden;
            }
            if (e.Key == Key.F7)
            {
                AddAccount addAccount = new AddAccount();
                addAccount.ShowDialog();
                txtBuyerName.Focus();
            }
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPrice.Focus();
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                txtCosts.Focus();
            }
        }

        private void txtCosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                txtTotalPrice.Focus();
            }
        }

        private void txtTotalPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnSave.Focus();
            }
        }

        private void txtSearchFromAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtSearchToAmount.Focus();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtSearchToAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                switch (_FilterType)
                {
                    case FilterType.Costs:
                        FilterDataGrid(FilterType.Costs, FromAmount, ToAmount, 0, 0);
                        break;
                    case FilterType.Price:
                        FilterDataGrid(FilterType.Price, FromAmount, ToAmount, 0, 0);
                        break;
                    case FilterType.TotalPrice:
                        FilterDataGrid(FilterType.TotalPrice, FromAmount, ToAmount, 0, 0);
                        break;
                    case FilterType.Number:
                        FilterDataGrid(FilterType.Number, 0, 0, FromNumber, ToNumber);
                        break;
                }
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

        #endregion

        #region ---Click---

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            InvoiceShow();
        }

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Insert();
        }

        private void SellCredit_Click(object sender, RoutedEventArgs e)
        {
            SellCredit.IsChecked = true;
            SellCash.IsChecked = false;
        }

        private void SellCash_Click(object sender, RoutedEventArgs e)
        {
            SellCash.IsChecked = true;
            SellCredit.IsChecked = false;
        }

        #endregion

        #region ---Lost Focus---

        private void txtBuyerName_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (!String.IsNullOrEmpty(txtBuyerName.Text))
            //{
            //    if (Account_Selector.DGV.SelectedItem != null)
            //    {
            //        CurrentAccount = Account_Selector.account;
            //        BuyerAccountId = CurrentAccount.ID;
            //        txtBuyerName.Text = CurrentAccount.AccountName;
            //    }
            //    else
            //    {
            //        CurrentAccount = null;
            //        BuyerAccountId = 0;
            //    }
            //    txtItemName.Focus();
            //}
            Account_Selector.Visibility = Visibility.Hidden;
        }

        private void txtItemName_LostFocus(object sender, RoutedEventArgs e)
        {
            Item_Selector.Visibility = Visibility.Hidden;
        }


        #endregion

        #region ---Preview Text Input---

        private void txtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtCosts_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtTotalPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtShowYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtShowMonth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtShowDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtMonth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBoxs_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #region ---Got Keyboard Focus---

        private void txtTotalPrice_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            GetParameters();
            txtTotalPrice.Text = (Number * Price).ToString();
        }
        
        #endregion

        #region ---Data Grid--

        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV.SelectedItem != null)
            {
                SellDocument obj = DGV.SelectedItem as SellDocument;
                Id = obj.ID;
            }
        }
        
        #endregion

        #endregion

    }
}
