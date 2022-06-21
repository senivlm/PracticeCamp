using System;
using System.Collections.Generic;
using System.IO;

namespace EmailChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> correct = EmailChecker.GetCorrectEmails(@"..\..\..\emails.txt");
                List<string> partiallyCorrect = EmailChecker.GetPartiallyCorrectEmails(@"..\..\..\emails.txt");
                List<string> correctUsingRegex = EmailChecker.GetCorrectEmailsUsingRegex(@"..\..\..\emails.txt");

                using (StreamWriter streamWriter = new StreamWriter(@"..\..\..\correct.txt", false))
                {
                    foreach (string item in correct)
                    {
                        streamWriter.WriteLine(item);
                    }
                }
                using (StreamWriter streamWriter = new StreamWriter(@"..\..\..\partiallyCorrect.txt", false))
                {
                    foreach (string item in partiallyCorrect)
                    {
                        streamWriter.WriteLine(item);
                    }
                }
                using (StreamWriter streamWriter = new StreamWriter(@"..\..\..\correctUsingRegex.txt", false))
                {
                    foreach (string item in correctUsingRegex)
                    {
                        streamWriter.WriteLine(item);
                    }
                }
            }
            catch(FileNotFoundException exp)
            {
                Console.WriteLine($"{exp.FileName} is not found");
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
