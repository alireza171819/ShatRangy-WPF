using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using ShatRangyy.CustomControls;

namespace ShatRangyy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }

        public string date { get { return (string)GetValue(Date); }
            set { SetValue(Date, value); }
        }
        public static readonly DependencyProperty Date = DependencyProperty.Register("date", typeof(string), typeof(MainWindow));

        private void btnBuyManagement_Click(object sender, RoutedEventArgs e)
        {
            BuyManagement buyManagement = new BuyManagement();
            ContentPanel.Children.Add(buyManagement);
            buyManagement.Width = ContentPanel.Width;
            buyManagement.Height = ContentPanel.Height;
        }

        private void btnAccounting_Click(object sender, RoutedEventArgs e)
        {
            TransactionManagement transactionManagement = new TransactionManagement();
            ContentPanel.Children.Add(transactionManagement);
            transactionManagement.Width = ContentPanel.Width;
            transactionManagement.Height = ContentPanel.Height;
        }

        private void btnAccountsManagement_Click(object sender, RoutedEventArgs e)
        {
            AccountsManagement accountsManagement = new AccountsManagement();
            ContentPanel.Children.Add(accountsManagement);
            accountsManagement.Width = ContentPanel.Width;
            accountsManagement.Height = ContentPanel.Height;
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            ItemsManagement itemsManagement = new ItemsManagement();
            ContentPanel.Children.Add(itemsManagement);
            itemsManagement.Width = ContentPanel.Width;
            itemsManagement.Height = ContentPanel.Height;
        }

        private void btnDocumentsReport_Click(object sender, RoutedEventArgs e)
        {
            DocumentsReport documentsReport = new DocumentsReport();
            ContentPanel.Children.Add(documentsReport);
            documentsReport.Width = ContentPanel.Width;
            documentsReport.Height = ContentPanel.Height;
        }

        private void btnAccountsReport_Click(object sender, RoutedEventArgs e)
        {
            AccountsReport accountsReport = new AccountsReport();
            ContentPanel.Children.Add(accountsReport);
            accountsReport.Width = ContentPanel.Width;
            accountsReport.Height = ContentPanel.Height;
        }

        private void btnItemsReport_Click(object sender, RoutedEventArgs e)
        {
            ItemsReport itemsReport = new ItemsReport();
            ContentPanel.Children.Add(itemsReport);
            itemsReport.Width = ContentPanel.Width;
            itemsReport.Height = ContentPanel.Height;
        }

        private void MainBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            ContentPanel.Children.Add(settings);
            settings.Width = ContentPanel.Width;
            settings.Height = ContentPanel.Height;
        }

        private void LeftBorder_DpiChanged(object sender, DpiChangedEventArgs e)
        {    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            date = DateTime.Now.ToLongDateString();
            lbDate.Content = date;
            _ShowMessage("به نرم افزار حسابداری میدان و تره بار کلیک خوش آمدید .", MessageBox_.enumType.InTheNameOfGod);
        }

        private void btnSellManagement_Click(object sender, RoutedEventArgs e)
        {
            SellManagement sellManagement = new SellManagement();
            ContentPanel.Children.Add(sellManagement);
            sellManagement.Width = ContentPanel.Width;
            sellManagement.Height = ContentPanel.Height;
        }

        private void RightBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnResize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnSerViceManagment_Click(object sender, RoutedEventArgs e)
        {
            ServiceManagment serviceManagment = new ServiceManagment();
            ContentPanel.Children.Add(serviceManagment);
            serviceManagment.Width = ContentPanel.Width;
            serviceManagment.Height = ContentPanel.Height;
        }

        private void btnProfitReport_Click(object sender, RoutedEventArgs e)
        {
            ProfitReport profitReport = new ProfitReport();
            profitReport.Show();
        }
    }
}
