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

namespace HireHub.JobSeekers.Views
{
    /// <summary>
    /// Interaction logic for JobSeekerEditProfile.xaml
    /// </summary>
    public partial class JobSeekerEditProfile : Window
    {
        long userId;
        string userEmailId;
        public JobSeekerEditProfile(long userId, string userEmailId)
        {
            this.userId = userId;
            this.userEmailId = userEmailId;
            InitializeComponent();
            InitializeViewValues();
        }

        private void InitializeViewValues()
        {

        }
        private async void SaveProfileDetailsBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void SaveAccountDetailsBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
