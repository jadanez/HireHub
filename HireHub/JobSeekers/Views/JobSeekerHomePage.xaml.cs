using HireHub.Common;
using HireHub.JobSeekers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using HireHub.Database.Queries;
using HireHub.Common.Models;

namespace HireHub.JobSeekers.Views
{
    /// <summary>
    /// Interaction logic for JobSeekerHomepage.xaml
    /// </summary>
    public partial class JobSeekerHomepage : Window
    {
        private readonly JobSeekerHomePageModel jobSeekerHomePageModel;
        private string userEmailID;
        public JobSeekerHomepage(string userEmailID)
        {
            InitializeComponent();

            this.jobSeekerHomePageModel = new JobSeekerHomePageModel();
            WindowOnLoad();

            this.DataContext = this.jobSeekerHomePageModel;
            this.userEmailID = userEmailID;
        }
        private async void WindowOnLoad()
        {
            // Debug.WriteLine("Entered onLoad debug");
            Trace.WriteLine("Entered onLoad trace");
            JobQueries jobQuery = new JobQueries();

            List<JobDetailModel> onLoadSearchResult = await jobQuery.SearchJob_All_Or_ByRoleName("Generic");
            // Debug.WriteLine("Back to line 46: " + onLoadSearchResult.Count);

            if (onLoadSearchResult.Count == 0)
            {
                MessageBox.Show("No results matching the search! Please try another keyword", "No results matching the search! Please try another keyword", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.jobSeekerHomePageModel.jobDetails = onLoadSearchResult;
                this.DataContext = null;
                this.DataContext = this.jobSeekerHomePageModel;
            }
        }


        private void SeeDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            int clickedButtonUId = Convert.ToInt32(((Button)sender).Uid);


            JobQueries jobQuery = new JobQueries();

            JobSeekerJobDetailPage jobSeekerJobDetailPage = new JobSeekerJobDetailPage(clickedButtonUId, userEmailID);
            this.Visibility = Visibility.Hidden;
            jobSeekerJobDetailPage.Show();

            // Debug.WriteLine("Id of clicked btn is" + clickedButtonUId);
        }
        private FormErrorMessages GetSearchString()
        {
            FormErrorMessages formErrorMessages = new FormErrorMessages();
            formErrorMessages.isFormValid = true;
            string searchString = SearchBox.Text;
            if (String.IsNullOrEmpty(searchString) || !FieldValidators.AreAlphabets(searchString))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = "Please try using a valid keyword for search";
                return formErrorMessages;
            }

            return formErrorMessages;
        }

        internal async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            // Debug.WriteLine("Entered Search Click");
            FormErrorMessages formErrorMessages = GetSearchString();
            if (formErrorMessages != null)
            {
                if (formErrorMessages.isFormValid)
                {
                    JobQueries jobQuery = new JobQueries();
                    JobSeekerHomePageModel jobSeekerHomePageModel = new JobSeekerHomePageModel()
                    {
                        searchString = SearchBox.Text
                    };
                    // Debug.WriteLine("Search String:" + jobSeekerHomePageModel.searchString);
                    try
                    {
                        //searchResult = jobQuery.SearchJob(jobSeekerHomePageModel.searchString);
                        List<JobDetailModel> onSearchSearchResult = await jobQuery.SearchJob_All_Or_ByRoleName(jobSeekerHomePageModel.searchString);

                        // Debug.WriteLine("Back to line 122: " + onSearchSearchResult.Count);
                        if (onSearchSearchResult.Count == 0)
                        {
                            MessageBox.Show("No results matching the search! Please try another keyword", "No results matching the search! Please try another keyword", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            this.jobSeekerHomePageModel.jobDetails = new List<JobDetailModel>(onSearchSearchResult);

                            this.DataContext = null;
                            this.DataContext = this.jobSeekerHomePageModel;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
                else
                {
                    MessageBox.Show(formErrorMessages.errorMessage, AccountFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void JobSeekerEditProfileClick(object sender, EventArgs e)
        {
            JobSeekerEditProfile employerHomePage = new JobSeekerEditProfile(userEmailID);
            this.Visibility = Visibility.Hidden;
            employerHomePage.Show();
        }
        public void LogoutBTN_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
    }
}

