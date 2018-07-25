using System.Collections.Generic;

namespace EventTracker.UserData
{
    public interface IDbConnection
    {
        IEnumerable<T> Read<T>(string query) where T : new();
        void Insert<T>(T obj, string TableName);
    }
}