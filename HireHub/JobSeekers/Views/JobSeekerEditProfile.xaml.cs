using HireHub.Common;
using HireHub.Common.Models;
using HireHub.Database.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HireHub.JobSeekers.Views
{
    /// <summary>
    /// Interaction logic for JobSeekerEditProfile.xaml
    /// </summary>
    public partial class JobSeekerEditProfile : Window
    {
        long userId;
        string userEmailId;
        AccountQueries accountQueries;
        ProfileQueries profileQueries;
        bool isProfileExist;
        public JobSeekerEditProfile(string userEmailId)
        {

            this.userEmailId = userEmailId;
            accountQueries = new AccountQueries();
            profileQueries = new ProfileQueries();
            InitializeComponent();
            InitializeViewValues();
        }

        private async void InitializeViewValues()
        {
            string[] employementStatus = { "Contract Employee", "Full-Time Employee", "Intern or Apprentice", "Part-Time Employee", "Self-Employed", "Temporary or Seasonal Employee", "Unemployed", "Volunteer" };
            EmploymentStatusCombo.ItemsSource = employementStatus.ToList();

            AccountModel accountModel = await accountQueries.GetUserAccountDetails(userEmailId);
            FirstNameTxtBox.Text = accountModel.FirstName;
            LastNameTxtBox.Text = accountModel.LastName;
            EmailTxtBox.Text = accountModel.Email;
            PhoneTxtBox.Text = accountModel.Phone;
            userId = accountModel.AccountID;
            isProfileExist = await profileQueries.IsProfileExist(userId);
            if (isProfileExist)
            {
                ProfileModel jobSeekerProfileModel = await profileQueries.GetUserProfileDetails(userId);
                if (jobSeekerProfileModel != null)
                {
                    ProfileHeaderTextBox.Text = jobSeekerProfileModel.profileHeader;
                    CurrentCompanyTxtBox.Text = jobSeekerProfileModel.currentCompany;
                    ExpectedSalaryTxtBox.Text = jobSeekerProfileModel.expectedSalary.ToString();
                    TargetRoleTxtBox.Text = jobSeekerProfileModel.targetRole;
                    EmployementHistoryTxtBox.Text = jobSeekerProfileModel.employmentHistory;
                    CurrentRoleTxtBox.Text = jobSeekerProfileModel.currentRole;
                    for (int i = 0; i < employementStatus.Length; i++)
                    {
                        if (employementStatus[i].Equals(jobSeekerProfileModel.employmentStatus))
                        {
                            EmploymentStatusCombo.SelectedIndex = i;
                        }
                    }
                }

            }
        }
        private async void SaveProfileDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            FormErrorMessages formErrorMessages = ValidateProfileFields();
            if (formErrorMessages != null)
            {
                if (formErrorMessages.isFormValid)
                {
                    ProfileModel profileModel = new ProfileModel()
                    {
                        accountId = userId,
                        profileHeader = ProfileHeaderTextBox.Text,
                        currentCompany = CurrentCompanyTxtBox.Text,
                        currentRole = CurrentRoleTxtBox.Text,
                        targetRole = TargetRoleTxtBox.Text,
                        employmentStatus = EmploymentStatusCombo.SelectedItem.ToString(),
                        employmentHistory = EmployementHistoryTxtBox.Text,
                        expectedSalary = double.Parse(ExpectedSalaryTxtBox.Text)
                    };
                    if (isProfileExist)
                    {
                        bool isUpdated = await profileQueries.UpdateProfileDetails(profileModel, userId);
                        if (isUpdated)
                        {
                            MessageBox.Show(ProfileFormConstants.ProfileUpdated, ProfileFormConstants.ProfileUpdated, MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(ProfileFormConstants.FailedToAddProfile, ProfileFormConstants.FailedToAddProfile, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        bool isInserted = await profileQueries.InsertProfileDetails(profileModel);
                        if (isInserted)
                        {
                            MessageBox.Show(ProfileFormConstants.ProfileAdded, ProfileFormConstants.ProfileAdded, MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(ProfileFormConstants.FailedToAddProfile, ProfileFormConstants.FailedToAddProfile, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(formErrorMessages.errorMessage, ProfileFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private FormErrorMessages ValidateProfileFields()
        {
            FormErrorMessages formErrorMessages = new FormErrorMessages();
            formErrorMessages.isFormValid = true;
            if (String.IsNullOrEmpty(ProfileHeaderTextBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = ProfileFormConstants.InValidProfileHeader;
            }
            if (String.IsNullOrEmpty(TargetRoleTxtBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = ProfileFormConstants.InValidTargetRole;
            }
            if (String.IsNullOrEmpty(ExpectedSalaryTxtBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = ProfileFormConstants.InValidEmptyExpectedSalary;
            }
            if (!FieldValidators.IsDecimalNumber(ExpectedSalaryTxtBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = ProfileFormConstants.InValidExpectedSalary;
            }
            if (EmploymentStatusCombo.SelectedItem == null)
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = ProfileFormConstants.InValidEmployementStatus;
            }
            return formErrorMessages;
        }

        private async void SaveAccountDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            // Account
            FormErrorMessages formErrorMessages = ValidateAccountFields();
            if (formErrorMessages != null)
            {
                if (formErrorMessages.isFormValid)
                {
                    AccountModel accountModel = new AccountModel()
                    {
                        FirstName = FirstNameTxtBox.Text,
                        LastName = LastNameTxtBox.Text,
                        Email = EmailTxtBox.Text,
                        Phone = PhoneTxtBox.Text
                    };
                    if (!accountModel.Email.Equals(userEmailId))
                    {
                        bool isExist = await accountQueries.IsAccountExist(accountModel.Email);
                        if (isExist)
                        {
                            MessageBox.Show(AccountFormConstants.EmailIdExist, AccountFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    bool isSuccess = await accountQueries.UpdateUserNameAndContactDetails(accountModel, userEmailId);
                    if (isSuccess)
                    {
                        MessageBox.Show(AccountFormConstants.AccountUpdated, AccountFormConstants.AccountUpdated, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(AccountFormConstants.FailedToAddAccount, AccountFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(formErrorMessages.errorMessage, AccountFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private FormErrorMessages ValidateAccountFields()
        {
            FormErrorMessages formErrorMessages = new FormErrorMessages();
            formErrorMessages.isFormValid = true;
            if (!FieldValidators.AreAlphabets(FirstNameTxtBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidFirstName;
            }
            if (!FieldValidators.AreAlphabets(LastNameTxtBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidLastName;
            }
            if (!FieldValidators.IsMailValid(EmailTxtBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidEmailName;
            }
            if (!FieldValidators.IsPhoneNumberValid(PhoneTxtBox.Text))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidPhoneName;
            }
            return formErrorMessages;
        }
        private void BackBtnToHomePage_Click(object sender, EventArgs e)
        {
            JobSeekerHomepage homePage = new JobSeekerHomepage(userEmailId);
            this.Visibility = Visibility.Hidden;
            homePage.Show();
        }
        private void LogOut_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
    }
}
