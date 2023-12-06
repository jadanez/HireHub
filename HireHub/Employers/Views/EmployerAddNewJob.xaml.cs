using HireHub.AllUsers.Models;
using HireHub.Common;
using HireHub.Database.Queries;
using HireHub.Employers.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
    /// Interaction logic for EmployerAddNewJob.xaml
    /// </summary>
    public partial class EmployerAddNewJob : Window
    {
        public string userFirstName;
        public string userEmailId;
        public long userId;

        public EmployerAddNewJob(long userId, string userEmailId, string userFirstName)
        {
            this.userFirstName = userFirstName;
            this.userEmailId = userEmailId;
            this.userId = userId;
            InitializeComponent();
            InitViewValues();
        }

        private void InitViewValues()
        {
            if (!String.IsNullOrEmpty(userFirstName))
            {
                if (userFirstName.Length > 5)
                {
                    UserFirstNameLbl.Content = userFirstName.Substring(0, 4) + "...";
                }
                else
                {
                    UserFirstNameLbl.Content = userFirstName;
                }
            }
            string[] jobsTypesArray = { "Part-time", "Full-time" };
            JobTypeCombo.ItemsSource = jobsTypesArray.ToList();


            string[] experienceArray = { "0-1 years", "1-2 years", "2-5 years", "5+ years" };
            ExperienceCombo.ItemsSource = experienceArray.ToList();

            var bitmap = new BitmapImage();
            using (var stream = new FileStream("../../../Common/Images/profileLogo.png", FileMode.Open))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                bitmap.Freeze();
            }
            UserImage.Source = bitmap;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FormErrorMessages formErrors = ValidateFormData();
            if (formErrors != null)
            {
                if (formErrors.isFormValid == false)
                {
                    MessageBox.Show(formErrors.errorMessage, EmployerAddAJobConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    JobModel jobModel = populateJobModel();
                    JobQueries jobQueries = new JobQueries();
                    bool isJobAdded = await jobQueries.AddAJob(jobModel);
                    if (isJobAdded)
                    {
                        MessageBox.Show(EmployerAddAJobConstants.JobAdded, EmployerAddAJobConstants.JobAdded, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(EmployerAddAJobConstants.JobAddedFailure, EmployerAddAJobConstants.InValidForm, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private JobModel populateJobModel()
        {
            JobModel jobModel = new JobModel()
            {
                RoleName = RoleNameTextBox.Text,
                CompanyName = CompanyNameTextBox.Text,
                JobType = JobTypeCombo.SelectedItem.ToString(),
                ExperienceLevel = ExperienceCombo.SelectedItem.ToString(),
                JobDetails = DescriptionTextBox.Text,
                Salary = double.Parse(SalaryTextBox.Text),
                HiringManager = ContactTextBox.Text,
                JobLocation = LocationTextBox.Text,
                EmployerId = userId,
                JobStatus = "Active"
            };
            return jobModel;
        }

        private FormErrorMessages ValidateFormData()
        {
            FormErrorMessages formErrorMessage = new FormErrorMessages();
            formErrorMessage.isFormValid = true;
            if (String.IsNullOrEmpty(SalaryTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidSalary;
                return formErrorMessage;
            }
            if (!FieldValidators.IsDecimalNumber(SalaryTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidSalary;
                return formErrorMessage;
            }
            if (ExperienceCombo.SelectedItem == null)
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidExperience;
                return formErrorMessage;
            }
            if (JobTypeCombo.SelectedItem == null)
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidJobCategory;
                return formErrorMessage;
            }
            if (String.IsNullOrEmpty(RoleNameTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidJobRole;
                return formErrorMessage;
            }
            if (String.IsNullOrEmpty(CompanyNameTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidCompanyName;
                return formErrorMessage;
            }
            if (String.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidJobDetails;
                return formErrorMessage;
            }
            if (String.IsNullOrEmpty(ContactTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidJobHRManager;
                return formErrorMessage;
            }
            if (String.IsNullOrEmpty(LocationTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidJobLocation;
                return formErrorMessage;
            }
            return formErrorMessage;
        }
    }
}
