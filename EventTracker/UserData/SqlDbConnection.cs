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
            Type objectModel = obj.GetType();
            MemberInfo[] prop = objectModel.GetProperties();

            string values = QueryBuilder.BuildPropertiesList(obj, prop, objectModel);
            string types = QueryBuilder.BuildTypeList(obj, prop, objectModel);

            using (SqlConnection dbconnection = new SqlConnection(ConnectionString))
            {
                dbconnection.Open();
                SqlCommand command = dbconnection.CreateCommand();
                command.Connection = dbconnection;
                command.CommandText = $"INSERT INTO {TableName} ({types}) VALUES ({values})";
                command.ExecuteNonQuery();
                dbconnection.Close();
            }

        }
        
        static class QueryBuilder
        {
            public static string BuildPropertiesList<T>(T obj, MemberInfo[] prop, Type objectModel)
            {
                string result = "";

                for (int i = 0; i < prop.Length; i++)
                {
                    if (i == prop.Length - 1)
                    {
                        if (objectModel.GetProperty(prop[i].Name).PropertyType == typeof(DateTime))
                        {
                            result += " '" + DateTimeFormatHandler(objectModel.GetProperty(prop[i].Name).GetValue(obj)) + "'";
                        }
                        else
                        {
                            result += " '" + objectModel.GetProperty(prop[i].Name).GetValue(obj) + "'";
                        }
                    }
                    else
                    {
                        if (objectModel.GetProperty(prop[i].Name).PropertyType == typeof(DateTime))
                        {
                            result += " '" + DateTimeFormatHandler(objectModel.GetProperty(prop[i].Name).GetValue(obj)) + "', ";
                        }
                        else
                        {
                            result += " '" + objectModel.GetProperty(prop[i].Name).GetValue(obj) + "', ";
                        }
                    }
                }

                return result;
            }

            public static string BuildTypeList<T>(T obj, MemberInfo[] prop, Type type)
            {
                string result = "";
                for (int i = 0; i < prop.Length; i++)
                {
                    if (i == prop.Length - 1)
                    {
                        result += prop[i].Name;
                    }
                    else
                    {
                        result += prop[i].Name + ", ";
                    }
                }
                return result;
            }

            private static string DateTimeFormatHandler(object v)
            {
                DateTime date = new DateTime();
                date = (DateTime)v;
                return date.ToString("yyyy-MM-dd");
            }
        }
    }

}


