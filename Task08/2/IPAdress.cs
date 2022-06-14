using System;

namespace Task08
{
    class IPAdress
    {
        public string Adress { get; set; }
        public TimeSpan Time { get; set; }
        public string DayOfWeek { get; set; }
        public IPAdress(string adress, TimeSpan time, string dayOfWeek)
        {
            Adress = adress ?? throw new ArgumentNullException("No objects in list given");
            if (Time == null)
            {
                throw new ArgumentNullException("No objects in list given");
            }
            Time = time;
            DayOfWeek = dayOfWeek ?? throw new ArgumentNullException("No objects in list given");
        }
        public override bool Equals(object obj)
        {
            return obj is IPAdress adress &&
                   Adress == adress.Adress &&
                   Time.Equals(adress.Time) &&
                   DayOfWeek == adress.DayOfWeek;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Adress, Time, DayOfWeek);
        }
        public override string ToString()
        {
            return $"{Adress} {Time} {DayOfWeek}\n";
        }
    }
}
