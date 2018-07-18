using System.Collections.Generic;
using System.Threading.Tasks;
using EventTracker.Models;

namespace EventTracker.UserData
{
    public interface IDbConnection
    {
        Task<List<EventModel>> GetAllEvents(int userId, string connectionString);
    }
}