using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventTracker.Models;
using EventTracker.Settings;

namespace EventTracker.UserData
{
    public sealed class DbConnection : IDbConnection
    {
        public string ConnectionString { get; set; }

        public DbConnection(){}

        public DbConnection(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }


        public IEnumerable<T> ReadToList<T>(string connectionString, string tableName) where T : new()
        {

            List<T> result = new List<T>();
            using (SqlConnection dbconnection = new SqlConnection(connectionString))
            {
                SqlCommand command = dbconnection.CreateCommand();
                command.Connection = dbconnection;
                try
                {
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
                        result.Add(t);
                    }
                    reader.Close();


                }
                catch (Exception e)
                {
                    //TBD-.-
                }

                return result;
            }
        }
    }
}


