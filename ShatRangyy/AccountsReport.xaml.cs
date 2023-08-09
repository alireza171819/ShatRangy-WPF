using Business;
using ShatRangyy.CustomControls;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeiwModels;

namespace ShatRangyy
{
    public partial class AccountsReport : UserControl
    {
        public AccountsReport()
        {
            InitializeComponent();
        }

        #region Object & Varible

        Account_BL Account_BL = new Account_BL();
        decimal CurrentAccountDebt, CurrentAccountCredit, TotalDebt, TotalCredit,
         SearchFromAmount, SearchToAmount;
        public enum FilterType
        {
            Id, AccountName, PhoneNumber, GroupName, Debt, Credit, All
        }
        FilterType _FilterType;
        int Id;
        bool _Update = false;
        string ContentSearchTextBox, AccountName, GroupName, PhoneNumber, Address, Note;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        #endregion

        #region Functions
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        void GetParameters()
        {
            ContentSearchTextBox = txtSearch.Text;
            if (!String.IsNullOrEmpty(txtSearchFromAmount.Text))
            {
                SearchFromAmount = decimal.Parse(txtSearchFromAmount.Text.Replace(",",""));
            }
            else
            {
                SearchFromAmount = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToAmount.Text))
            {
                SearchToAmount = decimal.Parse(txtSearchToAmount.Text.Replace(",", ""));
            }
            else
            {
                SearchToAmount= 0;
            }
            if (!String.IsNullOrEmpty(txtCredit.Text))
            {
                CurrentAccountCredit = decimal.Parse(txtCredit.Text.Replace(",", ""));
            }
            else
            {
                CurrentAccountCredit = 0;
            }
            if (!String.IsNullOrEmpty(txtDebt.Text))
            {
                CurrentAccountDebt = decimal.Parse(txtDebt.Text.Replace(",", ""));
            }
            else
            {
                CurrentAccountDebt = 0;
            }
        }
        bool ParametersValidaition()
        {
            if (DGV.SelectedItem == null)
            {
                _ShowMessage("شما هیچ حسابی انتخاب نکردید ." , MessageBox_.enumType.Warning);
                txtDebt.Focus();
                return false;
            }
            if (_Update == false)
            {
                _ShowMessage("لطفا روی دکمه ویرایش کلیک کنید .", MessageBox_.enumType.Warning);
                return false;
            }
            return true;
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
        void Clear()
        {
            txtCredit.Clear();
            txtDebt.Clear();
            txtDebt.Focus();
            GetParameters();
            FilterDataGrid(_FilterType, Id, ContentSearchTextBox, SearchFromAmount, SearchToAmount);
            ClearDataGridIndex();
            _Update = false;
        }
        void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        void Update()
        {
            Account account = new Account();
            account = Account_BL.GetAccountById(Id);
            txtCredit.Text = account.Credit.ToString();
            txtDebt.Text = account.Debt.ToString();
            AccountName = account.AccountName;
            Address = account.Address;
            PhoneNumber = account.PhoneNumber;
            Note = account.Note;
            GroupName = account.GroupName;
            _Update = true;
            txtDebt.Focus();
        }
        void Save()
        {
            GetParameters();
            if (ParametersValidaition())
            {
                Account account = new Account();
                account.ID = Id;
                account.AccountName = AccountName;
                account.Address = Address;
                account.PhoneNumber = PhoneNumber;
                account.GroupName = GroupName;
                account.Note = Note;
                account.Credit = CurrentAccountCredit;
                account.Debt = CurrentAccountDebt;
                if (Account_BL.UpdateAccount(account))
                {
                    _ShowMessage("عملیات با موفقیت انجام شد .", MessageBox_.enumType.Success);
                    Clear();
                }
                else
                {
                    _ShowMessage("عملیات با خطا مواجه شد لطفا دوبار امتحان کنید .", MessageBox_.enumType.Erorr);
                    txtDebt.Focus();
                }
            }
        }
        void Search()
        {
            FilterBox filterBox = new FilterBox();
            filterBox.LabelContent1 = "سریال";
            filterBox.LabelContent2 = "نام حساب";
            filterBox.LabelContent3 = "شماره موبایل";
            filterBox.LabelContent4 = "نام گروه";
            filterBox.LabelContent5 = "بدهکاری";
            filterBox.LabelContent6 = "بستانکاری";
            filterBox.LabelContent7 = "....";
            filterBox.LabelContent8 = "....";
            filterBox.LabelContent9 = "....";
            filterBox.CheckBox2.IsChecked = true;
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
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام حساب");
                    _FilterType = FilterType.AccountName;
                    break;
                case FilterBox.SelectedIndex.Index3:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس شماره موبایل");
                    _FilterType = FilterType.PhoneNumber;
                    break;
                case FilterBox.SelectedIndex.Index4:
                    txtSearchFromAmount.Visibility = Visibility.Hidden;
                    txtSearchToAmount.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس گروه");
                    _FilterType = FilterType.GroupName;
                    break;
                case FilterBox.SelectedIndex.Index5:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    _FilterType = FilterType.Debt;
                    break;
                case FilterBox.SelectedIndex.Index6:
                    txtSearchFromAmount.Visibility = Visibility.Visible;
                    txtSearchToAmount.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    _FilterType = FilterType.Credit;
                    break;
                default:
                    break;
            }
        }
        void FilterDataGrid(FilterType filterType, int id, string input, decimal fromAmount, decimal toAmount)
        {
            GetParameters();
            switch (filterType)
            {
                case FilterType.Id:
                    try
                    {
                        DGV.ItemsSource = null;
                        DGV.Items.Add(Account_BL.GetAccountById(id));
                    }
                    catch (Exception)
                    {
                        DGV.ItemsSource = null;
                    }
                    break;
                case FilterType.AccountName:
                    DGV.ItemsSource = Account_BL.GetAccountsByName(input);
                    break;
                case FilterType.PhoneNumber:
                    DGV.ItemsSource = Account_BL.GetAccountsByPhoneNumber(input);
                    break;
                case FilterType.GroupName:
                    DGV.ItemsSource = Account_BL.GetAccountsByGroupName(input);
                    break;
                case FilterType.Debt:
                    DGV.ItemsSource = Account_BL.GetAccountsByDebt(fromAmount, toAmount);
                    break;
                case FilterType.Credit:
                    DGV.ItemsSource = Account_BL.GetAccountsByCredit(fromAmount, toAmount);
                    break;
                case FilterType.All:
                    DGV.ItemsSource = Account_BL.GetAll();
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
            _FilterType = FilterType.All;
            Clear();
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    Update();
                    break;
                case Key.F1:
                    Save();
                    break;
                case Key.F5:
                    Search();
                    break;
                case Key.Escape:
                    Close();
                    break;
            }
        }

        #endregion

        #region ---Text Change---

        private void txtCredit_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCredit.Text = ThreeDigitSeparator(txtCredit.Text);
            txtCredit.SelectionStart = txtCredit.Text.Length;
        }

        private void txtDebt_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtDebt.Text = ThreeDigitSeparator(txtDebt.Text);
            txtDebt.SelectionStart = txtDebt.Text.Length;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetParameters();
            switch (_FilterType)
            {
                case FilterType.Id:
                    try
                    {
                        FilterDataGrid(FilterType.Id, int.Parse(ContentSearchTextBox), String.Empty, 0, 0);
                    }
                    catch (Exception)
                    {
                        DGV.ItemsSource = null;
                    }
                    break;
                case FilterType.AccountName:
                    FilterDataGrid(FilterType.AccountName, 0, ContentSearchTextBox, 0, 0);
                    break;
                case FilterType.PhoneNumber:
                    FilterDataGrid(FilterType.AccountName, 0, ContentSearchTextBox, 0, 0);
                    break;
                case FilterType.GroupName:
                    FilterDataGrid(FilterType.AccountName, 0, ContentSearchTextBox, 0, 0);
                    break;
            }
        }

        #endregion

        #region ---Key Down---

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSave.Focus();
            }
        }

        private void txtSearchToAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_FilterType == FilterType.Debt)
                {
                    FilterDataGrid(FilterType.Debt, 0, String.Empty, SearchFromAmount, SearchToAmount);
                }
                if (_FilterType == FilterType.Credit)
                {
                    FilterDataGrid(FilterType.Credit, 0, String.Empty, SearchFromAmount, SearchToAmount);
                }
            }
        }

        private void txtDebt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtCredit.Focus();
            }
        }

        private void txtSearchFromAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtSearchToAmount.Focus();
            }
        }

        #endregion

        #region ---Data Grid---

        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV.SelectedItem != null)
            {
                Account obj = DGV.SelectedItem as Account;
                Id = obj.ID;
                CurrentAccountCredit = obj.Credit;
                CurrentAccountDebt = obj.Debt;
                txtCredit.Text = obj.Credit.ToString();
                txtDebt.Text = obj.Debt.ToString();
            }
        }

        private void DGV_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (DGV.Items != null)
            {
                TotalCredit = 0;
                TotalDebt = 0;
                foreach (var item in DGV.Items)
                {
                    Account obj = item as Account;
                    TotalCredit += obj.Credit;
                    TotalDebt += obj.Debt;
                }
            }
            if (DGV.ItemsSource != null)
            {
                lbTotalCredit.Content = TotalCredit.ToString("#,#");
                lbTotalDebt.Content = TotalDebt.ToString("#,#");
            }
            else
            {
                lbTotalCredit.Content = 0000;
                lbTotalDebt.Content = 0000;
            }
            lbCountRows.Content = $"تعداد ردیف ها : {DGV.Items.Count.ToString()}";
        }

        #endregion

        #region ---Click---

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        #endregion

        #region ---Preview Text Input---

        private void AllTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #endregion

    }
}
