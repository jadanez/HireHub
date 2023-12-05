using HireHub.AllUsers.Models;
using HireHub.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public async Task<bool> IsAccountExist(string emailId)
        {
            try
            {
                connection.Open();
                string selectQuery = $"SELECT COUNT(*) from Account ac where ac.email = '{emailId}'";
                SqlCommand cmd = new SqlCommand(selectQuery, connection);

                int userCount = (int)await cmd.ExecuteScalarAsync();
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
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> AddUserAccount(SignUpModel signUpModel)
        {
            try
            {
                connection.Open();

                string insertQuery = $"INSERT INTO Account (userType, firstName, lastName, email, phoneNumber, passphrase) VALUES ('{signUpModel.UserType}','{signUpModel.FirstName}','{signUpModel.LastName}', '{signUpModel.Email}', '{signUpModel.Phone}', '{signUpModel.Password}')";

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
        // get Account ID by emailID
        public async Task<long> GetUserAccountId(string emailId)
        {
            try
            {
                string selectQuery = $"Select ac.accountID FROM Account ac WHERE ac.email = '{emailId}'";

                connection.Open();
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                await cmd.ExecuteNonQueryAsync();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                string userAccountIdString = dt.Rows[0]["accountId"].ToString();
                if (int.TryParse(userAccountIdString, out int Id))
                {
                    return Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        // get Account Details by emailID
        public async Task<AccountModel> GetUserAccountDetails(string emailId)
        {
            AccountModel account = null;
            try
            {
                string selectQuery = $"Select * FROM Account ac WHERE ac.email = '{emailId}'";

                connection.Open();
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                await cmd.ExecuteNonQueryAsync();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                string userAccountIdString = dt.Rows[0]["accountId"].ToString();
                string userFirstNameString = dt.Rows[0]["firstName"].ToString();
                string userLastNameString = dt.Rows[0]["lastName"].ToString();
                string userEmailString = dt.Rows[0]["email"].ToString();
                string userPhoneNumberString = dt.Rows[0]["phoneNumber"].ToString();
                string userType = dt.Rows[0]["userType"].ToString();
                int userId;
                if (int.TryParse(userAccountIdString, out int Id))
                {
                    userId = Id;
                }
                else
                {
                    userId = 0;
                }
                account = new AccountModel()
                {
                    AccountID = userId,
                    FirstName = userFirstNameString,
                    LastName = userLastNameString,
                    Email = userEmailString,
                    Phone = userPhoneNumberString,
                    UserType = userType
                };
                return account;
            }
            catch (Exception ex)
            {
                return account;
            }
        }
        public async Task<bool> UpdateUserNameAndContactDetails(AccountModel accountModel, string emailID)
        {
            try
            {
                connection.Open();

                string updateQuery = $"UPDATE ACCOUNT SET firstName='{accountModel.FirstName}', lastName='{accountModel.LastName}', email='{accountModel.Email}', phoneNumber='{accountModel.Phone}' where email='{emailID}'";

                SqlCommand cmd = new SqlCommand(updateQuery, connection);

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
