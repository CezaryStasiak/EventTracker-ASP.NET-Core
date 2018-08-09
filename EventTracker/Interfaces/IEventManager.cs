using System.Collections.Generic;
using System.Threading.Tasks;
using EventTracker.Models;

namespace EventTracker.Services
{
    public interface IEventManager
    {
        Task<IEnumerable<EventModel>> GetEventsAsync(string UserId);
    }
}