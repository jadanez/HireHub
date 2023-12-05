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
        public string btnName;

        public EmployerSeeApplicants(string btnName, string empFirstName, string empLastName, string empEmail, long userId, string userType)
        {
            this.empFirstName = empFirstName;
            this.empLastName = empLastName;
            this.empEmail = empEmail;
            this.userId = userId;
            this.userType = userType;
            this.btnName = btnName;

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

            // Create a Grid
            Grid applicantsTable = new Grid();
            applicantsTable.HorizontalAlignment = HorizontalAlignment.Left;
            applicantsTable.VerticalAlignment = VerticalAlignment.Top;

            // Define the Columns
            ColumnDefinition applicantId = new ColumnDefinition();
            ColumnDefinition applicantName = new ColumnDefinition();
            ColumnDefinition expectedSalary = new ColumnDefinition();
            ColumnDefinition decision = new ColumnDefinition();
            applicantsTable.ColumnDefinitions.Add(applicantId);
            applicantsTable.ColumnDefinitions.Add(applicantName);
            applicantsTable.ColumnDefinitions.Add(expectedSalary);
            applicantsTable.ColumnDefinitions.Add(decision);



            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            applicantsTable.RowDefinitions.Add(rowDef1);
            applicantsTable.RowDefinitions.Add(rowDef2);
            applicantsTable.RowDefinitions.Add(rowDef3);

            applicantsTable.Children.Add(CreateLabel("Data 1", 0, 1));
            applicantsTable.Children.Add(CreateLabel("Data 2", 1, 1));
            applicantsTable.Children.Add(CreateLabel("Data 3", 2, 1));

            applicantsTable.Children.Add(CreateLabel("Data 4", 0, 2));
            applicantsTable.Children.Add(CreateLabel("Data 5", 1, 2));
            applicantsTable.Children.Add(CreateLabel("Data 6", 2, 2));

            // Add content to the Grid
            applicantsTable.Children.Add(CreateLabel("Applicant ID", 0, 0));
            applicantsTable.Children.Add(CreateLabel("Applicant Name", 1, 0));
            applicantsTable.Children.Add(CreateLabel("Expected Salary", 2, 0));
            applicantsTable.Children.Add(CreateLabel("Decision", 3, 0));



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
