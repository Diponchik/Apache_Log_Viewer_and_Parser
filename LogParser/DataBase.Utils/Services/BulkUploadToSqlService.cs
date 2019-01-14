using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataBase.Utils.Services
{
    public class BulkUploadToSqlService<T>
    {
        public IList<T> InternalStore { get; set; }
        public string TableName { get; set; }
        public int CommitBatchSize { get; set; } = 1000;
        public string ConnectionString { get; set; }

        public void Commit()
        {
            if (InternalStore.Count <= 0)
                return;

            var numberOfPages = (InternalStore.Count / CommitBatchSize) +
                                (InternalStore.Count % CommitBatchSize == 0 ? 0 : 1);
            for (var pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
            {
                var dt = InternalStore.Skip(pageIndex * CommitBatchSize).Take(CommitBatchSize).ToDataTable();
                BulkInsert(dt);
            }
        }

        private void BulkInsert(DataTable dt)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var bulkCopy =
                    new SqlBulkCopy
                    (
                        connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers |
                        SqlBulkCopyOptions.UseInternalTransaction,
                        null
                    )
                    {
                        DestinationTableName = TableName
                    };

                connection.Open();

                bulkCopy.WriteToServer(dt);
                connection.Close();
            }
        }
    }

    public static class BulkUploadToSqlHelper
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}