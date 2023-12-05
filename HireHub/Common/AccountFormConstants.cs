using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireHub.Common
{
    class AccountFormConstants
    {
        public static string InValidForm = "Invalid form data";
        public static string AccountAdded = "Account added";
        public static string Failure = "Failure";
        public static string WelcomeMessage = "Welcome ";
        public static string FailedToAddAccount = "Something went wrong. Failed to add account. Please try again later!";
        public static string EmailIdExist = "Email Id exist. \nAlready a user? Please login";
        public static string InValidFirstName = "Invalid first name";
        public static string InValidLastName = "Invalid last name";
        public static string InValidPasswordName = "Invalid password. Password must have more than 8 character with combination of special symbols, numbers, captial and small alphabets";
        public static string InValidCPasswordName = "Confirm password didnt matched password";
        public static string InValidPhoneName = "Invalid phone number";
        public static string InValidEmailName = "Invalid email address";
        public static string InValidUserType = "Please select user type in form. Sign-up as employer or job seeker?";
        public static string Employer = "Employer";
        public static string JobSeeker = "Job Seeker";
    }
}
