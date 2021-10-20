using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInventory.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StudentId { get; set; }

        public UserTypes Type { get; set; }
    }

    public enum UserTypes { 
        Admin = 1,
        Student = 2,
        Faculty = 3
    }
}
