using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_System_Project_Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }


    public class UserRollMapping
    {
        public ApplicationRole Role { get; set; }
        public bool IsChecked { get; set; }
    }

    
}
