using HireHub.AllUsers.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace HireHub.Database.Queries
{
    class AccountQueries : DatabaseConnection
    {
        SqlConnection connection;

        public AccountQueries()
        {
            connection = new SqlConnection(dbConnectionString);
        }
        public bool IsAccountExist(string emailId)
        {
            connection.Open();
            string selectQuery = $"SELECT COUNT(*) from Account ac where ac.email = '{emailId}'";
            SqlCommand cmd = new SqlCommand(selectQuery, connection);

            int userCount = (int)cmd.ExecuteScalar();
            connection.Close();
            if (userCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddUserAccount(SignUpModel signUpModel)
        {
            connection.Open();

            string insertQuery = $"INSERT INTO Account (userType, firstName, lastName, email, phoneNumber, passphrase) VALUES ('{signUpModel.UserType}','{signUpModel.FirstName}','{signUpModel.LastName}', '{signUpModel.Email}', '{signUpModel.Phone}', '{signUpModel.Password}')";

            SqlCommand cmd = new SqlCommand(insertQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
        }


        // checks Account Table for matching email and password
        public bool ValidateLoginCredentials(Login login)
        {
            int matchCount = 0; //init
            string selectQuery = $"Select Count(*) FROM Account ac WHERE ac.email = '{login.EmailAddress}' AND ac.passphrase = '{login.Password}' AND ac.userType = '{login.UserType}'";

            connection.Open();
            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            matchCount = (int)cmd.ExecuteScalar();
            connection.Close();

            return (matchCount == 0 ? false : true);


        }
    }
}
