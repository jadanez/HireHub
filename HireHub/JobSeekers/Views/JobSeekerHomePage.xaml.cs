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
using System.Reflection.Metadata;

namespace HireHub.JobSeekers.Views
{
    /// <summary>
    /// Interaction logic for JobSeekerHomepage.xaml
    /// </summary>
    public partial class JobSeekerHomepage : Window
    {
        private readonly JobSeekerHomePageModel jobSeekerHomePageModel;
        public JobSeekerHomepage()
        {
            InitializeComponent();

            this.jobSeekerHomePageModel = new JobSeekerHomePageModel();
            WindowOnLoad();

            this.DataContext = this.jobSeekerHomePageModel;
        }
        private async void WindowOnLoad()
        {
            Debug.WriteLine("Entered onLoad debug");
            Trace.WriteLine("Entered onLoad trace");
            JobQueries jobQuery = new JobQueries();

            List<JobDetailModel> onLoadSearchResult = await jobQuery.SearchJob_All_Or_ByRoleName("Generic");
            Debug.WriteLine("Back to line 46: " + onLoadSearchResult.Count);

            if (onLoadSearchResult.Count == 0)
            {
                MessageBox.Show("No results matching the search! Please try another keyword", "No results matching the search! Please try another keyword", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Debug.WriteLine("roleName" + i + onLoadSearchResult[i].roleName);
                }
                this.jobSeekerHomePageModel.jobDetails = onLoadSearchResult;
                this.DataContext = null;
                this.DataContext = this.jobSeekerHomePageModel;
            }
        }


        private async void SeeDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            int clickedButtonUId = Convert.ToInt32(((Button)sender).Uid);
            Debug.WriteLine("-- See Details Tag: " + Convert.ToInt32(userProfileIcon.Tag));
            JobSeekerJobDetailPage jobSeekerJobDetailPage =  new JobSeekerJobDetailPage(clickedButtonUId, Convert.ToInt32(userProfileIcon.Tag));
            jobSeekerJobDetailPage.userProfileIcon.Tag = (userProfileIcon.Tag).ToString();
            this.Visibility = Visibility.Hidden;
            jobSeekerJobDetailPage.Show();

           Debug.WriteLine("Id of clicked btn is" + clickedButtonUId);
            
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
            Debug.WriteLine("Entered Search Click");
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
                    Debug.WriteLine("Search String:" + jobSeekerHomePageModel.searchString);
                    try
                    {
                        //searchResult = jobQuery.SearchJob(jobSeekerHomePageModel.searchString);
                        List<JobDetailModel> onSearchSearchResult = await jobQuery.SearchJob_All_Or_ByRoleName(jobSeekerHomePageModel.searchString);

                        Debug.WriteLine("Back to line 122: " + onSearchSearchResult.Count);
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
    }
}

