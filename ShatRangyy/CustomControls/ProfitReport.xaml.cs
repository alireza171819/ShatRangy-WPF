using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Business.Service;
using Business;
using System.Globalization;

namespace ShatRangyy.CustomControls
{
    public partial class ProfitReport : Window
    {
        public ProfitReport()
        {
            InitializeComponent();
        }
        PersianCalendar PersianCalendar = new PersianCalendar();
        ProfitReport_BL ProfitReport_BL = new ProfitReport_BL();
        Transaction_BL Transaction_BL = new Transaction_BL();
        int StartYear, EndYear, StartMonth, EndMonth, StartDay, EndDay;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        
        void GetParameters()
        {
            if (!String.IsNullOrEmpty(txtSearchFromYear.Text))
            {
                StartYear = int.Parse(txtSearchFromYear.Text);
            }
            else
            {
                StartYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToYear.Text))
            {
                EndYear = int.Parse(txtSearchToYear.Text);
            }
            else
            {
                EndYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromMonth.Text))
            {
                StartMonth = int.Parse(txtSearchFromMonth.Text);
            }
            else
            {
                StartMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToMonth.Text))
            {
                EndMonth = int.Parse(txtSearchToMonth.Text);
            }
            else
            {
                EndMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromDay.Text))
            {
                StartDay = int.Parse(txtSearchFromDay.Text);
            }
            else
            {
                StartDay = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToDay.Text))
            {
                EndDay = int.Parse(txtSearchToDay.Text);
            }
            else
            {
                EndDay = 0;
            }
        }
        void GetProfit()
        {
            GetParameters();
            var _listByDate = Transaction_BL.GetTransactionsByDate(StartYear, EndYear, StartMonth, EndMonth, StartDay, EndDay);
            decimal buysTotal, sellsTotal, serviceTotal, otherCosts, otherIncom;
            buysTotal = ProfitReport_BL.GetBuysTotal(_listByDate);
            sellsTotal = ProfitReport_BL.GetSellsTotal(_listByDate);
            serviceTotal = ProfitReport_BL.GetServiceTotal(_listByDate);
            otherCosts = ProfitReport_BL.GetTotalOtherPayment(_listByDate);
            otherIncom = ProfitReport_BL.GetServiceTotal(_listByDate);
            lbBuysTotal.Content = buysTotal.ToString("#,#");
            lbSellsTotal.Content = sellsTotal.ToString("#,#");
            lbServicesTotal.Content = serviceTotal.ToString("#,#");
            lbOtherCosts.Content = otherCosts.ToString("#,#");
            lbOtherIncom.Content = otherIncom.ToString("#,#");
            lbTotalIncom.Content = (serviceTotal + sellsTotal + otherIncom).ToString("#,#");
            lbTotalCosts.Content = (buysTotal + otherCosts).ToString("#,#");
            lbProfit.Content = ((serviceTotal + sellsTotal + otherIncom) - (buysTotal + otherCosts)).ToString("#,#");
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearchFromYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchFromMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtSearchFromDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            txtSearchToYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchToMonth.Text = PersianCalendar.GetMonth(DateTime.Now).ToString();
            txtSearchToDay.Text = PersianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            GetProfit();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AllTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text); 
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            GetProfit();
        }
    }
}
