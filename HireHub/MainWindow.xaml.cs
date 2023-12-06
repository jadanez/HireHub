using HireHub;
using HireHub.Common;
using HireHub.JobSeekers;
using HireHub.AllUsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography.Xml;
using HireHub.Database.Queries;
using HireHub.JobSeekers.Views;
using HireHub.Employers.Views;

namespace HireHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // If user clicks on sign up button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SignUpForm signUpForm = new SignUpForm();
            this.Visibility = Visibility.Hidden;
            signUpForm.Show();
        }


        // If user clicks on login button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            FormErrorMessages formErrorMessages = new FormErrorMessages();
            formErrorMessages.isFormValid = true;

            //get inputs
            string emailLogin = EmailAddressLogin.Text;
            string passwordLogin = PasswordLogin.Password;
            string userType = "";

            //validate inputs

            //check email
            if (!FieldValidators.IsMailValid(emailLogin))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = AccountFormConstants.InValidEmailName;         
                
            }

            //check password

            if (String.IsNullOrEmpty(passwordLogin))
            {
               
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage += "\n Missing password.";
               
            }


            //check radio buttons

            if (!EmployerLogin.IsChecked.HasValue || !JobSeekerLogin.IsChecked.HasValue ||
                 (EmployerLogin.IsChecked.Value == false && JobSeekerLogin.IsChecked.Value == false))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage += "\n Please select either Employer or Job Seeker.";

            }
            else
            {
                userType = EmployerLogin.IsChecked.Value == true ? "Employer" : "Job Seeker";

            }


            if (!String.IsNullOrEmpty(formErrorMessages.errorMessage))
            {
                MessageBox.Show(formErrorMessages.errorMessage);
            }
            else
            {
                Login login = new Login()
                {
                    EmailAddress = emailLogin,
                    Password = passwordLogin,
                    UserType = userType,
                };

                AccountQueries loginCheck = new AccountQueries();
                bool validCredentials = loginCheck.ValidateLoginCredentials(login);


                if (validCredentials)
                {
                    //open homepage
                   /* MessageBox.Show("Success");*/
                    if (login.UserType == "Job Seeker")
                    {
                        JobSeekerHomepage jobSeekerHomepage = new JobSeekerHomepage();
                        this.Visibility = Visibility.Hidden;
                        jobSeekerHomepage.Show();

                    }
                    else
                    {
                        EmployerHomePage employerHomePage = new EmployerHomePage(login.EmailAddress, login.UserType);
                        this.Visibility = Visibility.Hidden;
                        employerHomePage.Show();
                    }

                    //reset values of inputs
                    
                   /* EmailAddressLogin.Text = null;
                    PasswordLogin.Password = null;
                    EmployerLogin.IsChecked = false;
                    JobSeekerLogin.IsChecked = false;*/



                }
                else
                {
                    
                    MessageBox.Show("Invalid Credentials");
                }



            }




        }

    }
}
