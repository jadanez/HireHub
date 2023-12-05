using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.Employers.Models
{
    public class JobModel
    {
        public long JobId { get; set; }
        public long EmployerId { get; set; }
        public string JobStatus { get; set; }
        public string RoleName { get; set; }
        public string CompanyName { get; set; }
        public string JobType { get; set; }
        public string ExperienceLevel { get; set; }
        public string JobDetails { get; set; }
        public double Salary { get; set; }
        public string JobLocation { get; set; }
        public string HiringManager { get; set; }
    }
}
