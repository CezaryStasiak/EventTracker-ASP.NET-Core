using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        
        public string UserEmail { get; set; }
        
        public string UserPassword { get; set; }
    }
}
