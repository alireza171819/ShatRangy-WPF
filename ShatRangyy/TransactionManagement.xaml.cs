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
    public partial class TransactionManagement : UserControl
    {
        public TransactionManagement()
        {
            InitializeComponent();
        }

        #region Varibels And Objects

        PersianCalendar PersianCalendar = new PersianCalendar();
        Transaction_BL Transaction_BL = new Transaction_BL();
        Account_BL Account_BL = new Account_BL();
        Account CurrentAccount;
        public enum FilterType
        {
            Id, AccountSideName, Description, Recived, Payment, Date
        }
        FilterType _FilterType;
        string AccountSideName, Description, CurentDate, TextSearchContent;
        int Id , AccountSideId, Year, Month, Day, SearchYear, SearchMonth, SearchDay;
        decimal Recived, Payment, FromAmount, ToAmount;
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
            if (txtAccountSideName.Text.Length < 3 && !String.IsNullOrEmpty(txtAccountSideName.Text))
            {
                _ShowMessage("نام طرف حساب باید بیشتر از 2 حرف باشد .", MessageBox_.enumType.Warning);
                txtAccountSideName.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtPayment.Text) && String.IsNullOrEmpty(txtRecived.Text))
            {
                _ShowMessage("لطفا مبلغ پرداختی یا دریافتی را وارد کنید .", MessageBox_.enumType.Warning);
                if (String.IsNullOrEmpty(txtRecived.Text))
                {
                    txtRecived.Focus();
                }
                if (String.IsNullOrEmpty(txtPayment.Text))
                {
                    txtPayment.Focus();
                }
                return false;
            }
            if (txtRecived.Text.Length < 4 && !String.IsNullOrEmpty(txtRecived.Text))
            {
                _ShowMessage("مبلغ دریافتی باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
                txtRecived.Focus();
                return false;
            }
            if (txtPayment.Text.Length < 4 && !String.IsNullOrEmpty(txtPayment.Text))
            {
                _ShowMessage("مبلغ پرداختی باید بیشتر از 3 رقم باشد .", MessageBox_.enumType.Warning);
                txtPayment.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtShowYear.Text) ||
                String.IsNullOrEmpty(txtShowMonth.Text) ||
                String.IsNullOrEmpty(txtShowDay.Text))
            {
                _ShowMessage("لطفا تاریخ تراکنش را اصلاح کنید .", MessageBox_.enumType.Warning);
                txtShowYear.Focus();
                return false;
            }

            if (Transaction_BL.Exist(AccountSideName, CurentDate, Payment, Recived) && _Update != true)
            {
                _ShowMessage("این تراکنش قبلا ثبت شده .", MessageBox_.enumType.Warning);
                txtAccountSideName.Focus();
                return false;
            }
            return true;
        }
        void GetParameters()
        {
            if (String.IsNullOrEmpty(txtAccountSideName.Text))
            {
                AccountSideId = 0;
            }
            AccountSideName = txtAccountSideName.Text;
            Description = txtDesciption.Text;
            if (!String.IsNullOrEmpty(txtRecived.Text))
            {
                Recived = decimal.Parse(txtRecived.Text.Replace(",", ""));
            }
            if (!String.IsNullOrEmpty(txtPayment.Text))
            {
                Payment = decimal.Parse(txtPayment.Text.Replace(",", ""));
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
            TextSearchContent = txtSearch.Text;
            if (!String.IsNullOrEmpty(txtSearchFromAmount.Text))
            {
                FromAmount = decimal.Parse(txtSearchFromAmount.Text.Replace(",", ""));
            }
            else
            {
                FromAmount = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromAmount.Text))
            {
                ToAmount = decimal.Parse(txtSearchToAmount.Text.Replace(",", ""));
            }
            else
            {
                ToAmount = 0;
            }
            CurentDate = $"{Year}/{Month}/{Day}";
        }
        void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        void Clear()
        {
            txtAccountSideName.Text = String.Empty;
            txtDesciption.Text = String.Empty;
            txtPayment.Text = String.Empty;
            txtRecived.Text = String.Empty;
            txtSearch.Text = String.Empty;
            txtSearchFromAmount.Text = String.Empty;
            txtSearchToAmount.Text = String.Empty;
            GetParameters();
            FilterDataGrid(SearchYear, SearchMonth, SearchDay);
            ClearDataGrid();
            Account_Selector.Visibility = Visibility.Hidden;
            _Update = false;
            txtAccountSideName.Focus();
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
        void Insert()
        {
            GetParameters();
            if (ParametersValidation())
            {
                Transaction transaction = new Transaction();
                #region GetData
                transaction.AccountSideName = AccountSideName;
                transaction.Description = Description;
                transaction.Date = CurentDate;
                transaction.Payment = Payment;
                transaction.Recived = Recived;
                transaction.Year = Year;
                transaction.Month = Month;
                transaction.Day = Day;
                transaction.AccountSideId = AccountSideId;
                #endregion
                if (_Update)
                {
                    //Update
                    transaction.ID = Id;
                    if (Transaction_BL.UpdateTransaction(transaction))
                    {
                        _ShowMessage("تراکنش با موفقیت ویرایش شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("تراکنش ویرایش نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtAccountSideName.Focus();
                    }
                }
                else
                {
                    //Insert
                    if (Transaction_BL.InsertTransaction(transaction))
                    {
                        _ShowMessage("تراکنش باموفقیت ذخیره شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("تراکنش ذخیره نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtAccountSideName.Focus();
                    }
                }
            }
        }
        void Update()
        {
            Transaction transaction = new Transaction();
            transaction = Transaction_BL.GetTransactionById(Id);
            txtAccountSideName.Text = transaction.AccountSideName;
            txtDesciption.Text = transaction.Description;
            txtPayment.Text = ThreeDigitSeparator(transaction.Payment.ToString());
            txtRecived.Text = ThreeDigitSeparator(transaction.Recived.ToString());
            txtShowYear.Text = transaction.Year.ToString();
            txtShowMonth.Text = transaction.Month.ToString();
            txtShowDay.Text = transaction.Day.ToString();
            AccountSideId = transaction.AccountSideId;
            _Update = true;
            txtAccountSideName.Focus();
        }
        void Delete()
        {
            QuestionBox_ questionBox_ = new QuestionBox_();
            questionBox_.Content = "آیا می خواهید این تراکنش را حذف کنید .";
            questionBox_.ShowDialog();
            if (questionBox_.Ok)
            {
                if (Transaction_BL.DeleteTransaction(Id))
                {
                    _ShowMessage("تراکنش با موفقیت حذف شد .", MessageBox_.enumType.Success);
                    ClearDataGrid();
                }
                else
                {
                    _ShowMessage("تراکنش حذف نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                }
            }
        }
        void Search()
        {
            FilterBox filterBox = new FilterBox();
            filterBox.LabelContent1 = "سریال";
            filterBox.LabelContent2 = "نام طرف حساب";
            filterBox.LabelContent3 = "شرح تراکنش";
            filterBox.LabelContent4 = "دریافتی";
            filterBox.LabelContent5 = "پرداختی";
            filterBox.LabelContent6 = "تاریخ";
            filterBox.LabelContent7 = "....";
            filterBox.LabelContent8 = "....";
            filterBox.LabelContent9 = "....";
            if (_FilterType == FilterType.Date)
            {
                filterBox.CheckBox6.IsChecked = true;
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
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام طرف حساب");
                    _FilterType = FilterType.AccountSideName;
                    txtSearch.Focus();
                    break;
                case FilterBox.SelectedIndex.Index3:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس شرح تراکنش");
                    _FilterType = FilterType.Description;
                    txtSearch.Focus();
                    break;
                case FilterBox.SelectedIndex.Index4:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchFromAmount, "از مبلغ");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchToAmount, "تا مبلغ");
                    _FilterType = FilterType.Recived;
                    txtSearchFromAmount.Focus();
                    break;
                case FilterBox.SelectedIndex.Index5:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchFromAmount, "از مبلغ");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearchToAmount, "تا مبلغ");
                    _FilterType = FilterType.Payment;
                    txtSearchFromAmount.Focus();
                    break;
                case FilterBox.SelectedIndex.Index6:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو");
                    _FilterType = FilterType.Date;
                    txtSearch.Focus();
                    break;
                default:
                    break;
            }
        }
        void FilterDataGrid(int year, int month, int day)
        {
            DG.ItemsSource = Transaction_BL.GetTransactionsByDate(year, year, month, month, day, day);
            ClearDataGrid();
        }
        void FilterDataGrid(FilterType filterType, decimal fromAmount, decimal toAmount)
        {
            var _list = Transaction_BL.GetTransactionsByDate(SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Recived:
                    DG.ItemsSource = Transaction_BL.GetTransactionsByRecivedAmount(_list, fromAmount, toAmount);
                    break;
                case FilterType.Payment:
                    DG.ItemsSource = Transaction_BL.GetTransactionsByPaymentAmount(_list, fromAmount, toAmount);
                    break;
                default:
                    break;
            }
            ClearDataGrid();
        }
        void FilterDataGrid(FilterType filterType, int id, string accountSideName,
            string description)
        {
            var _list = Transaction_BL.GetTransactionsByDate(SearchYear, SearchYear
                , SearchMonth, SearchMonth, SearchDay, SearchDay);
            switch (filterType)
            {
                case FilterType.Id:
                    DG.ItemsSource = null;
                    DG.Items.Add(Transaction_BL.GetTransactionById(id));
                    break;
                case FilterType.AccountSideName:
                    DG.ItemsSource = Transaction_BL.GetTransactionsByAccountSide(null, accountSideName);
                    break;
                case FilterType.Description:
                    DG.ItemsSource = Transaction_BL.GetTransactionsByDescription(_list, description);
                    break;
                case FilterType.Date:
                    DG.ItemsSource = Transaction_BL.GetTransactionsByNameOrDescription(accountSideName);
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

        #region ---UserControl---

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
                case Key.Delete:
                    Delete();
                    break;
                case Key.F5:
                    Search();
                    break;
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearchYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchByMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtSearchByDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            txtShowYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtShowMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtShowDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            _FilterType = FilterType.Date;
            Clear();
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

        #endregion

        #region ---TextChanged---

        private void txtPayment_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPayment.Text = ThreeDigitSeparator(txtPayment.Text);
            txtPayment.SelectionStart = txtPayment.Text.Length;
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

        private void txtAccountSideName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Visible;
            Account_Selector.DGV.ItemsSource = Account_BL.GetAccountsByName(txtAccountSideName.Text);
            if (Account_Selector.DGV.Items.Count != 0)
            {
                Account_Selector.DGV.SelectedIndex = 0;
            }
            if (!String.IsNullOrEmpty(txtAccountSideName.Text))
            {
                if (Account_Selector.DGV.SelectedItem != null)
                {
                    CurrentAccount = Account_Selector.account;
                }
                else
                {
                    CurrentAccount = null;
                    AccountSideId = 0;
                }
            }
            else
            {
                CurrentAccount = null;
                AccountSideId = 0;
            }
        }

        private void txtRecived_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtRecived.Text = ThreeDigitSeparator(txtRecived.Text);
            txtRecived.SelectionStart = txtRecived.Text.Length;
        }

        #endregion

        #region ---KeyDown---

        private void txtDesciption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtRecived.Focus();
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
                    case FilterType.AccountSideName:
                        FilterDataGrid(FilterType.AccountSideName, 0, TextSearchContent, String.Empty);
                        break;
                    case FilterType.Description:
                        FilterDataGrid(FilterType.Description, 0, String.Empty, TextSearchContent);
                        break;
                    case FilterType.Date:
                        FilterDataGrid(FilterType.Date, 0, TextSearchContent, String.Empty);
                        break;
                }
            }
        }

        private void txtAccountSideName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CurrentAccount != null)
                {
                    txtAccountSideName.Text = CurrentAccount.AccountName;
                }
                Account_Selector.Visibility = Visibility.Hidden;
                txtDesciption.Focus();
            }
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                AddAccount addAccount = new AddAccount();
                addAccount.ShowDialog();
                txtAccountSideName.Focus();
            }
        }

        private void txtRecived_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPayment.Focus();
            }
        }

        private void txtPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
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

        private void txtSearchToAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                switch (_FilterType)
                {
                    case FilterType.Recived:
                        FilterDataGrid(FilterType.Recived, FromAmount, ToAmount);
                        break;
                    case FilterType.Payment:
                        FilterDataGrid(FilterType.Payment, FromAmount, ToAmount);
                        break;
                }
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

        private void txtSearchByMonth_KeyDown(object sender, KeyEventArgs e)
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

        #region ---DataGrid---

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                Transaction obj = DG.SelectedItem as Transaction;
                Id = obj.ID;
                //Transaction transaction = new Transaction();
                //transaction = obj;
                if (obj.AccountSideId != 0)
                {
                    decimal _credit, _debt;
                    _credit = Account_BL.GetAccountById(obj.AccountSideId).Credit;
                    _debt = Account_BL.GetAccountById(obj.AccountSideId).Debt;
                    lbCredit_Value.Content = _credit.ToString("#,#");
                    lbDebt_Value.Content = _debt.ToString("#,#");
                }
                else
                {
                    lbCredit_Value.Content = 0000;
                    lbDebt_Value.Content = 0000;
                }
            }
        }

        #endregion

        #region ---Lost Focus---
        private void txtAccountSideName_LostFocus(object sender, RoutedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Hidden;
        }
        #endregion

        #region ---PreviewTextInput---

        private void TextBoxs_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #endregion
    }
}
