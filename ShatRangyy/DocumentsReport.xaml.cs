using Business;
using Business.Service;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShatRangyy
{
    /// <summary>
    /// Interaction logic for DocumentsReport.xaml
    /// </summary>
    public partial class DocumentsReport : UserControl
    {
        public DocumentsReport()
        {
            InitializeComponent();
        }

        #region Varibles And Objects
        BuyDocument_BL BuyDocument_BL = new BuyDocument_BL();
        SellDocument_BL SellDocument_BL = new SellDocument_BL();
        SerVice_BL SerVice_BL = new SerVice_BL();
        Transaction_BL Transaction_BL = new Transaction_BL();
        string AccountName;
        int SearchFromYear, SearchToYear, SearchFromMonth, SearchToMonth, SearchFromDay, SearchToDay;
        /// <summary>
        /// regex that matches disallowed text
        /// </summary>
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        #endregion

        #region Function

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        public void GetParameters()
        {
            if (!String.IsNullOrEmpty(txtSearchToDay.Text))
            {
                SearchToDay = int.Parse(txtSearchToDay.Text);
            }
            else
            {
                SearchToDay = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToMonth.Text))
            {
                SearchToMonth = int.Parse(txtSearchToMonth.Text);
            }
            else
            {
                SearchToMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToYear.Text))
            {
                SearchToYear = int.Parse(txtSearchToYear.Text);
            }
            else
            {
                SearchToYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromYear.Text))
            {
                SearchFromYear = int.Parse(txtSearchFromYear.Text);
            }
            else
            {
                SearchFromYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromMonth.Text))
            {
                SearchFromMonth = int.Parse(txtSearchFromMonth.Text);
            }
            else
            {
                SearchFromMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromDay.Text))
            {
                SearchFromDay = int.Parse(txtSearchFromDay.Text);
            }
            else
            {
                SearchFromDay = 0;
            }

            AccountName = txtSearch.Text;
        }
        public void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        public void FilterDataGrid(string accountName,int fromYear, int toYear, int fromMonth, int toMonth
            , int fromDay, int toDay)
        {
            GetParameters();
            var _buyDocumentByDate = BuyDocument_BL.GetBuyDocumentsByDate(fromYear, toYear, fromMonth, toMonth, fromDay, toDay);
            var _sellDocumentByDate = SellDocument_BL.GetSellDocumentsByDate(fromYear, toYear, fromMonth, toMonth, fromDay, toDay);
            var _transactionByDate = Transaction_BL.GetTransactionsByDate(fromYear, toYear, fromMonth, toMonth, fromDay, toDay);
            var _serviceByDate = SerVice_BL.GetServicesByStartDate(fromYear, toYear, fromMonth, toMonth, fromDay, toDay);

            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                DGV_BuyDocument.ItemsSource = BuyDocument_BL.GetBuyDocumentsBySellerName(_buyDocumentByDate, accountName);
                DGV_SellDocument.ItemsSource = SellDocument_BL.GetSellDocumentsByBuyerName(_sellDocumentByDate, accountName);
                DGV_Service.ItemsSource = SerVice_BL.GetSerVicesByCustomerName(_serviceByDate, accountName);
                DGV_Transaction.ItemsSource = Transaction_BL.GetTransactionsByAccountSide(_transactionByDate, accountName);
            }
            else
            {
                DGV_BuyDocument.ItemsSource = _buyDocumentByDate;
                DGV_SellDocument.ItemsSource = _sellDocumentByDate;
                DGV_Service.ItemsSource = _serviceByDate;
                DGV_Transaction.ItemsSource = _transactionByDate;
            }
        }
        public void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }

        #endregion

        #region Event

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            FilterDataGrid(AccountName, SearchFromYear, SearchToYear, SearchFromMonth,
                SearchToMonth, SearchFromDay, SearchToDay);
        }

        private void AllTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtSearchToMonth_KeyDown(object sender, KeyEventArgs e)
        {
            FilterDataGrid(AccountName, SearchFromYear, SearchToYear, SearchFromMonth,
                SearchToMonth, SearchFromDay, SearchToDay);
        }

        private void txtSearchToDay_KeyDown(object sender, KeyEventArgs e)
        {
            FilterDataGrid(AccountName, SearchFromYear, SearchToYear, SearchFromMonth,
                SearchToMonth, SearchFromDay, SearchToDay);
        }

        private void txtSearchToYear_KeyDown(object sender, KeyEventArgs e)
        {
            FilterDataGrid(AccountName, SearchFromYear, SearchToYear, SearchFromMonth,
                SearchToMonth, SearchFromDay, SearchToDay);
        }

        private void txtSearchFromDay_KeyDown(object sender, KeyEventArgs e)
        {
            FilterDataGrid(AccountName, SearchFromYear, SearchToYear, SearchFromMonth,
                SearchToMonth, SearchFromDay, SearchToDay);
        }

        private void txtSearchFromMonth_KeyDown(object sender, KeyEventArgs e)
        {
            FilterDataGrid(AccountName, SearchFromYear, SearchToYear, SearchFromMonth,
                SearchToMonth, SearchFromDay, SearchToDay);
        }

        private void txtSearchFromYear_KeyDown(object sender, KeyEventArgs e)
        {
            FilterDataGrid(AccountName, SearchFromYear, SearchToYear, SearchFromMonth,
                SearchToMonth, SearchFromDay, SearchToDay);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
