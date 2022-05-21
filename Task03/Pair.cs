
namespace Task03
{
    class Pair
    {
        public Pair(in int number, in int freq)
        {
            Number = number;
            Freq = freq;
        }
        public int Number { get; set; }
        public int Freq { get; set; }
        public override string ToString()
        {
            return $"{Number} : {Freq}";
        }
    }
}
