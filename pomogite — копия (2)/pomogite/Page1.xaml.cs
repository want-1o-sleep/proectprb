
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace pomogite
{
    public partial class Page1 : Page
    {
        private string imya;
        private int aa = 0;

        public Page1()
        {
            InitializeComponent();
        }

        private void nachat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page5());
        }

        private void lid_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page4());
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            imya = names.Text; // Заносим значение из TextBox в переменную
            aa++;

            if (aa > 0)
            {
                nachat.IsEnabled = true;
                lid.IsEnabled = true;
            }
        }
    }
}
