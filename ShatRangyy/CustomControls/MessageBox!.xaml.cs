using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace ShatRangyy
{
    /// <summary>
    /// Interaction logic for MessageBox_.xaml
    /// </summary>
    public partial class MessageBox_ : Window
    {
        public MessageBox_()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        }
        public enum enumAction
        {
            wait, start, close
        }
        public enum enumType
        {
            Success, Warning, Erorr, Info, InTheNameOfGod
        }
        private MessageBox_.enumAction action;
        private double x, y;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        
        public void ShowMessage(string Message, enumType type)
        {
            MainBorder.Opacity = 0.0;
            string formName;
            for (int i = 1; i < 10; i++)
            {
                formName = "MessageBox" + i.ToString();
                this.Name = formName;
                this.Left = SystemParameters.PrimaryScreenWidth - Width - 2;
                this.Top = SystemParameters.PrimaryScreenHeight - Height * i - 30 * i;
                break;
            }
            x = SystemParameters.PrimaryScreenWidth - base.Width - 5;
           
            switch (type)
            {
                case enumType.Success:
                    lbTitel.Content = "عملیات موفق";
                    MainBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 199, 136));
                    RightBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 151, 136));
                    break;
                case enumType.Warning:
                    lbTitel.Content = "هشدار";
                    MainBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(241, 196, 17));
                    RightBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(216, 119, 33));
                    break;
                case enumType.Erorr:
                    lbTitel.Content = "خطا";
                    MainBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 66, 54));
                    RightBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 20, 9));
                    break;
                case enumType.Info:
                    lbTitel.Content = "پیام";
                    MainBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 158, 255));
                    RightBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 128, 255));
                    break;
                case enumType.InTheNameOfGod:
                    lbTitel.Content = "به نام خدا";
                    MainBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 158, 255));
                    RightBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 128, 255));
                    break;
            }
            lbMessage.Text = Message;
            this.Topmost = true;
            this.Show();
            action = enumAction.start;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case enumAction.wait:
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 4000);
                    MainBorder.Opacity = 100;
                    action = enumAction.close;
                    break;
                case enumAction.start:
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                    MainBorder.Opacity += 0.1;
                    if (x < this.Left)
                    {
                        Left--;
                    }
                    else
                    {
                        if (MainBorder.Opacity == 0.1)
                        {
                            action = enumAction.wait;
                        }
                    }
                    action = enumAction.wait;
                    break;
                case enumAction.close:
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                    Left -= 4;
                    MainBorder.Opacity -= 0.1;
                    if (MainBorder.Opacity == 99.9)
                    {
                        this.Close();
                    }
                    break;
            }
        }
    }
}
