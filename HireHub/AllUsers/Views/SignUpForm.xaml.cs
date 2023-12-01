using HireHub.AllUsers.Models;
using HireHub.Common;
using HireHub.Database.Queries;
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
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            FormErrorMessages formErrorMessages = GetAllFieldValues();
            if (formErrorMessages != null)
            {
                if (formErrorMessages.isFormValid)
                {
                    AccountQueries jobSeekerRepository = new AccountQueries();
                    SignUpModel signUpModel = new SignUpModel()
                    {
                        Email = EmailTxtBox.Text,
                        FirstName = FirstNameTxtBox.Text,
                        LastName = LastNameTxtBox.Text,
                        Phone = PhoneTxtBox.Text,
                        Password = PasswordTxtBox.Text,
                        UserType = SignUpAsRBtnValue

                    };
                    bool isEmailExist = jobSeekerRepository.IsAccountExist(signUpModel.Email);
                    if (isEmailExist)
                    {
                        MessageBox.Show(SignUpFormConstants.EmailIdExist, SignUpFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        jobSeekerRepository.AddUserAccount(signUpModel);
                        MessageBox.Show(SignUpFormConstants.WelcomeMessage + " " + signUpModel.FirstName, SignUpFormConstants.AccountAdded, MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                }
                else
                {
                    MessageBox.Show(formErrorMessages.errorMessage, SignUpFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private FormErrorMessages GetAllFieldValues()
        {
            FormErrorMessages formErrorMessages = new FormErrorMessages();
            formErrorMessages.isFormValid = true;
            string firstName = FirstNameTxtBox.Text;
            if (!FieldValidators.AreAlphabets(firstName))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = SignUpFormConstants.InValidFirstName;
                return formErrorMessages;
            }

            string lastName = LastNameTxtBox.Text;
            if (!FieldValidators.AreAlphabets(lastName))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = SignUpFormConstants.InValidLastName;
                return formErrorMessages;
            }
            string emailAddress = EmailTxtBox.Text;
            if (!FieldValidators.IsMailValid(emailAddress))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = SignUpFormConstants.InValidEmailName;
                return formErrorMessages;
            }
            string phoneNumber = PhoneTxtBox.Text;
            if (!FieldValidators.IsPhoneNumberValid(phoneNumber))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = SignUpFormConstants.InValidPhoneName;
                return formErrorMessages;
            }
            string password = PasswordTxtBox.Text;
            if (!FieldValidators.IsPasswordValid(password))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = SignUpFormConstants.InValidPasswordName;
                return formErrorMessages;
            }
            string cPassword = CPasswordTxtBox.Text;
            if (!FieldValidators.ConfirmPasswordMatched(cPassword, password))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = SignUpFormConstants.InValidCPasswordName;
                return formErrorMessages;
            }
            if (String.IsNullOrEmpty(SignUpAsRBtnValue))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = SignUpFormConstants.InValidUserType;
                return formErrorMessages;
            }
            return formErrorMessages;
        }

        private void RBTNEmployer_Click(object sender, RoutedEventArgs e)
        {
            SignUpAsRBtnValue = SignUpFormConstants.Employer;
        }

        private void RBTNJobSeeker_Click(object sender, RoutedEventArgs e)
        {
            SignUpAsRBtnValue = SignUpFormConstants.JobSeeker;
        }
    }
}
