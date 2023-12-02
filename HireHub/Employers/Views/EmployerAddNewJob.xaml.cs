using HireHub.Common;
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
    /// Interaction logic for EmployerAddNewJob.xaml
    /// </summary>
    public partial class EmployerAddNewJob : Window
    {
        public string userFirstName;
        public string userEmailId;
        public EmployerAddNewJob(string userEmailId, string userFirstName)
        {
            this.userFirstName = userFirstName;
            this.userEmailId = userEmailId;
            InitializeComponent();
            InitViewValues();
        }

        private void InitViewValues()
        {
            if (!String.IsNullOrEmpty(userFirstName))
            {
                UserFirstNameLbl.Content = userFirstName;
            }
            string[] jobsTypesArray = { "Business Administration", "Education", "Architecture and construction", "Arts", "Medical", "Science", "Engineering" };
            JobTypeCombo.ItemsSource = jobsTypesArray.ToList();


            string[] experienceArray = { "0-1 years", "1-2 years", "2-5 years", "5+ years" };
            ExperienceCombo.ItemsSource = experienceArray.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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

                }
            }
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
            if (!FieldValidators.IsNumeric(SalaryTextBox.Text))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidSalary;
                return formErrorMessage;
            }
            if (String.IsNullOrEmpty(ExperienceCombo.SelectedValue.ToString()))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidExperience;
                return formErrorMessage;
            }
            if (String.IsNullOrEmpty(JobTypeCombo.SelectedValue.ToString()))
            {
                formErrorMessage.isFormValid = false;
                formErrorMessage.errorMessage = EmployerAddAJobConstants.InValidJobCategory;
                return formErrorMessage;
            }
            return formErrorMessage;
        }
    }
}
