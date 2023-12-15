using HireHub.AllUsers.Models;
using HireHub.Common;
using HireHub.Common.Models;
using HireHub.Database.Queries;
using HireHub.Employers.Views;
using HireHub.JobSeekers.Views;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace HireHub.JobSeekers
{
    /// <summary>
    /// Interaction logic for SignUpForm.xaml
    /// </summary>
    public partial class SignUpForm : Window
    {
        private string SignUpAsRBtnValue { get; set; }
        AccountQueries accountQueries;
        public SignUpForm()
        {
            accountQueries = new AccountQueries();
            InitializeComponent();
        }

        private async void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            FormErrorMessages formErrorMessages = GetAllFieldValues();
            if (formErrorMessages != null)
            {
                if (formErrorMessages.isFormValid)
                {
                    SignUpModel signUpModel = new SignUpModel()
                    {
                        Email = EmailTxtBox.Text,
                        FirstName = FirstNameTxtBox.Text,
                        LastName = LastNameTxtBox.Text,
                        Phone = PhoneTxtBox.Text,
                        Password = PasswordTxtBox.Password,
                        UserType = SignUpAsRBtnValue

                    };
                    bool isEmailExist = (bool)await accountQueries.IsAccountExist(signUpModel.Email);
                    if (isEmailExist)
                    {
                        MessageBox.Show(AccountFormConstants.EmailIdExist, AccountFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        bool isAccountAdded = (bool)await accountQueries.AddUserAccount(signUpModel);
                        if (isAccountAdded)
                        {
                            {
                                long UserId = await GetUserId(signUpModel.Email);
                                MessageBox.Show(AccountFormConstants.WelcomeMessage + " " + signUpModel.FirstName, AccountFormConstants.AccountAdded, MessageBoxButton.OK, MessageBoxImage.Information);
                                if (signUpModel.UserType == AccountFormConstants.Employer)
                                {
                                    EmployerAddNewJob employerAddNewJob = new EmployerAddNewJob(UserId, signUpModel.Email, signUpModel.FirstName);
                                    this.Visibility = Visibility.Hidden;
                                    employerAddNewJob.Show();
                                }
                                else
                                {
                                    JobSeekerEditProfile editProfilePage = new JobSeekerEditProfile(signUpModel.Email);
                                    this.Visibility = Visibility.Hidden;
                                    editProfilePage.Show();

                                    //JobSeekerHomepage jobSeekerHomepage = new JobSeekerHomepage(signUpModel.Email);
                                    //this.Visibility = Visibility.Hidden;
                                    //jobSeekerHomepage.Show();
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show(AccountFormConstants.FailedToAddAccount, AccountFormConstants.Failure, MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }


                }
                else
                {
                    MessageBox.Show(formErrorMessages.errorMessage, AccountFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Back_BTN_Login(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow=new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
        public async Task<long> GetUserId(string emailId)
        {
            long userAccountId = await accountQueries.GetUserAccountId(emailId);
            return userAccountId;
        }

        private FormErrorMessages GetAllFieldValues()
        {
            FormErrorMessages formErrorMessages = new FormErrorMessages();
            formErrorMessages.isFormValid = true;
            string firstName = FirstNameTxtBox.Text;
            if (!FieldValidators.AreAlphabets(firstName))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidFirstName;
                return formErrorMessages;
            }

            string lastName = LastNameTxtBox.Text;
            if (!FieldValidators.AreAlphabets(lastName))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidLastName;
                return formErrorMessages;
            }
            string emailAddress = EmailTxtBox.Text;
            if (!FieldValidators.IsMailValid(emailAddress))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidEmailName;
                return formErrorMessages;
            }
            string phoneNumber = PhoneTxtBox.Text;
            if (!FieldValidators.IsPhoneNumberValid(phoneNumber))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidPhoneName;
                return formErrorMessages;
            }
            string password = PasswordTxtBox.Password;
            if (!FieldValidators.IsPasswordValid(password))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidPasswordName;
                return formErrorMessages;
            }
            string cPassword = CPasswordTxtBox.Password;
            if (!FieldValidators.ConfirmPasswordMatched(cPassword, password))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidCPasswordName;
                return formErrorMessages;
            }
            if (String.IsNullOrEmpty(SignUpAsRBtnValue))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidUserType;
                return formErrorMessages;
            }
            return formErrorMessages;
        }

        private void RBTNEmployer_Click(object sender, RoutedEventArgs e)
        {
            SignUpAsRBtnValue = AccountFormConstants.Employer;
        }

        private void RBTNJobSeeker_Click(object sender, RoutedEventArgs e)
        {
            SignUpAsRBtnValue = AccountFormConstants.JobSeeker;
        }
    }
}
