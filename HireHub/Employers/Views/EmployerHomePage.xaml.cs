using HireHub.AllUsers.Models;
using HireHub.Database.Queries;
using HireHub.JobSeekers;
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

namespace HireHub.Employers.Views
{
    /// <summary>
    /// Interaction logic for EmployerHomePage.xaml
    /// </summary>
    public partial class EmployerHomePage : Window
    {
        public string empFirstName;
        public string empLastName;
        public string empEmail;
        public long userId;


        public EmployerHomePage(string emailAddress, string userType)
        {
            AccountQueries currentAccount = new AccountQueries();

            Account accountDetails = currentAccount.getAccountDetails(emailAddress, userType);
            this.empFirstName = accountDetails.FirstName;
            this.empLastName = accountDetails.LastName;
            this.empEmail = accountDetails.Email;
            this.userId = accountDetails.AccountId;

            InitializeComponent();
        }



        //set profile Name
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            EmpProfileName.Content = empFirstName;

        }



        //add new jobs
        private void AddJobs_Click(object sender, RoutedEventArgs e)
        {
            /*MessageBox.Show(empFirstName +  userId.ToString() + empEmail);*/
            EmployerAddNewJob addNewJob = new EmployerAddNewJob(userId,empEmail, empFirstName);
            this.Visibility = Visibility.Hidden;
            addNewJob.Show();
        }



        //your jobs
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Your jobs!");
        }


        //Edit Profile
        private void Profile_Click (object sender, MouseButtonEventArgs e)
        {

            MessageBox.Show("Edit Profile");
        }
    }
}
