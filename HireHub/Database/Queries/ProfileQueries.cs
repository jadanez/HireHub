using HireHub.AllUsers.Models;
using HireHub.Common.Models;
using HireHub.JobSeekers.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HireHub.Database.Queries
{
    public class ProfileQueries : DatabaseConnection
    {
        SqlConnection connection;

        public ProfileQueries()
        {
            connection = new SqlConnection(dbConnectionString);
        }
        public async Task<bool> IsProfileExist(long accountID)
        {
            try
            {
                connection.Open();
                string selectQuery = $"SELECT COUNT(*) from Profile pr where pr.accountID = '{accountID}'";
                SqlCommand cmd = new SqlCommand(selectQuery, connection);

                int profileCount = (int)await cmd.ExecuteScalarAsync();
                connection.Close();
                if (profileCount > 0)
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
        public async Task<ProfileModel> GetUserProfileDetails(long accountID)
        {
            ProfileModel profileModel = null;
            try
            {
                string selectQuery = $"Select * FROM Profile ac WHERE ac.accountID = '{accountID}'";

                connection.Open();
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                await cmd.ExecuteNonQueryAsync();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                string userProfileHeaderString = dt.Rows[0]["profileHeader"].ToString();
                string userCurrentRoleString = dt.Rows[0]["currentRole"].ToString();
                string userTargetRoleString = dt.Rows[0]["targetRole"].ToString();
                string userCurrentCompanyString = dt.Rows[0]["currentCompany"].ToString();
                string userExpectedSalaryString = dt.Rows[0]["expectedSalary"].ToString();
                string employmentStatusString = dt.Rows[0]["employmentStatus"].ToString();
                string employmentHistoryString = dt.Rows[0]["employmentHistory"].ToString();
                string profileIDString = dt.Rows[0]["profileID"].ToString();
                long profileID;
                connection.Close();
                if (long.TryParse(profileIDString, out long Id))
                {
                    profileID = Id;
                }
                else
                {
                    profileID = 0;
                }
                double expectedSalary;
                if (double.TryParse(userExpectedSalaryString, out double salary))
                {
                    expectedSalary = salary;
                }
                else
                {
                    expectedSalary = 0;
                }
                profileModel = new ProfileModel()
                {
                    profileId = profileID,
                    accountId = accountID,
                    profileHeader = userProfileHeaderString,
                    currentRole = userCurrentRoleString,
                    targetRole = userTargetRoleString,
                    currentCompany = userCurrentCompanyString,
                    employmentStatus = employmentStatusString,
                    employmentHistory = employmentHistoryString,
                    expectedSalary = expectedSalary
                };

                return profileModel;
            }
            catch (Exception ex)
            {
                return profileModel;
            }
        }
        public async Task<bool> UpdateProfileDetails(ProfileModel profileModel, long accountID)
        {
            try
            {
                connection.Open();

                string updateQuery = $"UPDATE Profile SET profileHeader='{profileModel.profileHeader}', currentRole='{profileModel.currentRole}', targetRole='{profileModel.targetRole}', currentCompany='{profileModel.currentRole}', expectedSalary='{profileModel.expectedSalary}', employmentStatus='{profileModel.employmentStatus}', employmentHistory='{profileModel.employmentHistory}' where accountID='{accountID}'";

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
        public async Task<bool> InsertProfileDetails(ProfileModel profileModel)
        {
            try
            {
                connection.Open();

                string insertQuery = $"INSERT INTO Profile (accountID,profileHeader,currentRole,targetRole,currentCompany,expectedSalary,employmentStatus,employmentHistory) VALUES ('{profileModel.accountId}','{profileModel.profileHeader}','{profileModel.currentRole}', '{profileModel.targetRole}', '{profileModel.currentCompany}', '{profileModel.expectedSalary}','{profileModel.employmentStatus}','{profileModel.employmentHistory}')";

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
