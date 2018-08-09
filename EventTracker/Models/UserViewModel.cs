using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.Models;

namespace EventTracker.Models
{
    public class UserViewModel
    {
        public UserValidation Validation { get; set; }
        public UserModel UserLogin { get; set; }
    }
}
