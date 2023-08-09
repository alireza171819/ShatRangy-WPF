using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShatRangyy
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }
        public void Close()
        {
            (this.Parent as DockPanel).Children.Remove(this);
        }
        public void GetParameters()
        {

        }
        public void Insert()
        {

        }
        public void Update()
        {

        }
        public void Delete()
        {

        }
        public void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
