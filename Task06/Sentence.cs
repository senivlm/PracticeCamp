using System;
using System.IO;
using System.Text;

namespace Task06
{
    class Sentence
    {
        public string Path { get; set; }
        public Sentence(string path)
        {
            Path = path;
        }// злиття великого файлу в одну стрічку не завжди можливе.
        private string ReadText()
        {
            if (!File.Exists(Path))
            {
                throw new FileNotFoundException();
            }
            return File.ReadAllText(Path);
        }
        private string[] GetSentences()
        {
            return ReadText().Split('.', StringSplitOptions.RemoveEmptyEntries);
        }
        private string[] GetParsedSentences()
        {
            string[] sentences = GetSentences();
            for (int i = 0; i < sentences.Length; i++)
            {
                sentences[i] = sentences[i].Trim();
                while (sentences[i].Contains("  "))
                    sentences[i] = sentences[i].Replace("  ", " ");
                if (sentences[i].Contains('\r'))
                    sentences[i] = sentences[i].Replace("\r", "");
                if (sentences[i].Contains('\n'))
                    sentences[i] = sentences[i].Replace("\n", "");
            }
            return sentences;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string[] toPrint = GetParsedSentences();
            foreach (string item in toPrint)
            {
                sb.Append(item);
                sb.Append("\n");
            }
            return sb.ToString();
        }
        public void PrintText()
        {
            string[] toPrint = GetParsedSentences();
            StreamWriter writer = new StreamWriter("..\\..\\..\\Result.txt");
            foreach (string item in toPrint)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }
        public string GetLongest()
        {
            string[] separated = string.Join(" ", GetParsedSentences())
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int maxLength = 0;
            int maxIndex = 0;
            for (int i = 0; i < separated.Length; i++)
            {
                if (maxLength < separated[i].Length)
                {
                    maxLength = separated[i].Length;
                    maxIndex = i;
                }
            }
            return separated[maxIndex];
        }
        public string GetShortest()
        {
            string[] separated = string.Join(" ", GetParsedSentences())
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int minLength = GetLongest().Length;
            int minIndex = 0;
            for (int i = 0; i < separated.Length; i++)
            {
                if (minLength > separated[i].Length)
                {
                    minLength = separated[i].Length;
                    minIndex = i;
                }
            }
            return separated[minIndex];
        }
       
    }
}
