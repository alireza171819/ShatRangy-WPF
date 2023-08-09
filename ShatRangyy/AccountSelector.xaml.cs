using System.Windows.Controls;
using System.Windows.Input;
using VeiwModels;

namespace ShatRangyy
{
    public partial class AccountSelector : UserControl
    {
        public AccountSelector()
        {
            InitializeComponent();
        }
        public Account account;

        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV.SelectedItem != null)
            {
                Account obj = DGV.SelectedItem as Account;
                account = obj;
            }
        }

        private void DGV_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (DGV.Items.Count != 0)
            {
                DGV.SelectedIndex = 0;
            }
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void DGV_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

                if (DGV.SelectedItem != null)
                {
                    Account obj = DGV.SelectedItem as Account;
                    account = obj;
                }
            }
        }
    }
}
