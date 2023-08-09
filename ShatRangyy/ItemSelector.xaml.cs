using System;
using System.Windows.Controls;
using System.Windows.Input;
using VeiwModels;

namespace ShatRangyy
{
    public partial class ItemSelector : UserControl
    {
        public ItemSelector()
        {
            InitializeComponent();
        }

        public Item item = null;
        private void DGV_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DGV.ItemsSource != null)
            {
                Item obj = DGV.SelectedItem as Item;
                item = obj;
            }
        }

        private void DGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGV.ItemsSource != null)
            {
                Item obj = DGV.SelectedItem as Item;
                item = obj;
            }
        }

        private void DGV_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (DGV.Items.Count != 0)
            {
                DGV.SelectedIndex = 0;
            }
        }
    }
}
