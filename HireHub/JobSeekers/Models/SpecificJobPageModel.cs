using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.JobSeekers.Models
{
    internal class SpecificJobPageModel
    {
        public IEnumerable<JobDetailModel>? specificJobDetails { get; set; }
    }
}
