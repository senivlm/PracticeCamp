using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace EmailChecker
{
    //Sources for validation: 
    //https://help.returnpath.com/hc/en-us/articles/220560587-What-are-the-rules-for-email-address-syntax-
    //https://help.xmatters.com/ondemand/trial/valid_email_format.htm
    public static class EmailChecker
    {
        private static readonly string characters = ".!#$%&'*+-/=?^_`{|@";
        private static readonly string charactersInValidForDomain = "!#$%&'*+/=?^_`{";
        private static readonly string charactersInValid = "|\"(),:;<>[\\]";

        private static readonly List<string> correct = new List<string>();
        private static readonly List<string> partiallyCorrect = new List<string>();
        private static readonly List<string> correctregex = new List<string>();

        private static readonly Regex validateEmailRegex = 
            new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]" +
                      "+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)" +
                      "*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)" +
                      "+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");

        private static string[] ParseData(in string fileName)
        {
            if(File.ReadAllLines(fileName).Length == 0)
            {
                throw new IOException("No data in file given"); 
                //not new ArgumentNullException() to avoid using System;
            }
            return File.ReadAllLines(fileName);
        }
        private static bool CharacterIsLastOrFirst(in string email)
        {
            foreach (char item in characters)
            {
                if(email[0] == item || email[^1] == item)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool DomainHasDot(in string email)
        {
            return email.Substring(email.IndexOf('@') + 1).Contains('.');
        }
        private static bool CharachtersGoConsecutively(in string email)
        {
            for (int i = 0; i < email.Length - 1; i++)
            {
                foreach (char item in characters)
                {
                    if (email[i] == email[i + 1] && email[i] == item)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool HasCharactersBefore(in string email)
        {
            if (email.IndexOf('@') != 0)
            {
                foreach (char item in characters)
                {
                    if (email[email.IndexOf('@') - 1] == item)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool HasCharactersInValidForDomain(in string email)
        {
            foreach (char item in charactersInValidForDomain)
            {
                if (email.Substring(email.IndexOf('@') + 1).Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool HasInvalidCharacters(in string email)
        {
            foreach (char item in email)
            {
                foreach (char character in charactersInValid)
                {
                    if(item == character)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool IsPartiallyCorreact(in string email)
        {
            return !IsCorrect(email) &&
                   email.Contains('@') &&
                   email.Substring(0, email.IndexOf('@') + 1).Length <= 64 &&
                   email.Substring(email.IndexOf('@') + 1).Length <= 253 &&
                   !HasCharactersInValidForDomain(email);
        }
        public static bool IsCorrect(in string email)
        {
            return email.Contains('@') &&
                   email.Substring(0, email.IndexOf('@') + 1).Length <= 64 &&
                   email.Substring(email.IndexOf('@') + 1).Length <= 253 &&
                   !CharacterIsLastOrFirst(email) &&
                   !CharachtersGoConsecutively(email) &&
                   !HasCharactersInValidForDomain(email) &&
                   !HasCharactersBefore(email) &&
                   !HasInvalidCharacters(email) &&
                   DomainHasDot(email);
        }
        public static bool IsValidRegex(in string email)
        {
            return validateEmailRegex.IsMatch(email);
        }
        private static void GetDataUsingRegex(in string fileName)
        {
            string[] parsed = ParseData(fileName);
            foreach (string item in parsed)
            {
                if (IsValidRegex(item))
                {
                    correctregex.Add(item);
                }
            }
        }
        private static void GetCorrectData(in string fileName)
        {
            string[] parsed = ParseData(fileName);
            foreach (string item in parsed)
            {
                if (IsCorrect(item))
                {
                    correct.Add(item);
                }
            }
        }
        private static void GetPartialData(in string fileName)
        {
            string[] parsed = ParseData(fileName);
            foreach (string item in parsed)
            {
                if (IsPartiallyCorreact(item))
                {
                    partiallyCorrect.Add(item);
                }
            }
        }
        public static List<string> GetCorrectEmails(in string fileName)
        {
            GetCorrectData(fileName);
            return correct;
        }
        public static List<string> GetPartiallyCorrectEmails(in string fileName)
        {
            GetPartialData(fileName);
            return partiallyCorrect;
        }
        public static List<string> GetCorrectEmailsUsingRegex(in string fileName)
        {
            GetDataUsingRegex(fileName);
            return correctregex;
        }
    }
}
