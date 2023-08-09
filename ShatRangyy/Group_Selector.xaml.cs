using System.Windows.Controls;
using VeiwModels;

namespace ShatRangyy
{
    public partial class Group_Selector : UserControl
    {
        public Group_Selector()
        {
            InitializeComponent();
        }
        public AccountGroup Group;

        private void DGV_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (DGV.Items.Count != 0)
            {
                DGV.SelectedIndex = 0;
            }
        }

        private void DGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGV.SelectedItem != null)
            {
                AccountGroup obj = DGV.SelectedItem as AccountGroup;
                Group = obj;
            }
        }
    }
}
