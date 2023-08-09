using System;
using System.Windows.Input;
using System.Windows;
using VeiwModels;
using Business;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using ShatRangyy.CustomControls;

namespace ShatRangyy
{
    public partial class AccountsManagement : UserControl
    {
        public AccountsManagement()
        {
            InitializeComponent();
        }

        #region Varibles And Objects

        Account_BL Account_BL = new Account_BL();
        AccountGroup_BL AccountGroup_BL = new AccountGroup_BL();
        AccountGroup AccountGroup;
        public enum FilterType
        {
            Id, Name, Group, PhoneNumber, Address, Note, All
        }
        FilterType _FilterType;
        string AccountName, GroupName, PhoneNumber, Address, Note, TextSearchContent;
        int Id;
        decimal AccountDebt, AccountCredit;
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
        public bool ParametersValidation()
        {
            if (String.IsNullOrEmpty(txtAccountName.Text))
            {
                _ShowMessage("لطفا نام حساب را وارد کنید .", MessageBox_.enumType.Warning);
                txtAccountName.Focus();
                return false;
            }
            if (txtAccountName.Text.Length < 3)
            {
                _ShowMessage("نام حساب باید بیشتر از 2 حرف باشد .", MessageBox_.enumType.Warning);
                txtAccountName.Focus();
                return false;
            }
            return true;
        }
        public void GetParameters()
        {
            AccountName = txtAccountName.Text;
            GroupName = txtAccountgroup.Text;
            PhoneNumber = txtPhoneNumber.Text;
            Address = txtAddress.Text;
            Note = txtNote.Text;
            TextSearchContent = txtSearch.Text;
        }
        public void Clear()
        {
            txtAccountName.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtPhoneNumber.Text = String.Empty;
            txtNote.Text = String.Empty;
            txtAccountgroup.Text = String.Empty;
            FilterDataGrid(FilterType.All, 0, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            GroupSelectorBorder.Visibility = Visibility.Hidden;
            txtAccountName.Focus();
        }
        public void ClearDataGridIndex()
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
        public void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        public void Insert()
        {
            GetParameters();
            if (ParametersValidation())
            {
                Account account = new Account();
                #region Get Data
                account.AccountName = AccountName;
                account.GroupName = GroupName;
                account.PhoneNumber = PhoneNumber;
                account.Address = Address;
                account.Note = Note;
                #endregion
                if (_Update)
                {
                    //Update
                    account.ID = Id;
                    account.Debt = AccountDebt;
                    account.Credit = AccountCredit;
                    if (Account_BL.UpdateAccount(account))
                    {
                        _ShowMessage("حساب با موفقیت ویرایش شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("حساب ویرایش نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtAccountName.Focus();
                    }
                }
                else
                {
                    //Insert
                    if (Account_BL.InsertAccount(account))
                    {
                        _ShowMessage("حساب با موفقیت ذخیره شد .", MessageBox_.enumType.Success);
                        Clear();
                    }
                    else
                    {
                        _ShowMessage("حساب ذخیره نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                        txtAccountName.Focus();
                    }
                }
            }
        }
        public void Update()
        {
            Account account = new Account();
            account = Account_BL.GetAccountById(Id);
            txtAccountName.Text = account.AccountName;
            txtAccountgroup.Text = account.GroupName;
            txtAddress.Text = account.Address;
            txtPhoneNumber.Text = account.PhoneNumber;
            txtNote.Text = account.Note;
            AccountCredit = account.Credit;
            AccountDebt = account.Debt;
            _Update = true;
            txtAccountName.Focus();
        }
        public void Delete()
        {
            QuestionBox_ questionBox_ = new QuestionBox_();
            questionBox_.Content = "آیا می خواهید این حساب را حذف کنید ؟";
            questionBox_.ShowDialog();
            if (questionBox_.Ok)
            {
                if (Account_BL.DeleteAccount(Id))
                {
                    _ShowMessage("حساب با موفقیت حذف شد .", MessageBox_.enumType.Success);
                    Clear();
                }
                else
                {
                    _ShowMessage("حساب حذف نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                }
            }
        }
        public void Search()
        {
            FilterBox filterBox = new FilterBox();
            filterBox.LabelContent1 = "سریال";
            filterBox.LabelContent2 = "نام حساب";
            filterBox.LabelContent3 = "نام گروه";
            filterBox.LabelContent4 = "شماره موبایل";
            filterBox.LabelContent5 = "آدرس";
            filterBox.LabelContent6 = "یاداشت";
            filterBox.LabelContent7 = "....";
            filterBox.LabelContent8 = "....";
            filterBox.LabelContent9 = "....";
            filterBox.ShowDialog();
            switch (filterBox.selectedIndex)
            {
                case FilterBox.SelectedIndex.Index1:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس سریال");
                    _FilterType = FilterType.Id;
                    break;
                case FilterBox.SelectedIndex.Index2:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام حساب");
                    _FilterType = FilterType.Name;
                    break;
                case FilterBox.SelectedIndex.Index3:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نام گروه");
                    _FilterType = FilterType.Group;
                    break;
                case FilterBox.SelectedIndex.Index4:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس شماره موبایل");
                    _FilterType = FilterType.PhoneNumber;
                    break;
                case FilterBox.SelectedIndex.Index5:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس آدرس");
                    _FilterType = FilterType.Address;
                    break;
                case FilterBox.SelectedIndex.Index6:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو براساس نوع یاداشت");
                    _FilterType = FilterType.Note;
                    break;
                default:
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txtSearch, "جستوجو");
                    break;
            }
        }
        public void FilterDataGrid(FilterType filterType, int id, string name
            , string group, string phoneNumber, string address, string note)
        {
            switch (filterType)
            {
                case FilterType.Id:
                    DGV.ItemsSource = null;
                    DGV.Items.Add(Account_BL.GetAccountById(id));
                    break;
                case FilterType.Name:
                    DGV.ItemsSource = Account_BL.GetAccountsByName(name);
                    break;
                case FilterType.Group:
                    DGV.ItemsSource = Account_BL.GetAccountsByGroupName(group);
                    break;
                case FilterType.PhoneNumber:
                    DGV.ItemsSource = Account_BL.GetAccountsByPhoneNumber(phoneNumber);
                    break;
                case FilterType.Address:
                    DGV.ItemsSource = Account_BL.GetAccountsByAddress(address);
                    break;
                case FilterType.Note:
                    DGV.ItemsSource = Account_BL.GetAccountsByNote(note);
                    break;
                case FilterType.All:
                    DGV.ItemsSource = Account_BL.GetAll();
                    break;
            }
            ClearDataGridIndex();   
        }
        public void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }

        #endregion

        #region Events

        #region ---Buttons Click---

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

        #region ---KeyDown---

        private void txtAccountName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAccountgroup.Focus();
            }
        }

        private void txtAccountgroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!String.IsNullOrEmpty(txtAccountgroup.Text))
                {
                    if (DGV_GroupName.SelectedItem != null)
                    {
                        txtAccountgroup.Text = AccountGroup.GroupName;
                        txtPhoneNumber.Focus();
                    }
                    else
                    {
                        AccountGroup accountGroup = new AccountGroup();
                        accountGroup.GroupName = txtAccountgroup.Text;
                        if (AccountGroup_BL.InsertGroup(accountGroup))
                        {
                            _ShowMessage("گروه حساب با موفقیت ثبت شد .", MessageBox_.enumType.Success);
                            AccountGroup = accountGroup;
                            txtPhoneNumber.Focus();
                        }
                        else
                        {
                            _ShowMessage("گروه تبت نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                            txtAccountgroup.Focus();
                        }
                    }
                }
                else
                {
                    AccountGroup = null;
                }
                GroupSelectorBorder.Visibility = Visibility.Hidden;
            }
        }

        private void txtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddress.Focus();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtNote.Focus();
            }
        }

        private void txtNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSave.Focus();
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
                            FilterDataGrid(_FilterType, int.Parse(TextSearchContent), String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty);
                        }
                        catch (Exception)
                        {
                            DGV.ItemsSource = null;
                        }
                        break;
                    case FilterType.Name:
                        FilterDataGrid(_FilterType, 0, TextSearchContent,
                            String.Empty, String.Empty, String.Empty, String.Empty);
                        break;
                    case FilterType.Group:
                        FilterDataGrid(_FilterType, 0, String.Empty,
                            TextSearchContent, String.Empty, String.Empty, String.Empty);
                        break;
                    case FilterType.PhoneNumber:
                        FilterDataGrid(_FilterType, 0, String.Empty,
                            String.Empty, TextSearchContent, String.Empty, String.Empty);
                        break;
                    case FilterType.Address:
                        FilterDataGrid(_FilterType, 0, String.Empty,
                            String.Empty, String.Empty, TextSearchContent, String.Empty);
                        break;
                    case FilterType.Note:
                        FilterDataGrid(_FilterType, 0, String.Empty,
                            String.Empty, String.Empty, String.Empty, TextSearchContent);
                        break;
                }
            }
        }

        #endregion

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

        #region ---Data Grid---

        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV.SelectedItem != null)
            {
                Account obj = DGV.SelectedItem as Account;
                Id = obj.ID;
            }
        }

        private void DGV_GroupName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGV_GroupName.SelectedItem != null)
            {
                AccountGroup obj = DGV_GroupName.SelectedItem as AccountGroup;
                AccountGroup = obj;
            }
        }

        private void DGV_GroupName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV_GroupName.ItemsSource != null)
            {
                AccountGroup obj = DGV_GroupName.SelectedItem as AccountGroup;
                AccountGroup =  obj;
            }
        }

        #endregion

        #region ---Preview Text Input---

        private void AllTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #region ---Text Changed---

        private void txtAccountgroup_TextChanged(object sender, TextChangedEventArgs e)
        {
            GroupSelectorBorder.Visibility = Visibility.Visible;
            DGV_GroupName.ItemsSource = AccountGroup_BL.GetAccountGroupsByName(txtAccountgroup.Text);
            if (DGV_GroupName.Items.Count != 0)
            {
                DGV_GroupName.SelectedIndex = 0;
            }
            if (DGV_GroupName.Items.Count != 0)
            {
                DGV_GroupName.SelectedIndex = 0;
            }
        }

        #endregion

        #region ---Lost Focus---
        private void txtAccountgroup_LostFocus(object sender, RoutedEventArgs e)
        {
            GroupSelectorBorder.Visibility = Visibility.Hidden;
        }
        #endregion

        #endregion
    }
}
