using HireHub.AllUsers.Models;
using HireHub.Employers.Models;
using HireHub.JobSeekers.Models;
using HireHub.JobSeekers.Views;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        internal async Task<List<JobDetailModel>> SearchJob_All_Or_ByRoleName(string searchString)
        {
            List<JobDetailModel> jobDetails = new List<JobDetailModel>();

            SqlCommand cmd;
            if (String.IsNullOrEmpty(searchString) || searchString == "Generic")
            {
                string searchStringGeneric = $"SELECT * from Job";
                cmd = new SqlCommand(searchStringGeneric, connection);
                Debug.WriteLine("SQL SEARCH STRING IF:" + searchStringGeneric);
            }
            else
            {
                string selectQueryBasedOnSearchString = $"SELECT * from Job jobInstance where jobInstance.roleName LIKE '%{searchString}%'";
                cmd = new SqlCommand(selectQueryBasedOnSearchString, connection);
                Debug.WriteLine("SQL SEARCH STRING ELSE:" + selectQueryBasedOnSearchString);
            }

            try
            {
                await connection.OpenAsync();
                cmd.CommandTimeout = 100000;
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    Debug.WriteLine("Entered try, executed reader");
                    if (reader == null)
                    {
                        Debug.WriteLine("Reader is null");
                        throw new Exception("Something went wrong while performing serach. Contact your administrator.");
                    }
                    else
                    {
                        Debug.WriteLine("In else");
                        while (await reader.ReadAsync())
                        {
                            Debug.WriteLine("Reading ++");
                            JobDetailModel jobDetail = new JobDetailModel();

                            jobDetail.roleName = reader["roleName"].ToString();
                            jobDetail.jobDetails = reader["jobDetails"].ToString();
                            jobDetail.jobId = Convert.ToInt32(reader["jobId"]);
                            jobDetail.jobType = reader["jobType"].ToString();
                            jobDetail.experienceLevel = reader["experienceLevel"].ToString();
                            jobDetail.jobStatus = reader["jobStatus"].ToString();
                            jobDetail.hiringManager = reader["hiringManager"].ToString();
                            jobDetail.salary = "$ " + Convert.ToDecimal(reader["salary"]) + " a year";
                            jobDetail.companyName = reader["companyName"].ToString();
                            jobDetail.jobLocation = reader["jobLocation"].ToString();

                            jobDetails.Add(jobDetail);

                            Debug.WriteLine("Job Id " + jobDetail.jobId);
                            Debug.WriteLine("Role Name " + jobDetail.roleName);
                            Debug.WriteLine("Job Details " + jobDetail.jobDetails);
                        }
                        reader.NextResult();

                        reader.Close();
                        Debug.WriteLine("Reader close");
                        //JobSeekerHomepage.searchResult = jobDetails;

                       // int rowsAffected = (int)reader.RecordsAffected;

                        return jobDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }

        }

        internal async Task<List<JobDetailModel>> SearchJob_ByJobId(int jobId)
        {
            List<JobDetailModel> jobDetails = new List<JobDetailModel>();

            string selectQuery = $"SELECT TOP 1 * from Job jobInstance where jobInstance.jobId = {jobId}";
            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            Debug.WriteLine("SQL SEARCH STRING:" + selectQuery);

            try
            {
                await connection.OpenAsync();
                cmd.CommandTimeout = 100000;
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    Debug.WriteLine("Entered try, executed reader");
                    if (reader == null)
                    {
                        Debug.WriteLine("Reader is null");
                        throw new Exception("Something went wrong while performing search. Contact your administrator.");
                    }
                    else
                    {
                        Debug.WriteLine("In else");
                        while (await reader.ReadAsync())
                        {
                            Debug.WriteLine("Reading ++");
                            JobDetailModel jobDetail = new JobDetailModel();

                            jobDetail.roleName = reader["roleName"].ToString();
                            jobDetail.jobDetails = reader["jobDetails"].ToString();
                            jobDetail.jobId = Convert.ToInt32(reader["jobId"]);
                            jobDetail.jobType = reader["jobType"].ToString();
                            jobDetail.experienceLevel = reader["experienceLevel"].ToString();
                            jobDetail.jobStatus = reader["jobStatus"].ToString();
                            jobDetail.hiringManager = reader["hiringManager"].ToString();
                            jobDetail.salary = "$ " + Convert.ToDecimal(reader["salary"]) + " a year";
                            jobDetail.companyName = reader["companyName"].ToString();
                            jobDetail.jobLocation = reader["jobLocation"].ToString();

                            jobDetails.Add(jobDetail);

                            Debug.WriteLine("Job Id " + jobDetail.jobId);
                            Debug.WriteLine("Role Name " + jobDetail.roleName);
                            Debug.WriteLine("Job Details " + jobDetail.jobDetails);
                        }
                        reader.NextResult();

                        reader.Close();
                        Debug.WriteLine("Reader close");
                        return jobDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
