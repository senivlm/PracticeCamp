using System;
using System.Text;
using System.Globalization;
using System.Linq;

namespace Task06
{
    class Account
    {
        public int Number { get; set; }
        public string Surname { get; set; }
        public int InputMeter { get; set; }
        public int OutputMeter { get; set; }
        public DateTime[] DatesMeter;

        public Account(int number, string surname, int inputMeter, int outputMeter, params DateTime[] datesMeter)
        {
            Number = number;
            Surname = surname;
            InputMeter = inputMeter;
            OutputMeter = outputMeter;
            DatesMeter = datesMeter;
        }
        private string ParseDateTime(DateTime dt)
        {
            return dt.ToString("MMM, dd yyyy", new CultureInfo("en-GB"));
        }

        public override bool Equals(object obj)
        {
            return obj is Account account &&
                   Number == account.Number &&
                   Surname == account.Surname;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Surname);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ParseDateTime(DatesMeter[0]));
            foreach (DateTime item in DatesMeter.Skip(1))
            {
                sb.Append("\n");
                sb.Append(ParseDateTime(item).PadLeft(25));
            }
            return $"Number: {Number}" +
                $"\nSurname: {Surname}" +
                $"\nInput meter: {InputMeter}" +
                $"\nOutput meter: {OutputMeter}" +
                $"\nDates meter: {sb}\n\n";
        }
    }
}
