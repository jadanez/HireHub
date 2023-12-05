using HireHub.Common;
using HireHub.Common.Models;
using HireHub.Database.Queries;
using HireHub.JobSeekers.Models;
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
        public JobSeekerEditProfile(long userId, string userEmailId)
        {
            this.userId = userId;
            this.userEmailId = userEmailId;
            InitializeComponent();
            InitializeViewValues();
        }

        private void InitializeViewValues()
        {
            string[] employementStatus = { "Contract Employee", "Full-Time Employee", "Intern or Apprentice", "Part-Time Employee", "Self-Employed", "Temporary or Seasonal Employee", "Unemployed", "Volunteer" };
            EmploymentStatusCombo.ItemsSource = employementStatus.ToList();

        }
        private async void SaveProfileDetailsBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void SaveAccountDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            // Account
            FormErrorMessages formErrorMessages = ValidateAccountFields();
            if (formErrorMessages != null)
            {
                if (formErrorMessages.isFormValid)
                {
                    AccountQueries accountQueries = new AccountQueries();
                    AccountModel accountModel = new AccountModel()
                    {
                        FirstName = FirstNameTxtBox.Text,
                        LastName = LastNameTxtBox.Text,
                        Email = EmailTxtBox.Text,
                        Phone = PhoneTxtBox.Text
                    };
                    bool isSuccess = await accountQueries.UpdateUserNameAndContactDetails(accountModel, userEmailId);
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
    }
}
