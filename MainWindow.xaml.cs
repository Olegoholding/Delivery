using Delivery.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Delivery
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        private void Button_Click(object sender, RoutedEventArgs e) => frame.Navigate(new DataPage(((Button) sender).Tag.ToString(), ((Button) sender).Uid));
        private void Border_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => frame.Navigate(new MainPage());
    }
}