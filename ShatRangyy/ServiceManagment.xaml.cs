using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Business;
using Business.Service;
using ShatRangyy.CustomControls;
using VeiwModels;

namespace ShatRangyy
{
    public partial class ServiceManagment : UserControl
    {
        public ServiceManagment()
        { 
            InitializeComponent();
        }

        #region Variables And Opjects 

        PersianCalendar persianCalendar = new PersianCalendar();
        SerVice_BL SerVice_BL = new SerVice_BL();
        Account_BL Account_BL = new Account_BL();
        Item_BL Item_BL = new Item_BL();
        Item CurrentItem;
        Account CurrentAccount;
        public enum FilterType
        {
            Id, CustomerName, Description, Comision, StartDate, EndDate
        }
        FilterType _FilterType;
        string CustomerName, ItemName, DescriptionService,
            StartDate, EndDate, PayType, TextSearchContent;
        int Id, CustomerAccountId, ItemId, StartYear, StartMonth, StartDay, EndYear, EndMonth,
            EndDay, SearchYear, SearchMonth, SearchDay;
        decimal Comision, FromAmount, ToAmount;
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
            if (String.IsNullOrEmpty(txtCustomerName.Text) && PayType == "نسیه")
            {
                _ShowMessage("لطفا نام مشتری را وارد کنید .", MessageBox_.enumType.Warning);
                txtCustomerName.Focus();
                return false;
            }
            if (txtCustomerName.Text.Length < 3 && PayType == "نسیه")
            {
                _ShowMessage("نام مشتری باید بیشتر از 2 حرف باشد .", MessageBox_.enumType.Warning);
                txtCustomerName.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtDesciptionService.Text))
            {
                _ShowMessage("لطفا شرح خدمات را وارد کنید .", MessageBox_.enumType.Warning);
                txtDesciptionService.Focus();
                return false;
            }
            if (txtDesciptionService.Text.Length < 3)
            {
                _ShowMessage("شرح خدمات باید بیشتر از 2 حرف باشد .", MessageBox_.enumType.Warning);
                txtDesciptionService.Focus();
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

            if (SerVice_BL.Exist(CustomerName, StartDate, Comision) && _Update != true)
            {
                _ShowMessage("این سند قبلا ثبت شده .", MessageBox_.enumType.Warning);
                txtCustomerName.Focus();
                return false;
            }
            return true;
        }
        void GetParameters()
        {
            ItemName = txtItemName.Text;
            CustomerName = txtCustomerName.Text;
            DescriptionService = txtDesciptionService.Text;
            if (!String.IsNullOrEmpty(txtShowYear.Text))
            {
                StartYear = int.Parse(txtShowYear.Text);
            }
            else
            {
                StartYear = 0;
            }
            if (!String.IsNullOrEmpty(txtShowMonth.Text))
            {
                StartMonth = int.Parse(txtShowMonth.Text);
            }
            else
            {
                StartMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtShowDay.Text))
            {
                StartDay = int.Parse(txtShowDay.Text);
            }
            else
            {
                StartDay = 0;
            }
            if (!String.IsNullOrEmpty(txtComision.Text))
            {
                Comision = decimal.Parse(txtComision.Text.Replace(",", ""));
            }
            else
            {
                Comision = 0;
            }
            if (!String.IsNullOrEmpty(txtEndYear.Text))
            {
                EndYear = int.Parse(txtEndYear.Text);
            }
            else
            {
                EndYear = 0;
            }
            if (!String.IsNullOrEmpty(txtEndMonth.Text))
            {
                EndMonth = int.Parse(txtEndMonth.Text);
            }
            else
            {
                EndMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtEndDay.Text))
            {
                EndDay = int.Parse(txtEndDay.Text);
            }
            else
            {
                EndDay = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchYear.Text))
            {
                SearchYear = int.Parse(txtSearchYear.Text);
            }
            else
            {
                SearchYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchByMonth.Text))
            {
                SearchMonth = int.Parse(txtSearchByMonth.Text);
            }
            else
            {
                SearchMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchByDay.Text))
            {
                SearchDay = int.Parse(txtSearchByDay.Text);
            }
            else
            {
                SearchDay = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromAmount.Text))
            {
                FromAmount = decimal.Parse(txtSearchFromAmount.Text.Replace(",", ""));
            }
            else
            {
                FromAmount = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToAmount.Text))
            {
                ToAmount = decimal.Parse(txtSearchToAmount.Text.Replace(",", ""));
            }
            else
            {
                ToAmount = 0;
            }
            TextSearchContent = txtSearch.Text;
            if (SellCash.IsChecked == true)
            {
                PayType = "نقدی";
            }
            else
            {
                PayType = "نسیه";
            }
            if (CurrentItem != null)
            {
                ItemId = CurrentItem.ID;
            }
            StartDate = $"{StartYear}/{StartMonth}/{StartDay}";
            EndDate = $"{EndYear}/{EndMonth}/{EndDay}";
        }
        void Clear()
        {
            txtCustomerName.Text = String.Empty;
            txtDesciptionService.Text = String.Empty;
            txtItemName.Text = String.Empty;
            txtComision.Text = String.Empty;
            txtEndYear.Text = String.Empty;
            txtEndMonth.Text = String.Empty;
            txtEndDay.Text = String.Empty;
            txtSearch.Text = String.Empty;
            GetParameters();
            FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            CustomerAccountId = 0;
            SellCredit.IsChecked = false;
            SellCash.IsChecked= true;
            ClearDataGrid();
            _Update = false;
            Account_Selector.Visibility = Visibility.Hidden;
            Item_Selector.Visibility = Visibility.Hidden;
            txtCustomerName.Focus();
        }
        void ClearDataGrid()
        {
            if (DG.Items.Count > 1)
            {
                DG.SelectedIndex = DG.Items.Count - 1;
            }
            else if (DG.Items.Count == 1)
            {
                DG.SelectedIndex = 1;
            }
        }
        void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        void Update()
        {
            SerVice serVice = new SerVice();
            serVice = SerVice_BL.GetServiceById(Id);
            txtCustomerName.Text = serVice.CostomerName;
            txtItemName.Text = serVice.ItemName;
            txtDesciptionService.Text = serVice.DescriptionService;
            txtComision.Text = serVice.Comision.ToString();
            txtShowYear.Text = serVice.StartYear.ToString();
            txtShowMonth.Text = serVice.StartMonth.ToString();
            txtShowDay.Text = serVice.StartDay.ToString();
            txtEndYear.Text = serVice.EndYear.ToString();
            txtEndMonth.Text = serVice.EndMonth.ToString();
            txtEndDay.Text = serVice.EndDay.ToString();
            CustomerAccountId = serVice.CustomerAccountId;
            if (serVice.PayType == "نقدی")
            {
                SellCash.IsChecked = true;
                SellCredit.IsChecked = false;
            }
            if (serVice.PayType == "نسیه")
            {
                SellCash.IsChecked = false;
                SellCredit.IsChecked = true;
            }
            _Update = true;
            Item_Selector.Visibility = Visibility.Hidden;
            Account_Selector.Visibility = Visibility.Hidden;
            txtCustomerName.Focus();
        }
        void Insert()
        {
            GetParameters();
            if (ParametersValidation())
            {
                SerVice SerVice = new SerVice();
                #region Get Data
                SerVice.CostomerName = CustomerName;
                SerVice.ItemName = ItemName;
                SerVice.DescriptionService = DescriptionService;
                SerVice.Comision = Comision;
                SerVice.StartDate = StartDate;
                SerVice.EndDate = EndDate;
                SerVice.EndYear = EndYear;
                SerVice.EndMonth = EndMonth;
                SerVice.EndDay = EndDay;
                SerVice.StartYear = StartYear;
                SerVice.StartMonth = StartMonth;
                SerVice.StartDay = StartDay;
                SerVice.PayType = PayType;
                SerVice.CustomerAccountId = CustomerAccountId;
                SerVice.ItemId = ItemId;
                #endregion
                if (_Update)
                {
                    //Update Service
                    SerVice.ID = Id;
                    if (SerVice_BL.UpdateService(Id, SerVice))
                    {
                        _ShowMessage("سند با موفقیت ویرایش شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("سند ویرایش نشد لطفا دوبار امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtCustomerName.Focus();
                    }
                }
                else
                {
                    //Insert Service
                    if (SerVice_BL.InsertService(SerVice))
                    {
                        _ShowMessage("سند با موفقیت ثبت شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("ثبت سند با خطا مواجه شد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtCustomerName.Focus();
                    }
                }
            }
        }
        void Delete()
        {
            QuestionBox_ questionBox_ = new QuestionBox_();
            questionBox_.Content = "آیا از حذف این سند مطمئن هستید ؟";
            questionBox_.ShowDialog();
            if (questionBox_.Ok)
            {
                if (SerVice_BL.DeleteService(Id))
                {
                    _ShowMessage("سند با موفقیت حذف شد .", MessageBox_.enumType.Success);
                    ClearDataGrid();
                }
                else
                {
                    _ShowMessage("حذف سند با خطا مواجه شد .", MessageBox_.enumType.Erorr);
                }
            }
        }
        void Search()
        {
            FilterBox filterBox = new FilterBox();
            filterBox.LabelContent1 = "سریال";
            filterBox.LabelContent2 = "شرح";
            filterBox.LabelContent3 = "نام مشتری";
            filterBox.LabelContent4 = "کارمزد";
            filterBox.LabelContent5 = "تاریخ شروع";
            filterBox.LabelContent6 = "تاریخ پایان";
            filterBox.LabelContent7 = "....";
            filterBox.LabelContent8 = "....";
            filterBox.LabelContent9 = "....";
            if (_FilterType == FilterType.StartDate)
            {
                filterBox.CheckBox5.IsChecked = true;
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
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس شرح");
                    _FilterType = FilterType.Description;
                    break;
                case FilterBox.SelectedIndex.Index3:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام مشتری");
                    _FilterType = FilterType.CustomerName;
                    break;
                case FilterBox.SelectedIndex.Index4:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchFromAmount, "از مبلغ");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchToAmount, "تا مبلغ");
                    _FilterType = FilterType.Comision;
                    break;
                case FilterBox.SelectedIndex.Index5:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو ");
                    _FilterType = FilterType.StartDate;
                    break;
                case FilterBox.SelectedIndex.Index6:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو ");
                    _FilterType = FilterType.EndDate;
                    break;
                default:
                    break;
            }
        }
        void FilterDataGrid(int year, int month, int day)
        {
            if (_FilterType == FilterType.StartDate)
            {
                DG.ItemsSource = SerVice_BL.GetServicesByStartDate(year, year, month, month, day, day);
            }
            if (_FilterType == FilterType.EndDate)
            {
                DG.ItemsSource = SerVice_BL.GetServicesByEndDate(year, year, month, month, day, day);
            }
            ClearDataGrid();
        }
        void FilterDataGrid(FilterType filterType, decimal fromAmount, decimal toAmount)
        {
            var _list = SerVice_BL.GetServicesByStartDate(SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            if (filterType == FilterType.Comision)
            {
                DG.ItemsSource = SerVice_BL.GetSerVicesByComision(_list, fromAmount, toAmount);
            }
            ClearDataGrid();
        }
        void FilterDataGrid(FilterType filterType, int id, string customerName,
            string description)
        {
            var _list = SerVice_BL.GetServicesByStartDate(SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Id:
                    DG.ItemsSource = null;
                    DG.Items.Add(SerVice_BL.GetServiceById(id));
                    break;
                case FilterType.CustomerName:
                    DG.ItemsSource = SerVice_BL.GetSerVicesByCustomerName(null, customerName);
                    break;
                case FilterType.Description:
                    DG.ItemsSource = SerVice_BL.GetServicesByDescription(_list, description);
                    break;
                case FilterType.StartDate:
                    DG.ItemsSource = SerVice_BL.GetSerVicesByNameOrDescription(customerName);
                    break;
            }
            ClearDataGrid();
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
            txtShowYear.Text = persianCalendar.GetYear(DateTime.Now).ToString();
            txtShowMonth.Text = persianCalendar.GetMonth(DateTime.Now).ToString();
            txtShowDay.Text = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            txtSearchYear.Text = persianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchByMonth.Text = persianCalendar.GetMonth(DateTime.Now).ToString();
            txtSearchByDay.Text = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            _FilterType = FilterType.StartDate;
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
                case Key.F4:
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

        private void txtComision_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtEndDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtEndMonth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtEndYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtSearchYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtSearchByMonth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtSearchByDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #region ---Click---

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Insert();
        }

        private void SellCash_Click(object sender, RoutedEventArgs e)
        {
            SellCash.IsChecked = true;
            SellCredit.IsChecked = false;
        }

        private void SellCredit_Click(object sender, RoutedEventArgs e)
        {
            SellCredit.IsChecked = true;
            SellCash.IsChecked = false;
        }

        #endregion

        #region ---Key Down---

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CurrentAccount != null)
                {
                    txtCustomerName.Text = CurrentAccount.AccountName;
                }
                txtItemName.Focus();
                Account_Selector.Visibility = Visibility.Hidden;
            }
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                AddAccount addAccount = new AddAccount();
                addAccount.ShowDialog();
                txtCustomerName.Focus();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                switch (_FilterType)
                {
                    case FilterType.Id:
                        try
                        {
                            FilterDataGrid(FilterType.Id, int.Parse(TextSearchContent), String.Empty, String.Empty);
                        }
                        catch (Exception)
                        {
                            DG.ItemsSource = null;
                        }
                        break;
                    case FilterType.CustomerName:
                        FilterDataGrid(FilterType.CustomerName, 0, TextSearchContent, String.Empty);
                        break;
                    case FilterType.Description:
                        FilterDataGrid(FilterType.Description, 0, String.Empty, TextSearchContent);
                        break;
                    case FilterType.StartDate:
                        FilterDataGrid(FilterType.StartDate, 0, TextSearchContent, String.Empty);
                        break;
                }
            }
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CurrentItem != null)
                {
                    txtItemName.Text = CurrentItem.ItemName;
                    txtComision.Text = CurrentItem.SellPrice.ToString("#,#");
                }
                txtDesciptionService.Focus();
                Item_Selector.Visibility = Visibility.Hidden;
            }
        }

        private void txtSearchFromAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtSearchToAmount.Focus();
            }
        }

        private void txtSearchToAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_FilterType == FilterType.Comision)
                {
                    GetParameters();
                    FilterDataGrid(FilterType.Comision, FromAmount, ToAmount);
                }
            }
        }

        private void txtComision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSave.Focus();
            }
        }

        private void txtDesciptionService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtComision.Focus();
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

        private void txtSearchByMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            }
        }

        private void txtSearchByDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            }
        }

        #endregion

        #region ---Data Grid---

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                SerVice obj = DG.SelectedItem as SerVice;
                Id = obj.ID;
            }
        }

        #endregion

        #region ---Lost Focus---
        private void txtCustomerName_LostFocus(object sender, RoutedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Hidden;
        }

        private void txtItemName_LostFocus(object sender, RoutedEventArgs e)
        {
            Item_Selector.Visibility= Visibility.Hidden;
        }

        #endregion

        #region ---Text Changed---

        private void txtComision_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtComision.Text = ThreeDigitSeparator(txtComision.Text);
            txtComision.SelectionStart = txtComision.Text.Length;
        }

        private void txtSearchFromAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSearchFromAmount.Text = ThreeDigitSeparator(txtSearchFromAmount.Text);
            txtSearchFromAmount.SelectionStart = txtSearchFromAmount.Text.Length;
        }

        private void txtSearchToAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSearchToAmount.Text = ThreeDigitSeparator(txtSearchToAmount.Text);
            txtSearchToAmount.SelectionStart = txtSearchToAmount.Text.Length;
        }

        private void txtCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Visible;
            Account_Selector.DGV.ItemsSource = Account_BL.GetAccountsByName(txtCustomerName.Text);
            if (Account_Selector.DGV.Items.Count != 0)
            {
                Account_Selector.DGV.SelectedIndex = 0;
            }
            if (!String.IsNullOrEmpty(txtCustomerName.Text))
            {
                if (Account_Selector.DGV.SelectedItem != null)
                {
                    CurrentAccount = Account_Selector.account;
                    CustomerAccountId = Account_Selector.account.ID;
                }
                else
                {
                    CurrentAccount = null;
                    CustomerAccountId = 0;
                }
            }
            else
            {
                CurrentAccount = null;
                CustomerAccountId = 0;
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

        #endregion

        #endregion

    }
}
