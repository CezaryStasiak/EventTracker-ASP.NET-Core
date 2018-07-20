using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventTracker.DateData;
using EventTracker.Models;
using EventTracker.Settings;

namespace EventTracker.UserData
{
    public sealed class DbConnection : IDbConnection
    {
        //async db connection 
        public async Task<IEnumerable<EventModel>> GetAllEvents(int userId, string connectionString)
        {
            // should use Store Procedures in release !
            List<EventModel> events = new List<EventModel>();
            using (SqlConnection dbconnection = new SqlConnection(connectionString))
            {
                SqlCommand command = dbconnection.CreateCommand();
                command.Connection = dbconnection;
                try
                {
                    command.CommandText = $"SELECT Title, Description FROM Events";
                    dbconnection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    events = Read<EventModel>(reader);
                }

                catch (Exception e)
                {
                    //TBD-.-
                }

                return events;
            }
        }

        //to be fixed
        private static List<T> Read<T>(SqlDataReader reader) where T : new()
        {
            List<T> result = new List<T>();
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

            return result;
        }
    }
}


