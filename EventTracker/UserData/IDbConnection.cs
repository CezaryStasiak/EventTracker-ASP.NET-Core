using System.Collections.Generic;
using System.Threading.Tasks;
using EventTracker.Models;

namespace EventTracker.UserData
{
    public interface IDbConnection
    {
        IEnumerable<T> Read<T>(string tableName) where T : new();
        void Insert<T>(T obj, string TableName);
    }
}