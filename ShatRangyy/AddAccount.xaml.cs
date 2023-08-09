using Business;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeiwModels;

namespace ShatRangyy
{
    public partial class AddAccount : Window
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        #region Varibles And Objects
        Account_BL Account_BL = new Account_BL();
        AccountGroup_BL AccountGroup_BL = new AccountGroup_BL();
        public Account Account;
        public AccountGroup CurrentGroup;
        string AccountName, GroupName, PhoneNumber, Address;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        #endregion

        #region Function
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
        }
        public void Clear()
        {
            txtAccountName.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtPhoneNumber.Text = String.Empty;
            txtAccountgroup.Text = String.Empty;
            txtAccountName.Focus();
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
                #endregion
                //Insert
                if (Account_BL.InsertAccount(account))
                {
                    Account = account;
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
        public void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }
        #endregion

        private void txtAccountName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAccountgroup.Focus();
            }
        }

        private void txtAccountgroup_LostFocus(object sender, RoutedEventArgs e)
        {
            Group_Selector.Visibility = Visibility.Hidden;
        }

        private void txtAccountgroup_TextChanged(object sender, TextChangedEventArgs e)
        {
            Group_Selector.Visibility = Visibility.Visible;
            Group_Selector.DGV.ItemsSource = AccountGroup_BL.GetAccountGroupsByName(txtAccountgroup.Text);
            if (Group_Selector.DGV.Items.Count != 0)
            {
                Group_Selector.DGV.SelectedIndex = 0;
            }
            if (!String.IsNullOrEmpty(txtAccountgroup.Text))
            {
                if (Group_Selector.DGV.SelectedItem != null)
                {
                    CurrentGroup = Group_Selector.Group;
                }
                else
                {
                    CurrentGroup = null;
                }
            }
            else
            {
                CurrentGroup = null;
            }
        }

        private void txtAccountgroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CurrentGroup != null)
                {
                    txtAccountgroup.Text = CurrentGroup.GroupName;
                }
                Group_Selector.Visibility = Visibility.Hidden;
                txtPhoneNumber.Focus();
            }
        }

        private void txtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddress.Focus();
            }
        }

        private void txtPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSave.Focus();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Insert();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtAccountName.Focus();
        }
    }
}
