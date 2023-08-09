using Business;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeiwModels;

namespace ShatRangyy.CustomControls
{
    public partial class Invoice : Window
    {
        public Invoice()
        {
            InitializeComponent();
        }

        #region Varible & Object
        PersianCalendar persianCalendar = new PersianCalendar();
        SellDocument_BL SellDocument_BL = new SellDocument_BL();
        Account_BL Account_BL = new Account_BL();
        Item_BL Item_BL = new Item_BL();
        Account CurrentAccount;
        Item CurrentItem;
        string CurrntDate, BuyerName, ItemName, PayType;
        decimal Price, TotalPrice, Costs, SumTotalPriceColumns;
        int BuyerAccountId, ItemId, Number, Year, Month, Day;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        #endregion

        #region Function
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        public bool ParametersVlidation()
        {
            if (txtBuyerName.Text == String.Empty && PayType == "نسیه")
            {
                _ShowMessage("لطفا نام خریدار را وارد کنید .", MessageBox_.enumType.Warning);
                txtBuyerName.Focus();
                return false;
            }
            else if (txtItemName.Text == String.Empty)
            {
                _ShowMessage("لطفا نام کالا را وارد کنید .", MessageBox_.enumType.Warning);
                txtItemName.Focus();
                return false;
            }
            else if (txtPrice.Text == String.Empty)
            {
                _ShowMessage("لطفا قیمت را وارد کنید .", MessageBox_.enumType.Warning);
                txtPrice.Focus();
                return false;
            }
            else if (txtPrice.Text.Length < 3)
            {
                _ShowMessage("مبلغ قیمت کالا باید بیشتر از سه رقم باشد .", MessageBox_.enumType.Warning);
                txtPrice.Focus();
                return false;
            }
            else if (txtTotalPrice.Text == String.Empty)
            {
                _ShowMessage("لطفا بهای کل را وارد کنید .", MessageBox_.enumType.Warning);
                txtTotalPrice.Focus();
                return false;
            }
            else if (txtTotalPrice.Text.Length < 3)
            {
                _ShowMessage("مبلغ بهای کل باید بیشتر از سه رقم باشد .", MessageBox_.enumType.Warning);
                txtTotalPrice.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        public void GetParameters()
        {
            if (String.IsNullOrEmpty(txtBuyerName.Text))
            {
                BuyerAccountId = 0;
            }
            BuyerName = txtBuyerName.Text;
            ItemName = txtItemName.Text;
            if (!String.IsNullOrEmpty(txtNumber.Text))
            {
                Number = int.Parse(txtNumber.Text);
            }
            if (!String.IsNullOrEmpty(txtPrice.Text))
            {
                Price = long.Parse(txtPrice.Text.Replace(",", ""));
            }
            if (!String.IsNullOrEmpty(txtTotalPrice.Text))
            {
                TotalPrice = long.Parse(txtTotalPrice.Text.Replace(",", ""));
            }
            if (SellCash.IsChecked == true)
            {
                PayType = "نقدی";
            }
            else 
            {
                PayType = "نسیه";
            }
            Year = persianCalendar.GetYear(DateTime.Now);
            Month = persianCalendar.GetMonth(DateTime.Now);
            Day = persianCalendar.GetDayOfMonth(DateTime.Now);
            CurrntDate = $"{Year}/{Month}/{Day}";
        }
        public void ClearDataGridIndex()
        {
            if (DGV.Items.Count > 1)
            {
                DGV.SelectedIndex = DGV.Items.Count - 1;
                for (int i = 0; i < DGV.Items.Count; i++)
                {
                    SellDocument obj = DGV.Items[i] as SellDocument;
                    SumTotalPriceColumns += obj.TotalPrice;
                }
                lbTotal.Content = SumTotalPriceColumns.ToString("#,#");
            }
            else if (DGV.Items.Count == 1)
            {
                DGV.SelectedIndex = 1;
                SellDocument obj = DGV.SelectedItem as SellDocument;
                lbTotal.Content = obj.TotalPrice.ToString("#,#");
            }
            else
            {
                lbTotal.Content = "0";
            }
        }
        public void Clear()
        {
            txtItemName.Text = String.Empty;
            txtNumber.Text = String.Empty;
            txtPrice.Text = String.Empty;
            txtTotalPrice.Text = String.Empty;
            Account_Selector.Visibility = Visibility.Hidden;
            Item_Selector.Visibility = Visibility.Hidden;
            ClearDataGridIndex();
            txtItemName.Focus();
        }
        public void Insert()
        {
            GetParameters();
            if (ParametersVlidation())
            {
                //Insert
                SellDocument document = new SellDocument();
                #region GetData
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
                if (SellDocument_BL.InsertSellDocument(document))
                {
                    _ShowMessage("سند با موفقیت ثبت شد .", MessageBox_.enumType.Success);
                    DGV.Items.Add(document);
                    Clear();
                }
                else
                {
                    _ShowMessage("سند ثبت نشد لطفا دوباره امتحان کنید .", MessageBox_.enumType.Erorr);
                    txtItemName.Focus();
                }
            }
        }
        public void Delete()
        {

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

        #region ---Window Event---
        private void Invoice1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion

        #region ---Key Down---

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
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                AddAccount addAccount = new AddAccount();
                addAccount.ShowDialog();
                txtBuyerName.Focus();
            }
        }
        private void txtTotalPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Insert();
            }
        }
        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Item_Selector.DGV.Items.Count != 0)
                {
                    CurrentItem = Item_Selector.item;
                    txtPrice.Text = CurrentItem.SellPrice.ToString();
                    txtNumber.Text = CurrentItem.Number.ToString();
                    txtNumber.Focus();
                }
                else
                {
                    _ShowMessage("هیچ کالایی با این نام یافت نشد .", MessageBox_.enumType.Warning);
                    txtItemName.Focus();
                }
                Item_Selector.Visibility = Visibility.Hidden;
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
                txtTotalPrice.Focus();
            }
        }

        #endregion

        #region ---Lost Focus---
        private void txtBuyerName_LostFocus(object sender, RoutedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Hidden;
        }

        #endregion

        #region ---Is Keyboard Focused Changed ---
        private void txtTotalPrice_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            GetParameters();
            txtTotalPrice.Text = (Number * Price).ToString();
        }

        private void Invoice1_Loaded(object sender, RoutedEventArgs e)
        {
            txtBuyerName.Focus();
        }
        #endregion

        #region ---Data Grid---
        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
        private void txtTotalPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #region ---Click---

        private void SellCash_Click(object sender, RoutedEventArgs e)
        {
            SellCash.IsChecked = true;
            SellCredit.IsChecked = false;
        }

        private void SellCredit_Click(object sender, RoutedEventArgs e)
        {
            SellCash.IsChecked = false;
            SellCredit.IsChecked = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region ---Text Changed---

        private void txtBuyerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Account_Selector.Visibility = Visibility.Visible;
            Account_Selector.DGV.ItemsSource = Account_BL.GetAccountsByName(txtBuyerName.Text);
            if (!String.IsNullOrEmpty(txtBuyerName.Text))
            {
                if (Account_Selector.DGV.Items.Count != 0)
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
        private void txtTotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTotalPrice.Text = ThreeDigitSeparator(txtTotalPrice.Text);
            txtTotalPrice.SelectionStart = txtTotalPrice.Text.Length;
        }
        private void txtItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Item_Selector.Visibility = Visibility.Visible;
            Item_Selector.DGV.ItemsSource = Item_BL.GetItemsByName(null, txtItemName.Text);
            if (!String.IsNullOrEmpty(txtItemName.Text))
            {
                if (Item_Selector.DGV.Items.Count != 0)
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
        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPrice.Text = ThreeDigitSeparator(txtPrice.Text);
            txtPrice.SelectionStart = txtPrice.Text.Length;
        }

        #endregion

        #endregion
    }
}
