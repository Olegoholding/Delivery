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

namespace Delivery.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        bool _isDarkTheme = false;
        public MainPage()
        {
            InitializeComponent();
            time.Text = DateTime.Now.ToShortTimeString().ToString();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            string currentTheme = _isDarkTheme ? "LightTheme" : "DarkTheme";

            var newTheme = (ResourceDictionary)Application.LoadComponent(new Uri($"Theme/{currentTheme}.xaml", UriKind.Relative));
            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}
