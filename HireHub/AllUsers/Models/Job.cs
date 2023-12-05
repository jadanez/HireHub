using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.AllUsers.Models
{
    internal class Job
    {

        public int jobId { get; set; }
        public int employerId { get; set; }
        public string roleName { get; set; }
       
        public string jobStatus { get; set; }
        public string jobDetails { get; set; }

        public string companyName { get; set; }

        public string experienceLevel { get; set; }

        public string jobType { get; set; }

        public string jobLocation { get; set; }

        public decimal salary { get; set; }

        public string hiringManager { get; set; }



    }
}
