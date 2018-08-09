using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.Models
{
    public class UserValidation : IUserValidation
    {
        private string description;

        public UserValidation RandomError() { return new UserValidation { description = "Sorry, something went wrong, try again!" }; }
        public UserValidation UserCreationSuccess() { return new UserValidation { description = "Account has been created!" }; }
        public UserValidation UserNameTaken() { return new UserValidation { description = "Account for this email is already registered!" }; }
        public UserValidation WrongPassword() { return new UserValidation { description = "Password is invalid!" }; }

        public override string ToString()
        {
            return description;
        }
    }


}
