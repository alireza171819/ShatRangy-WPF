using System;
using System.Windows;

namespace ShatRangyy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Cultures.InitializePersianCulture();
        }
            

        private void TemplateBindingExtension_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void ControlTemplate_DragEnter(object sender, DragEventArgs e)
        {
            
        }
    }
}
