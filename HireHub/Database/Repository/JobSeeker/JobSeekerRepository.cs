using HireHub.JobSeekers.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.Database.Repository.JobSeeker
{
    class JobSeekerRepository : DatabaseConnection
    {
        SqlConnection connection;

        public JobSeekerRepository()
        {
            connection = new SqlConnection(dbConnectionString);
        }
        public bool IsAccountExist(string emailId)
        {
            return true;
        }
        public void AddUser(SignUpModel signUpModel)
        {
            connection.Open();

            string insertQuery = $"INSERT INTO Account (userType, firstName, lastName, email, phoneNumber, passphrase) VALUES ('Job Seeker',{signUpModel.FirstName},{signUpModel.LastName}, {signUpModel.Email}, {signUpModel.Phone}, {signUpModel.Password})";

            SqlCommand cmd = new SqlCommand(insertQuery, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
