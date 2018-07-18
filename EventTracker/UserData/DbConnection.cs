using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.DateData;
using EventTracker.Models;
using EventTracker.Settings;

namespace EventTracker.UserData
{
    public sealed class DbConnection : IDbConnection
    {
        //async db connection 
        public async Task<List<EventModel>> GetAllEvents(int userId, string connectionString)
        {
            // should use Store Procedures in release !
            List<EventModel> events = new List<EventModel>();
            using (SqlConnection dbconnection = new SqlConnection(connectionString))
            {
                dbconnection.Open();
                SqlCommand command = dbconnection.CreateCommand();
                SqlTransaction transaction;

                transaction = dbconnection.BeginTransaction("GetAllEvents");

                command.Connection = dbconnection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = $"SELECT Title, Description, Date FROM Events WHERE userId = {userId}";
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        events.Add(new EventModel
                        {
                            Title = reader.GetString(0),
                            Description = reader.GetString(1),
                            Date = new Date { Year = reader.GetDateTime(2).Year, Month = reader.GetDateTime(2).Month, Day = reader.GetDateTime(2).Day }
                        });
                    }
                }
                
                catch (Exception e)
                {
                    //TBD-.-
                }

                return events;
            }
        }
    }
}
