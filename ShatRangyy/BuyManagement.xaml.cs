using Business;
using ShatRangyy.CustomControls;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeiwModels;

namespace ShatRangyy
{
    public partial class BuyManagement : UserControl
    {
        public BuyManagement()
        {
            InitializeComponent();
        }

        #region Varibles And Objects

        PersianCalendar PersianCalendar = new PersianCalendar();
        BuyDocument_BL BuyDocument_BL = new BuyDocument_BL();
        Account_BL Account_BL = new Account_BL();
        Account CurrentAccount;
        public enum FilterType
        {
            Id, ItemName, SellerName, Price, Costs, Number, TotalPrice, PayType, Date, Name
        }
        FilterType _FilterType;
        string SellerName, ItemName, CurentDate, PayType, TextSearchContent;
        int Id, SellerAccountId, Number, FromNumber, ToNumber, Year, Month, Day, SearchYear, SearchMonth, SearchDay;
        decimal Price, Costs, TotalPrice, FromAmount, ToAmount;
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
            if (String.IsNullOrEmpty(txtSellerName.Text) && PayType == "نسیه")
            {
                _ShowMessage("لطفا نام فروشنده را وارد کنید .", MessageBox_.enumType.Warning);
                txtSellerName.Focus();
                return false;
            }
            if (txtSellerName.Text.Length < 2 && PayType == "نسیه")
            {
                _ShowMessage("نام فروشنده باید بیشتر از 2 حرف باشد .", MessageBox_.enumType.Warning);
                txtSellerName.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtItemName.Text))
            {
                _ShowMessage("لطفا نام کالا را وارد کنید .", MessageBox_.enumType.Warning);
                txtItemName.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtNumber.Text))
            {
                _ShowMessage("لطفا تعداد کالا را وارد کنید .", MessageBox_.enumType.Warning);
                txtNumber.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtPrice.Text))
            {
                _ShowMessage("لطفا قیمت کالا را وارد کنید .", MessageBox_.enumType.Warning);
                txtPrice.Focus();
                return false;
            }
            if (txtPrice.Text.Length < 3)
            {
                _ShowMessage("مبلغ قیمت باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
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
                _ShowMessage("لطفا بهای کل را وارد کنید .", MessageBox_.enumType.Warning);
                txtTotalPrice.Focus();
                return false;
            }
            if (txtTotalPrice.Text.Length < 3)
            {
                _ShowMessage("مبلغ بهای کل باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
                txtTotalPrice.Focus();
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

            if (BuyDocument_BL.Exist(SellerName, CurentDate, ItemName, Price, Number, PayType) && _Update != true)
            {
                _ShowMessage("این سند قبلا ذخیره شده ،لطفا مشخصات را تغییر دهید .", MessageBox_.enumType.Warning);
                txtSellerName.Focus();
                return false;
            }
            return true;
        }
        void GetParameters()
        {
            SellerName = txtSellerName.Text;
            ItemName = txtItemName.Text;
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
            TextSearchContent = txtSearch.Text;
            CurentDate = $"{Year}/{Month}/{Day}";
        }
        void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        void Clear()
        {
            txtSellerName.Text = String.Empty;
            txtItemName.Text = String.Empty;
            txtNumber.Text = String.Empty;
            txtPrice.Text = String.Empty;
            txtCosts.Text = String.Empty;
            txtTotalPrice.Text = String.Empty;
            txtSearch.Text = String.Empty;
            SellCash.IsChecked = true;
            SellCredit.IsChecked = false;
            GetParameters();
            FilterDataGrid( SearchYear, SearchMonth, SearchDay);
            Account_Selector.Visibility = Visibility.Hidden;
            _Update = false;
            txtSellerName.Focus();
        }
        void ClearDataGridIndex()
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
        void Insert()
        {
            GetParameters();
            if (ParametersValidation())
            {
                BuyDocument buyDocument = new BuyDocument();
                #region Get Data
                buyDocument.SellerName = SellerName;
                buyDocument.ItemName = ItemName;
                buyDocument.Number = Number;
                buyDocument.Price = Price;
                buyDocument.Costs = Costs;
                buyDocument.TotalPrice = TotalPrice;
                buyDocument.Year = Year;
                buyDocument.Month = Month;
                buyDocument.Day = Day;
                buyDocument.Date = CurentDate;
                buyDocument.PayType = PayType;
                buyDocument.SellerAccountId = SellerAccountId;
                #endregion
                if (_Update)
                {
                    //Update
                    if (BuyDocument_BL.UpdateBuyDocument( Id, buyDocument))
                    {
                        _ShowMessage("ویرایش سند با موفقیت انجام شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("سند ویرایش نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtSellerName.Focus();
                    }
                }
                else
                {
                    //Insert
                    if (BuyDocument_BL.InsertBuyDocument(buyDocument))
                    {
                        _ShowMessage("سند با موفقیت ذخیره شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("سند  ذخیره نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtSellerName.Focus();
                    }
                }
            }
        }
        void Update()
        {
            BuyDocument buyDocument = new BuyDocument();
            buyDocument = BuyDocument_BL.GetBuyDocumentById(Id);
            txtSellerName.Text = buyDocument.SellerName;
            txtItemName.Text = buyDocument.ItemName;
            txtNumber.Text = buyDocument.Number.ToString();
            txtPrice.Text = ThreeDigitSeparator(buyDocument.Price.ToString());
            txtCosts.Text = ThreeDigitSeparator(buyDocument.Costs.ToString());
            txtTotalPrice.Text = ThreeDigitSeparator(buyDocument.TotalPrice.ToString());
            txtShowYear.Text = buyDocument.Year.ToString();
            txtShowMonth.Text = buyDocument.Month.ToString();
            txtShowDay.Text = buyDocument.Day.ToString();
            if (buyDocument.PayType == "نقدی")
            {
                SellCash.IsChecked = true;
                SellCredit.IsChecked = false;
            }
            if (buyDocument.PayType == "نسیه")
            {
                SellCash.IsChecked = false;
                SellCredit.IsChecked = true;
            }
            _Update = true;
            txtSellerName.Focus();
        }
        void Delete()
        {
            QuestionBox_ questionBox_ = new QuestionBox_();
            questionBox_.Content = "آیا می خواهید این سند را حذف کنید ؟";
            questionBox_.ShowDialog();
            if (questionBox_.Ok)
            {
                if (BuyDocument_BL.DeleteBuyDocument(Id))
                {
                    _ShowMessage("سند خرید با موفقیت حذف شد .", MessageBox_.enumType.Success);
                    FilterDataGrid(SearchYear, SearchMonth, SearchDay);
                }
                else
                {
                    _ShowMessage("سند خرید حذف نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                }
            }
        }
        void Search()
        {
            FilterBox filterBox = new FilterBox();
            filterBox.LabelContent1= "سریال";
            filterBox.LabelContent2 = "نام کالا";
            filterBox.LabelContent3 = "نام فروشنده";
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
                    _FilterType = FilterType.SellerName;
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
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نوع پرداخت");
                    _FilterType = FilterType.PayType;
                    break;
                case FilterBox.SelectedIndex.Index9:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو ");
                    _FilterType = FilterType.Date;
                    break;
                default:
                    break;
            }
        }
        void FilterDataGrid( int year, int month, int day)
        {
            DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByDate(year, year, month, month, day, day);
            ClearDataGridIndex();
        }
        void FilterDataGrid(FilterType filterType, decimal fromAmount, decimal toAmount
            , int fromNumber, int toNumber)
        {
            var _list = BuyDocument_BL.GetBuyDocumentsByDate( SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Price:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByPrice( _list, fromAmount, toAmount);
                    break;
                case FilterType.Costs:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByCosts( _list, fromAmount, toAmount);
                    break;
                case FilterType.TotalPrice:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByTotalPrice( _list, fromAmount, toAmount);
                    break;
                case FilterType.Number:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByItemNumber( _list, fromNumber, toNumber);
                    break;
                default:
                    break;
            }
            ClearDataGridIndex();
        }
        void FilterDataGrid(FilterType filterType, int id, string input)
        {
            GetParameters();
            var _list = BuyDocument_BL.GetBuyDocumentsByDate(SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Id:
                    DGV.ItemsSource = null;
                    DGV.Items.Add(BuyDocument_BL.GetBuyDocumentById(id));
                    break;
                case FilterType.ItemName:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByItemName( _list, input);
                    break;
                case FilterType.SellerName:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsBySellerName(null, input);
                    break;
                case FilterType.PayType:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByPayType(_list, input);
                    break;
                case FilterType.Name:
                    DGV.ItemsSource = BuyDocument_BL.GetBuyDocumentsByName(input);
                    break;
            }
            ClearDataGridIndex();
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

        #region ---User Control---

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearchYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtSearchDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            txtShowYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtShowMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtShowDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
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
                default:
                    break;
            }
        }

        #endregion

        #region ---Click---

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SellCash_Click(object sender, RoutedEventArgs e)
        {
            SellCash.IsChecked = true;
            SellCredit.IsChecked = false;
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
        #endregion

        #region ---Key Down---

        private void txtSellerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CurrentAccount != null)
                {
                    txtSellerName.Text = CurrentAccount.AccountName;
                }
                Account_Selector.Visibility = Visibility.Hidden;
                txtItemName.Focus();
            }
            if (e.Key == Key.F6)
            {
                AddAccount addAccount = new AddAccount();
                addAccount.ShowDialog();
                txtSellerName.Focus();
            }
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtNumber.Focus();
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
                        FilterDataGrid(FilterType.Id, int.Parse(TextSearchContent), String.Empty);
                        break;
                    case FilterType.ItemName:
                        FilterDataGrid(FilterType.ItemName, 0, TextSearchContent);
                        break;
                    case FilterType.SellerName:
                        FilterDataGrid(FilterType.SellerName, 0, TextSearchContent);
                        break;
                    case FilterType.PayType:
                        FilterDataGrid(FilterType.Id, 0, TextSearchContent);
                        break;
                    case FilterType.Date:
                        FilterDataGrid(FilterType.Name, 0, TextSearchContent);
                        break;
                    default:
                        FilterDataGrid(FilterType.Name, 0, TextSearchContent);
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

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPrice.Focus();
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtCosts.Focus();
            }
        }

        private void txtCosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtTotalPrice.Focus();
            }
        }

        private void txtTotalPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSave.Focus();
            }
        }

        #endregion

        #region ---Text Changed---

        private void txtCosts_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCosts.Text = ThreeDigitSeparator(txtCosts.Text);
            txtCosts.SelectionStart = txtCosts.Text.Length;
        }

        private void txtTotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTotalPrice.Text = ThreeDigitSeparator(txtTotalPrice.Text);
            txtTotalPrice.SelectionStart = txtTotalPrice.Text.Length;
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPrice.Text = ThreeDigitSeparator(txtPrice.Text);
            txtPrice.SelectionStart = txtPrice.Text.Length;
        }

        private void txtSellerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Visible;
            Account_Selector.DGV.ItemsSource = Account_BL.GetAccountsByName(txtSellerName.Text);
            if (Account_Selector.DGV.Items.Count != 0)
            {
                Account_Selector.DGV.SelectedIndex = 0;
            }
            if (!String.IsNullOrEmpty(txtSellerName.Text))
            {
                if (Account_Selector.DGV.SelectedItem != null)
                {
                    CurrentAccount = Account_Selector.account;
                    SellerAccountId = CurrentAccount.ID;
                }
                else
                {
                    CurrentAccount = null;
                    SellerAccountId = 0;
                }
            }
            else
            {
                CurrentAccount = null;
                SellerAccountId = 0;
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

        #endregion

        #region ---Lost Focus---

        private void txtSellerName_LostFocus(object sender, RoutedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Hidden;
        }

        #endregion

        #region ---Preview Text Input---

        private void AllTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #region ---Is Keyboard Focused Changed---

        private void txtTotalPrice_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            GetParameters();
            txtTotalPrice.Text = (Number * Price).ToString();
        }


        #endregion

        #region ---Data Grid---

        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV.SelectedItem != null)
            {
                BuyDocument obj = DGV.SelectedItem as BuyDocument;
                Id = obj.ID;
            }
        }

        #endregion

        #endregion

    }
}
