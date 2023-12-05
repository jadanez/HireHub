using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.JobSeekers.Models
{
    class JobSeekerEditProfileModel
    {
        public long profileId { get; set; }
        public long accountId { get; set; }
        public string profileHeader { get; set; }
        public string currentRole { get; set; }
        public string targetRole { get; set; }
        public string currentCompany { get; set; }
        public double expectedSalary { get; set; }
        public string employmentStatus { get; set; }
        public string employmentHistory { get; set; }

    }
}
