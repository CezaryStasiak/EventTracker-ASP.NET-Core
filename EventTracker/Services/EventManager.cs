using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.Models;
using Dapper;

namespace EventTracker.Services
{
    public class EventManager : IEventManager
    {
        private string _connectionString { get; set; }

        public EventManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<EventModel>> GetEventsAsync(string UserId)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var dbUserData = await con.QueryAsync<EventModel>($"SELECT * FROM Events WHERE UserId={UserId}");

                return dbUserData;
            }
        }
    }
}
