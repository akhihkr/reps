using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.DATA.Entity
{
    public class AuthenticateIdentityContext : IdentityDbContext<MyIdentityUser>
    {
        public AuthenticateIdentityContext()
            : base("connectionstringAuth") // connnection to be used found in config file
        {

        }

    }

    public class MyIdentityUser : IdentityUser
    {

    }

    public class MyIdentityRole : IdentityRole
    {
        public MyIdentityRole()
        {

        }

        public MyIdentityRole(string roleName, string description)
            : base(roleName)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
