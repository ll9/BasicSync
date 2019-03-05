using BasicSync.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSync.Data
{
    public class AdoContext
    {
        private const string TableName = nameof(ApplicationDbContext.BasicEntities);

        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection("Data Source=db.sqlite");
            connection.Open();
            return connection;
        }

        public void ExecuteQuery(string query)
        {
            using (var conneciton = GetConnection())
            using (var command = new SQLiteCommand(query, conneciton))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Fill(DataTable dataTable)
        {
            var query = $"SELECT * FROM {TableName}";

            using (var conneciton = GetConnection())
            using (var adapter = new SQLiteDataAdapter(query, conneciton))
            {
                adapter.Fill(dataTable);
            }
        }

        public void Update(DataTable dataTable)
        {
            var query = $"SELECT * FROM {TableName}";

            using (var conneciton = GetConnection())
            using (var adapter = new SQLiteDataAdapter(query, conneciton))
            {
                var commandBuilder = new SQLiteCommandBuilder(adapter);

                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                adapter.InsertCommand = commandBuilder.GetInsertCommand();

                adapter.Update(dataTable);
            }
        }
    }
}
