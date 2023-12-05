using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HireHub.Common
{
    public class FieldValidators
    {
        public static bool IsMailValid(string email)
        {
            string regex = "\\A(?:[a-z0-9!#$%&'*+\\/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+\\/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            string regex = "(^[0-9]{10})";
            return Regex.IsMatch(phoneNumber, regex, RegexOptions.IgnoreCase);
        }
        // Special Char and digit and alphabets
        public static bool IsPasswordValid(string password)
        {
            string regex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
            return Regex.IsMatch(password, regex, RegexOptions.IgnoreCase);
        }
       
        public static bool AreAlphabets(string input)
        {
            string regex = "^[A-Za-z]+$";
            return Regex.IsMatch(input, regex, RegexOptions.IgnoreCase);
        }
        
        public static bool ConfirmPasswordMatched(string CPassword,string Password)
        {
            return CPassword.Equals(Password);
        }
        public static bool IsNumeric(string numbers)
        {
            string regex = "^[0-9]+$";
            return Regex.IsMatch(numbers, regex, RegexOptions.IgnoreCase);
        }
        public static bool IsDecimalNumber(string numbers)
        {
            string regex = "^[0-9]*(?:\\.[0-9]+)?$";
            return Regex.IsMatch(numbers, regex, RegexOptions.IgnoreCase);
        }
    }
}
