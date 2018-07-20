using System.Collections.Generic;
using System.Threading.Tasks;
using EventTracker.Models;

namespace EventTracker.UserData
{
    public interface IDbConnection
    {
        IEnumerable<T> ReadToList<T>(string connectionString, string tableName) where T : new();
    }
}