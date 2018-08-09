using System.Threading.Tasks;
using EventTracker.Models;
using Microsoft.AspNetCore.Http;

namespace EventTracker.Services
{
    public interface IUserManager
    {
        IUserValidation Register(IUserModel user);
        Task SignIn(HttpContext httpContext, UserModel user);
        void SignOut(HttpContext httpContext);
    }
}