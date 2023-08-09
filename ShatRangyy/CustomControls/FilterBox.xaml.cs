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
using System.Windows.Shapes;

namespace ShatRangyy.CustomControls
{
    /// <summary>
    /// Interaction logic for FilterBox.xaml
    /// </summary>
    public partial class FilterBox : Window
    {
        public FilterBox()
        {
            InitializeComponent();
        }
        public string LabelContent1, LabelContent2,
            LabelContent3, LabelContent4, LabelContent5,
            LabelContent6, LabelContent7, LabelContent8,
            LabelContent9;
        public SelectedIndex selectedIndex;
        public enum SelectedIndex 
        { 
            Index1 = 1, Index2 = 2, Index3 = 3, Index4 = 4,
            Index5 = 5, Index6 = 6, Index7 = 7, Index8 = 8, Index9 = 9
        }

        public void ClearSelection(int checkBoxNumber)
        {
            switch (checkBoxNumber)
            {
                case 1:
                    CheckBox2.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 2:
                    CheckBox1.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 3:
                    CheckBox2.IsChecked = false;
                    CheckBox1.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 4:
                    CheckBox2.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox1.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 5:
                    CheckBox2.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox1.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 6:
                    CheckBox2.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox1.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 7:
                    CheckBox2.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox1.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 8:
                    CheckBox2.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox1.IsChecked = false;
                    CheckBox9.IsChecked = false;
                    break;
                case 9:
                    CheckBox2.IsChecked = false;
                    CheckBox3.IsChecked = false;
                    CheckBox4.IsChecked = false;
                    CheckBox5.IsChecked = false;
                    CheckBox6.IsChecked = false;
                    CheckBox7.IsChecked = false;
                    CheckBox8.IsChecked = false;
                    CheckBox1.IsChecked = false;
                    break;
            }
        }
        public void SelectIndex(SelectedIndex index, int checkBoxNumber)
        {
            ClearSelection(checkBoxNumber);
            switch (index)
            {
                case SelectedIndex.Index1:
                    CheckBox1.IsChecked = true;
                    selectedIndex = SelectedIndex.Index1;
                    break;
                case SelectedIndex.Index2:
                    CheckBox2.IsChecked = true;
                    selectedIndex = SelectedIndex.Index2;
                    break;
                case SelectedIndex.Index3:
                    CheckBox3.IsChecked = true;
                    selectedIndex = SelectedIndex.Index3;
                    break;
                case SelectedIndex.Index4:
                    CheckBox4.IsChecked = true;
                    selectedIndex = SelectedIndex.Index4;
                    break;
                case SelectedIndex.Index5:
                    CheckBox5.IsChecked = true;
                    selectedIndex = SelectedIndex.Index5;
                    break;
                case SelectedIndex.Index6:
                    CheckBox6.IsChecked = true;
                    selectedIndex = SelectedIndex.Index6;
                    break;
                case SelectedIndex.Index7:
                    CheckBox7.IsChecked = true;
                    selectedIndex = SelectedIndex.Index7;
                    break;
                case SelectedIndex.Index8:
                    CheckBox8.IsChecked = true;
                    selectedIndex = SelectedIndex.Index8;
                    break;
                case SelectedIndex.Index9:
                    CheckBox9.IsChecked = true;
                    selectedIndex = SelectedIndex.Index9;
                    break;
                default:
                    break;
            }
        }

        #region ---Checked---
        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index1, 1);
        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index2, 2);
        }

        private void CheckBox3_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index3, 3);
        }

        private void CheckBox4_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index4, 4);
        }

        private void CheckBox5_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index5, 5);
        }

        private void CheckBox6_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index6, 6);
        }

        private void CheckBox7_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index7, 7);
        }

        private void CheckBox8_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index8, 8);
        }

        private void CheckBox9_Checked(object sender, RoutedEventArgs e)
        {
            SelectIndex(SelectedIndex.Index9,9);
        }

        #endregion


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbN1.Content = LabelContent1;
            lbN2.Content = LabelContent2;
            lbN3.Content = LabelContent3;
            lbN4.Content = LabelContent4;
            lbN5.Content = LabelContent5;
            lbN6.Content = LabelContent6;
            lbN7.Content = LabelContent7;
            lbN8.Content = LabelContent8;
            lbN9.Content = LabelContent9;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
