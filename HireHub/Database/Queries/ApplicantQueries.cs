using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HireHub.AllUsers.Models;
using HireHub.Employers.Models;


namespace HireHub.Database.Queries
{
    internal class ApplicantQueries : DatabaseConnection
    {
        SqlConnection connection;

        public ApplicantQueries()
        {
            connection = new SqlConnection(dbConnectionString);
        }

        public async Task<List<JobApplicant>> GetApplicants(long jobID)
        {
            List<JobApplicant> myApplicants = new List<JobApplicant>();

            try
            {
                string selectQuery = $"SELECT al.applicantID, al.applicantName, al.expectedSalary\r\nFROM ApplicantsList al\r\nWHERE al.jobID = {jobID};";

                connection.Open();
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    JobApplicant jobApplicant = new JobApplicant();
                    jobApplicant.ApplicantId = reader.GetInt32(reader.GetOrdinal("applicantID"));
                    jobApplicant.ApplicantName = reader.GetString(reader.GetOrdinal("applicantName"));
                    jobApplicant.ExpectedSalary = reader.GetDecimal(reader.GetOrdinal("expectedSalary"));


                    myApplicants.Add(jobApplicant);

                }

                connection.Close();




            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            return myApplicants;


        }








    }
}
