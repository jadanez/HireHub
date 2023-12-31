﻿using HireHub.AllUsers.Models;
using HireHub.Employers.Models;
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
using System.Data;
using HireHub.JobSeekers.Views;

namespace HireHub.Employers.Views
{
    /// <summary>
    /// Interaction logic for EmployerSeeApplicants.xaml
    /// </summary>
    public partial class EmployerSeeApplicants : Window

    {
        public List<JobApplicant> myApplicants;
        public string empFirstName;
        public string empLastName;
        public string empEmail;
        public string jobName;
        public long  userId;
        public string userType;
        public long jobId;






        public EmployerSeeApplicants(string btnName, string jobName, string empFirstName, string empLastName, string empEmail, long userId, string userType)
        {
            this.empFirstName = empFirstName;
            this.empLastName = empLastName;
            this.empEmail = empEmail;
            this.jobName = jobName;
            this.userId = userId;
            this.userType = userType;
            this.jobId = long.Parse(btnName.Substring(6));

            InitializeComponent();
        }


       


        //add new jobs
        private void AddJobs_Click(object sender, RoutedEventArgs e)
        {
           
            EmployerAddNewJob addNewJob = new EmployerAddNewJob(userId, empEmail, empFirstName);
            this.Visibility = Visibility.Hidden;
            addNewJob.Show();
        }



        //your jobs
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            EmployerHomePage empHomePage = new EmployerHomePage(empEmail, userType);
            this.Visibility = Visibility.Hidden;
            empHomePage.Show();


        }

        // dyncamically name buttons
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is JobApplicant jobApplicant)
            {
                button.Name = jobApplicant.ApplicantButtonName;
            }
        }



        private async void ApproveButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string status = button.Content.ToString();
                

                if (button.DataContext is JobApplicant jobApplicant)
                {
                    try
                    {
                        ApplicantQueries updateApplicant = new ApplicantQueries();
                        bool result = await updateApplicant.UpdateStatus(jobApplicant.ApplicantId, status);

                        if (result)
                        {
                            MessageBox.Show($"{jobApplicant.ApplicantName} has been \"{status}d\".");
                            button.Content = (button.Content.ToString() == "Approve") ? "Disapprove" : "Approve";
                            
                        }
                        else
                        {
                            MessageBox.Show($"Error occurred while updating status for {jobApplicant.ApplicantName}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
        }













        //Edit Profile
        private void EmpProfileName_Click(object sender, RoutedEventArgs e)
        {
            
            
            JobSeekerEditProfile editProfile = new JobSeekerEditProfile(empEmail);
            this.Visibility = Visibility.Hidden;
            editProfile.Show();
        }


        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            //Edit Profile / Show
            JobSeekerEditProfile editProfile = new JobSeekerEditProfile(empEmail);
            this.Visibility = Visibility.Hidden;
            editProfile.Show();
        }


        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            EmpProfileName.Content = empFirstName;


            try
            {

                ApplicantQueries getApplicants = new ApplicantQueries();
                myApplicants = await getApplicants.GetApplicants(jobId);

                // Set the data to the DataGrid
                applicantDataGrid.ItemsSource = myApplicants;

                JobName.Text = $"{jobName} (Job Id: {jobId})";

             




            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
                




        }
        private void Logout_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }








    }
}
