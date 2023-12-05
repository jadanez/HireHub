using HireHub.AllUsers.Models;
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

namespace HireHub.Employers.Views
{
    /// <summary>
    /// Interaction logic for EmployerSeeApplicants.xaml
    /// </summary>
    public partial class EmployerSeeApplicants : Window

    {

        public string empFirstName;
        public string empLastName;
        public string empEmail;
        public long userId;
        public string userType;
        public long jobId;

        public EmployerSeeApplicants(string btnName, string empFirstName, string empLastName, string empEmail, long userId, string userType)
        {
            this.empFirstName = empFirstName;
            this.empLastName = empLastName;
            this.empEmail = empEmail;
            this.userId = userId;
            this.userType = userType;
            this.jobId = long.Parse(btnName.Substring(6));

            InitializeComponent();
        }


       


        //add new jobs
        private void AddJobs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add jobs");
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


            




        //Edit Profile
        private void EmpProfileName_Click(object sender, RoutedEventArgs e)
        {
            //Edit Profile / Show
            MessageBox.Show("Edit Profile Call");
        }


        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            //Edit Profile / Show
            MessageBox.Show("Edit Profile Call: Image");
        }


        private async void Window_ContentRendered(object sender, EventArgs e)
        {

            /* MessageBox.Show("Here");*/

            



            try
            {
                ApplicantQueries getApplicants = new ApplicantQueries();
                List<JobApplicant> myApplicants = await getApplicants.GetApplicants(jobId);

                // Create a Grid
                Grid applicantsTable = new Grid();
                applicantsTable.HorizontalAlignment = HorizontalAlignment.Left;
                applicantsTable.VerticalAlignment = VerticalAlignment.Top;


/*                applicantsTable.Children.Clear();
                grid1.RowDefinitions.Clear();
                grid1.ColumnDefinitions.Clear();*/

                // Define the Columns
                ColumnDefinition applicantId = new ColumnDefinition();
                ColumnDefinition applicantName = new ColumnDefinition();
                ColumnDefinition expectedSalary = new ColumnDefinition();
                ColumnDefinition decision = new ColumnDefinition();
                applicantsTable.ColumnDefinitions.Add(applicantId);
                applicantsTable.ColumnDefinitions.Add(applicantName);
                applicantsTable.ColumnDefinitions.Add(expectedSalary);
                applicantsTable.ColumnDefinitions.Add(decision);


                // Add content to the Grid
                applicantsTable.Children.Add(CreateLabel("Applicant ID", 0, 0));
                applicantsTable.Children.Add(CreateLabel("Applicant Name", 1, 0));
                applicantsTable.Children.Add(CreateLabel("Expected Salary", 2, 0));
                applicantsTable.Children.Add(CreateLabel("Decision", 3, 0));







/*                for (var i = 1; i < 7; i++)
                {
                    RowDefinition rowApplicant = new RowDefinition();
                    applicantsTable.RowDefinitions.Add(rowApplicant);

                  

      
                        applicantsTable.Children.Add(CreateLabel("Sample0", 0, i+1));
                        applicantsTable.Children.Add(CreateLabel("Sample1", 1, i + 1));
                        applicantsTable.Children.Add(CreateLabel("Sample2", 2, i+1));
                        applicantsTable.Children.Add(CreateLabel("Sample3", 3, i + 1));

                   


                }*/



                int i = 1;
                foreach (var applicant in myApplicants)
                {
                   

                    //create row
                    RowDefinition rowApplicant = new RowDefinition();
                    applicantsTable.RowDefinitions.Add(rowApplicant);
                

                    //create button to approve/ decide
                    Button btnApproveApplicant = new Button();
                    btnApproveApplicant.Content = "Approve";
                    btnApproveApplicant.Width = 100;
                    btnApproveApplicant.HorizontalAlignment = HorizontalAlignment.Right;
                    btnApproveApplicant.Name = "btnApprove" + applicant.ApplicantId.ToString();
                    btnApproveApplicant.Margin = new Thickness(10, 10, 10, 10);
                 /*   btnApproveApplicant.Click += (sender, e) => See_Applicants_Click(btnSeeApplicants.Name);
*/



                    //add date to cells
                    applicantsTable.Children.Add(CreateLabel(applicant.ApplicantId.ToString(), 0, i));
                    applicantsTable.Children.Add(CreateLabel(applicant.ApplicantName, 1, i));
                    applicantsTable.Children.Add(CreateLabel(applicant.ExpectedSalary.ToString(), 2, i));

                    btnApproveApplicant.SetValue(Grid.RowProperty, i);
                    btnApproveApplicant.SetValue(Grid.ColumnProperty, 3);

                    applicantsTable.Children.Add(btnApproveApplicant);



                    i += 1;
                }



                // Add a black border to the grid
                Border border = new Border();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1);
                border.Child = applicantsTable;



                // Find the StackPanel named "Applicants" in the mainGrid
                StackPanel applicantsStackPanel = mainGrid.FindName("Applicants") as StackPanel;

                // If "Applicants" StackPanel doesn't exist, create one
                if (applicantsStackPanel == null)
                {
                    applicantsStackPanel = new StackPanel();
                    applicantsStackPanel.Name = "Applicants";
                    mainGrid.Children.Add(applicantsStackPanel);
                }

                // Add the bordered dynamicGrid to the "Applicants" StackPanel
                applicantsStackPanel.Children.Add(border);




            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
                




        }





        private Label CreateLabel(string text, int column, int row)
        {
            Label label = new Label();
            label.Content = text;
            Grid.SetColumn(label, column);
            Grid.SetRow(label, row);
            return label;
        }





    }
}
