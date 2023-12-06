using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.JobSeekers.Models
{
    class JobSeekerEditAccountModel
    {
        public long accountId { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userEmailName { get; set; }
        public string userPhoneNumber { get; set; }
    }
}
