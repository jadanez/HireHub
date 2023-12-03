using HireHub.AllUsers.Models;
using HireHub.Employers.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.Database.Queries
{
    public class JobQueries : DatabaseConnection
    {
        SqlConnection connection;

        public JobQueries()
        {
            connection = new SqlConnection(dbConnectionString);
        }
        public async Task<bool> AddAJob(JobModel jobModel)
        {
            try
            {
                connection.Open();
                string insertQuery = $"INSERT INTO Job (employerID, jobStatus, jobType, roleName, companyName, experienceLevel, jobDetails, salary, jobLocation, hiringManager) VALUES ('{jobModel.EmployerId}','{jobModel.JobStatus}','{jobModel.JobType}', '{jobModel.RoleName}', '{jobModel.CompanyName}','{jobModel.ExperienceLevel}','{jobModel.JobDetails}', '{jobModel.Salary}', '{jobModel.JobLocation}','{jobModel.HiringManager}')";
                SqlCommand cmd = new SqlCommand(insertQuery, connection);

                int rowAffected = (int)await cmd.ExecuteNonQueryAsync();
                connection.Close();

                ////Updated
                return (rowAffected > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
