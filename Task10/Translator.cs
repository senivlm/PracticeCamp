using System;
using System.Collections.Generic;
using System.Text;

namespace Task10
{
    //FileManipulator is created to use files independently
    //Uppercase, punctuation signs and multiply gaps are processed
    //Translation can be outputed to the console or written to the file
    class Translator
    {
        private readonly Dictionary<string, string> vocabulary;
        public string Text { get; set; }
        public static int changeCounterAllowed = 5;
        public Translator()
        { }
        public Translator(in Dictionary<string, string> vocabulary, in string text)
        {
            this.vocabulary = vocabulary;
            Text = text;
        }
        public Translator(in string vocabularyFile, in string textFile)
        {
            vocabulary = FileManipulator.ReadVocabulary(vocabularyFile);
            Text = FileManipulator.ReadText(textFile);
        }
        public string Translate()
        {
            string[] words = Text.Split(new char[] { ' ' });
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (string item in words)
            {
                if(item == "")
                {
                    sb.Append(" ");
                    continue;
                }
                string wordToTranslate = item;
                
                if (char.IsUpper(item[0]))
                {
                    wordToTranslate = item.ToLower();
                }
                if (char.IsPunctuation(item[^1]))
                {
                    wordToTranslate = wordToTranslate[0..^1];
                }
                if(!vocabulary.TryGetValue(wordToTranslate, out string translation))
                {
                    GetTranslation(wordToTranslate, out translation);
                    counter++;
                }
                if(counter > changeCounterAllowed)
                {
                    throw new ArgumentException("Cannot correct vocabulary");
                }
                if (char.IsUpper(item[0]))
                {
                    translation = char.ToUpper(translation[0]) + translation.Substring(1);
                }
                if (char.IsPunctuation(item[^1]))
                {
                    translation += item[^1];
                }
                sb.Append(translation + " ");
            }
            return sb.ToString();
        }
        private void AddTranslation(in string word)
        {
            Console.WriteLine($"Translation for {word} is not found. Input your translation: ");
            string translation = Console.ReadLine().Trim().ToLower() ?? throw new ArgumentException("Empty word given");
            vocabulary.Add(word, translation);
        }
        private bool GetTranslation(in string word, out string translation)
        {
            AddTranslation(word);
            return vocabulary.TryGetValue(word, out translation);
        }
        public void WriteTranslationToFile(in string fileName)
        {
            FileManipulator.WriteTranslation(Translate(), fileName);
        }
    }
}
