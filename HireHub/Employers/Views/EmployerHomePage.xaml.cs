using HireHub.AllUsers.Models;
using HireHub.Database.Queries;
using HireHub.Employers.Models;
using HireHub.JobSeekers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public string userType;

        public EmployerHomePage(string emailAddress, string userType)
        {
            AccountQueries currentAccount = new AccountQueries();

            Account accountDetails = currentAccount.getAccountDetails(emailAddress, userType);
            this.empFirstName = accountDetails.FirstName;
            this.empLastName = accountDetails.LastName;
            this.empEmail = accountDetails.Email;
            this.userId = accountDetails.AccountId;
            this.userType = userType;
            InitializeComponent();
            



        }



        //set profile Name
        private async void Window_ContentRendered(object sender, EventArgs e)
        {

            EmpProfileName.Content = empFirstName;
            YourJobs.Foreground = Brushes.Blue;

            try
            {
                JobQueries getJobs = new JobQueries();
                List<Job> myJobs = await getJobs.GetMyJobs(userId);


                foreach (var job in myJobs)
                {
                    // Create a Rectangle
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 500;
                    rectangle.Height = 100;

                    // Create an ImageBrush with your image source
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("../../../Common/Images/TeamWall.jpg", UriKind.RelativeOrAbsolute));


                    // Set the Fill property of the Rectangle to the ImageBrush
                    rectangle.Fill = imageBrush;


                    //create a stackpanel for job
                    StackPanel stackPanelDetails = new StackPanel();
                    stackPanelDetails.Background = Brushes.LightBlue;
                    stackPanelDetails.Width = 500;



                    // Create TextBlock for the job name
                    TextBlock roleName = new TextBlock();
                    roleName.Text = job.roleName;
                    roleName.HorizontalAlignment = HorizontalAlignment.Stretch;
                    roleName.VerticalAlignment = VerticalAlignment.Top;
                    roleName.Padding = new Thickness(10, 10, 10, 10);

                    //create text block for job details
                    TextBlock jobDetails = new TextBlock();
                    jobDetails.Text = job.jobDetails;
                    jobDetails.HorizontalAlignment = HorizontalAlignment.Stretch;
                    jobDetails.VerticalAlignment = VerticalAlignment.Top;
                    jobDetails.TextWrapping = TextWrapping.Wrap;
                    jobDetails.Padding = new Thickness(10, 10, 10, 10);


                    //create button to view details
                    Button btnSeeApplicants = new Button();
                    btnSeeApplicants.Content = "See Applicants";
                    btnSeeApplicants.Width = 100;
                    btnSeeApplicants.HorizontalAlignment = HorizontalAlignment.Right;
                    btnSeeApplicants.Name = "btnJob" + job.jobId.ToString();
                    btnSeeApplicants.Margin = new Thickness(10, 10, 10, 10);
                    btnSeeApplicants.Click += (sender, e) => See_Applicants_Click(btnSeeApplicants.Name);



                    stackPanelDetails.Children.Add(roleName);
                    stackPanelDetails.Children.Add(jobDetails);
                    stackPanelDetails.Children.Add(btnSeeApplicants);




                    // Create a StackPanel to hold the Rectangle and the stackpanel
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Children.Add(rectangle);
                    stackPanel.Children.Add(stackPanelDetails);
                    stackPanel.Margin = new Thickness(0, 10, 0, 10);
                    
                    
                    
                    // Add the StackPanel to your JobsStackPanel
                    JobsStackPanel.Children.Add(stackPanel);
                }





            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

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

           /* MessageBox.Show("Your jobs!");*/
        }


        private void See_Applicants_Click(string btnName)
        {

           /* MessageBox.Show($"Button '{btnName}' clicked!");*/
            EmployerSeeApplicants seeApplicants = new EmployerSeeApplicants(btnName,  empFirstName,  empLastName,  empEmail,  userId,  userType);
            this.Visibility = Visibility.Hidden;
            seeApplicants.Show();

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
    }
}
