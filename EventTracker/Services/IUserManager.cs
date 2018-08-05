using EventTracker.Models;
using Microsoft.AspNetCore.Http;

namespace EventTracker.Services
{
    public interface IUserManager
    {
        void SignIn(HttpContext httpContext, UserModel user);
        void SignOut(HttpContext httpContext);
    }
}