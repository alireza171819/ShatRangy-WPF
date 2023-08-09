using System;
using System.Windows;

namespace ShatRangyy.CustomControls
{
    /// <summary>
    /// Interaction logic for QuestionBox_.xaml
    /// </summary>
    public partial class QuestionBox_ : Window
    {
        public QuestionBox_()
        {
            InitializeComponent();
        }

        public string Content = String.Empty;
        public bool Ok = false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lbContent.Content = Content;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Ok = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
