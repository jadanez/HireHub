using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HireHub.AllUsers.Models
{
    internal class Login
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; }

    }
}
