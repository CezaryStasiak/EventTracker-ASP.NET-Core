using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;

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

        public IEnumerable<T> Read<T>(string query) where T : new()
        {
            using (SqlConnection dbconnection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = dbconnection.CreateCommand();
                command.Connection = dbconnection;
                command.CommandText = query;
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
                dbconnection.Close();
            }
        }

        public void Insert<T>(T obj, string TableName)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder typeBuilder = new StringBuilder();
            typeBuilder.Clear();
            builder.Clear();

            Type type = obj.GetType();
            MemberInfo[] prop = type.GetProperties();

            for (int i = 0; i < prop.Length; i++)
            {
                if (i == prop.Length - 1)
                {
                    if (type.GetProperty(prop[i].Name).PropertyType == typeof(DateTime))
                    {
                        builder.Append(" '" + DateTimeFormatHandler(type.GetProperty(prop[i].Name).GetValue(obj)) + "'");
                    }
                    else
                    {
                        builder.Append(" '" + type.GetProperty(prop[i].Name).GetValue(obj) + "'");
                    }
                        typeBuilder.Append(prop[i].Name);
                }
                else
                {
                    if (type.GetProperty(prop[i].Name).PropertyType == typeof(DateTime))
                    {
                        builder.Append(" '" + DateTimeFormatHandler(type.GetProperty(prop[i].Name).GetValue(obj)) + "', ");
                    }
                    else
                    {
                        builder.Append(" '" + type.GetProperty(prop[i].Name).GetValue(obj) + "', ");
                    }
                        typeBuilder.Append(prop[i].Name + ", ");
                }
            }

            using (SqlConnection dbconnection = new SqlConnection(ConnectionString))
            {
                dbconnection.Open();
                SqlCommand command = dbconnection.CreateCommand();
                command.Connection = dbconnection;
                command.CommandText = $"INSERT INTO {TableName} ({typeBuilder.ToString()}) VALUES ({builder.ToString()})";
                command.ExecuteNonQuery();
                dbconnection.Close();
            }
            
        }

        private string DateTimeFormatHandler(object v)
        {
            DateTime date = new DateTime();
            date = (DateTime) v;
            return date.ToString("yyyy-MM-dd");
        }
    }

}


