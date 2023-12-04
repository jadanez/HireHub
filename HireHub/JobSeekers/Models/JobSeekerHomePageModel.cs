using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.JobSeekers.Models
{
    internal class JobSeekerHomePageModel
    {
       
        public IEnumerable<JobDetail>? jobDetails { get; set; }
        public string searchString { get; set; }
    }


    public class JobDetail
    {
        public string roleName { get; set; }
        public int jobId { get; set; }

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
