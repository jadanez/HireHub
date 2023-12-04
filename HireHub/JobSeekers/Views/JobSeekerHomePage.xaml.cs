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

        public List<JobDetail> searchResult;


        private void WindowOnLoad()
        {
            Debug.WriteLine("Entered onLoad debug");
            Trace.WriteLine("Entered onLoad trace");
            JobQueries jobQuery = new JobQueries();
            List<JobDetail> fetchedResult = jobQuery.SearchJob("Generic");
            if (fetchedResult != null)
            {
                Debug.WriteLine("set value debug");
                Trace.WriteLine("set value trace");
                this.jobSeekerHomePageModel.jobDetails = fetchedResult;
            }
        }
        private void SeeDetailsBtn_Click(object sender, RoutedEventArgs e)
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
                    searchResult = jobQuery.SearchJob(jobSeekerHomePageModel.searchString);
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

    }
}