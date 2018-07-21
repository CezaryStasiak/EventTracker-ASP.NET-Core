using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace EventTracker.UserData
{
    public sealed class SqlDbConnection : IDbConnection
    {
        public string ConnectionString { get; set; }

        public SqlDbConnection() { }

        public SqlDbConnection(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public IEnumerable<T> Read<T>(string tableName) where T : new()
        {
            using (SqlConnection dbconnection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = dbconnection.CreateCommand();
                command.Connection = dbconnection;
                command.CommandText = $"SELECT * FROM {tableName}";
                dbconnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    T t = new T();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Type type = t.GetType();
                        string name = reader.GetName(i);
                        PropertyInfo prop = type.GetProperty(name);
                        if (prop != null)
                        {
                            if (name == prop.Name)
                            {
                                var value = reader.GetValue(i);
                                if (value != DBNull.Value)
                                {
                                    prop.SetValue(t, Convert.ChangeType(value, prop.PropertyType), null);
                                }
                                prop.SetValue(t, value, null);
                            }
                        }
                    }
                    yield return t;
                }
                reader.Close();

            }
        }
    }
}


