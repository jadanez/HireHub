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

        public static List<JobDetail> searchResult;
        private async void WindowOnLoad()
        {
            Debug.WriteLine("Entered onLoad debug");
            Trace.WriteLine("Entered onLoad trace");
            JobQueries jobQuery = new JobQueries();
            await jobQuery.SearchJob("Generic");
            Debug.WriteLine("Back to line 46: " + searchResult.Count);

            if (searchResult.Count == 0)
            {
                MessageBox.Show("No results matching the search! Please try another keyword", "No results matching the search! Please try another keyword", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                for (int i=0;i<4;i++)
                {
                    Debug.WriteLine("roleName" +i+ searchResult[i].roleName);
                }
                this.jobSeekerHomePageModel.jobDetails = searchResult;
                this.DataContext = null;
                this.DataContext = this.jobSeekerHomePageModel;
            }
            //List<JobDetail> fetchedResult = jobQuery.SearchJob("Generic");
            //if (fetchedResult != null)
            //{
            //    Debug.WriteLine("set value debug");
            //    Trace.WriteLine("set value trace");
            //    this.jobSeekerHomePageModel.jobDetails = fetchedResult;
            //}
        }

        
        private  void SeeDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            FormErrorMessages formErrorMessages = GetAllFieldValues();
            if (formErrorMessages != null)
            {
                if (formErrorMessages.isFormValid)
                {
                    JobQueries jobQuery = new JobQueries();
                    JobSeekerHomePageModel jobSeekerHomePageModel = new JobSeekerHomePageModel()
                    {
                        searchString = SearchBox.Text
                    };

                    jobQuery.SearchJob(jobSeekerHomePageModel.searchString);
                    //searchResult = jobQuery.SearchJob(jobSeekerHomePageModel.searchString);

                    Debug.WriteLine("Back to line 71: " + searchResult.Count);
                    if (searchResult == null)
                    {
                        MessageBox.Show("No results matching the search! Please try another keyword", "No results matching the search! Please try another keyword", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        // Navigate to Specific job page
                        this.jobSeekerHomePageModel.jobDetails = searchResult;
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
            string searchString = SearchBox.Text;
            if (String.IsNullOrEmpty(searchString) || !FieldValidators.AreAlphabets(searchString))
            {
                formErrorMessages.isFormValid = false;
                formErrorMessages.errorMessage = "Please try using a valid keyword for search";
                return formErrorMessages;
            }

            return formErrorMessages;
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
           Debug.WriteLine("Entered Search Click");
            FormErrorMessages formErrorMessages = GetAllFieldValues();
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
                        await jobQuery.SearchJob(jobSeekerHomePageModel.searchString);

                        Debug.WriteLine("Back to line 122: " + searchResult.Count);
                        if (searchResult.Count == 0)
                        {
                            MessageBox.Show("No results matching the search! Please try another keyword", "No results matching the search! Please try another keyword", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            this.jobSeekerHomePageModel.jobDetails = new List<JobDetail>();
                            this.jobSeekerHomePageModel.jobDetails = searchResult;

                            this.DataContext = null;
                            this.DataContext = this.jobSeekerHomePageModel;
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.Message , MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                   
                }
                else
                {
                    MessageBox.Show(formErrorMessages.errorMessage, SignUpFormConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Navigate to Specific job page - yet to implement
    }
}

