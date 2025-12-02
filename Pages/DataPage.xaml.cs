using Delivery.Back;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Delivery.Pages
{
    public partial class DataPage : Page
    {
        string Tag;
        string Uid;
        string Command;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        readonly string connStr = "Server = 95.183.12.18; Port = 3306; Database = obed; user = obed";
        public DataPage(string tag, string uid)
        {
            InitializeComponent();
            Tag = uid;
            Uid = tag;
            label.Text = $"Поиск по {tag}";
            dataGrid.ItemsSource = dataGrid.ItemsSource = new DatabaseInteraction().LoadData($"Select * From {Tag}").DefaultView;
        }
        private void searchButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => dataGrid.ItemsSource = new DatabaseInteraction().LoadData($"Select * From {Tag} where {Uid} = '{SrcBox.Text}'").DefaultView;
        private void deleteButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new DatabaseInteraction().InsertData($"{Tag}", $"Select * From {Tag}", (DataView)dataGrid.ItemsSource);
            dataGrid.ItemsSource = new DatabaseInteraction().LoadData($"Select * From {Tag}").DefaultView;
        }

        private void AddButton_Копировать_Click(object sender, RoutedEventArgs e)
        {
            int ID = 0;
            try
            {
                using (conn = new MySqlConnection(connStr))
                {
                    var cellContent = dataGrid.Columns[0].GetCellContent(dataGrid.SelectedItem);
                    if (cellContent is TextBlock textBlock)
                    {
                        ID = int.Parse(textBlock.Text);
                    }
                    string quer = $"DELETE FROM {Tag} WHERE Номер = {ID}";
                    cmd = new MySqlCommand(quer, conn);
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            dataGrid.ItemsSource = dataGrid.ItemsSource = new DatabaseInteraction().LoadData($"Select * From {Tag}").DefaultView;
        }
    }
}
