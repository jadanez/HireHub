using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.Employers.Models
{
    public class JobApplicant
    {
        public long JobId { get; set; }
        public long ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string ApplicantButtonName => $"btnApplicantId{ApplicantId}";


    }
}
