using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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

        public async Task SignIn(HttpContext httpContext, UserModel user)
        {
            string sql = "SELECT * FROM Users WHERE UserEmail = @UserEmail AND UserPassword = @UserPassword;";

            using (var con = new SqlConnection(_connectionString))
            {
                var dbUserData = con.Query<UserModel>(sql, new { user.UserEmail, user.UserPassword }).FirstOrDefault();

                if (dbUserData == null)
                {
                    throw new Exception();
                }

                IIdentity identity = ClaimsFactory.GetClaimsIdentity(GetUserClaims(dbUserData), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = ClaimsFactory.GetClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
        }

        public IUserValidation Register(IUserModel user)
        {
            if (Exist(user) == false)
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    con.ExecuteAsync("INSERT INTO Users (UserEmail, UserPassword) VALUES (@UserEmail, @UserPassword)", new { user.UserEmail, user.UserPassword });
                }
                return new UserValidation().UserCreationSuccess();
            }
            else
                return new UserValidation().UserNameTaken();

        }

        private bool Exist(IUserModel user)
        {
            string sql = "SELECT * FROM Users WHERE UserEmail = @UserEmail";
            IUserModel dbUserData;
            using (var con = new SqlConnection(_connectionString))
            {
                dbUserData = con.Query<UserModel>(sql, new { user.UserEmail }).FirstOrDefault();
            }

            if (dbUserData == null)
                return false;
            else
                return true;
        }

        public async void SignOut(HttpContext httpContext)
        {
            if (!(httpContext.User == null))
                await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> GetUserClaims(UserModel user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserEmail));
            return claims;
        }
    }
}
