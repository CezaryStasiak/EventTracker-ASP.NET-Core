using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using EventTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace EventTracker.Services
{
    public class UserManager : IUserManager
    {
        string _connectionString;

        public UserManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async void SignIn(HttpContext httpContext, UserModel user)
        {
            string sql = "SELECT * FROM Users WHERE UserEmail = @UserEmail AND UserPassword = @UserPassword;";

            using (var con = new SqlConnection(_connectionString))
            {
                var dbUserData = con.Query<UserModel>(sql, new { user.UserEmail, user.UserPassword }).FirstOrDefault();

                ClaimsIdentity identity = new ClaimsIdentity(GetUserClaims(dbUserData), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> GetUserClaims(UserModel user)
        {
            if (user == null)
            {
                throw new Exception();
            }

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserEmail));
            return claims;
        }
    }
}
