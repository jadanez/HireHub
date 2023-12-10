using HireHub.Database.Queries;
using HireHub.JobSeekers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HireHub.JobSeekers.Views
{
    /// <summary>
    /// Interaction logic for JobSeekerJobDetailPage.xaml
    /// </summary>
    public partial class JobSeekerJobDetailPage : Window
    {
        private readonly SpecificJobPageModel specificJobPageModel;
        public JobSeekerJobDetailPage( int jobId)
        {
            InitializeComponent();

            this.specificJobPageModel = new SpecificJobPageModel();
            WindowOnLoad(jobId);

            this.DataContext = this.specificJobPageModel;
          
        }

        //public static List<JobDetailModel> jobDetailSearchResult;
        private async void WindowOnLoad(int jobId)
        {
            JobQueries jobQuery = new JobQueries();
            try
            {
                List<JobDetailModel> searchResult = await jobQuery.SearchJob_ByJobId(jobId);

                Debug.WriteLine("Back to line 45: " + searchResult.Count);

                if (searchResult.Count == 0)
                {
                    MessageBox.Show("No results matching the search! Please try another keyword", "No results matching the search! Please try another keyword", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Debug.WriteLine("roleName" + searchResult[0].roleName);
                   
                    this.specificJobPageModel.specificJobDetails = searchResult;
                    this.DataContext = null;
                    this.DataContext = this.specificJobPageModel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }   

        }


        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            JobSeekerHomepage jobSeekerHomepage = new JobSeekerHomepage();
            this.Visibility = Visibility.Hidden;
            jobSeekerHomepage.SearchBox.Text = SearchBox.Text;
            jobSeekerHomepage.Show();
        }

        private void ShowSuccessToast(string message)
        {
            // Create a new TextBlock for the success message

            SuccessMessageTextBlock.Text = message;
            SuccessMessageTextBlock.Padding = new Thickness(10,10,10,10);
            SuccessMessageTextBlock.Background = Brushes.DarkGreen;
            SuccessMessageTextBlock.Foreground = Brushes.White;
            SuccessMessageTextBlock.FontWeight = FontWeights.Bold;
            SuccessMessageTextBlock.FontSize = 14;
            SuccessMessageTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            SuccessMessageTextBlock.VerticalAlignment = VerticalAlignment.Center;

            SuccessMessageBorder.Margin = new Thickness(30,10,0,10);
            SearchBox.Visibility = Visibility.Hidden;
            SuccessMessageTextBlock.Visibility = Visibility.Visible;
           

            // Close the toast after a delay

            var timer = new Timer(state =>
            {
                Application.Current.Dispatcher.Invoke(() => SuccessMessageTextBlock.Visibility = Visibility.Collapsed);

                // Navigate to the JobSeekerHomePage
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var jobSeekerHomePage = new JobSeekerHomepage();
                    this.Visibility = Visibility.Hidden;
                    jobSeekerHomePage.Show();

                });
            }, null, 2500, Timeout.Infinite);



        }

        private void ApplyNowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSuccessToast("Successfully applied for the job !");
        }

    }
}
