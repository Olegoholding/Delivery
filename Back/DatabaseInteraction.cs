using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace Delivery.Back
{
    class DatabaseInteraction
    {
        string Tag;
        string Uid;
        string Command;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        readonly string connStr = "Server = 95.183.12.18; Port = 3306; Database = obed; user = obed";
        public DataTable LoadData(string Command)
        {
            DataTable DataTable = new DataTable();
            using (conn = new MySqlConnection($"{connStr}"))
            {
                try
                {
                    conn.Open();
                    using (cmd = new MySqlCommand($"{Command}", conn))
                    {
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(DataTable);
                    }
                    conn.Close();
                    return DataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.HResult.ToString());
                    return DataTable;
                }
            }
        }
        public void InsertData(string TableName, string Command, DataView DataView)
        {
            var DataSet = new DataSet();
            var DataTable = DataView.Table;
            DataTable.TableName = TableName;
            DataSet.Tables.Add(DataTable);
            try
            {
                using (conn = new MySqlConnection(connStr))
                {
                    adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Command, conn);
                    var builder = new MySqlCommandBuilder(adapter);

                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.Update(DataSet, TableName);
                }
                DataSet.Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
