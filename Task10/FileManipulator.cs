using System;
using System.Collections.Generic;
using System.IO;

namespace Task10
{
    static class FileManipulator
    {
        static public Dictionary<string, string> ReadVocabulary(in string vocabularyFile)
        {
            Dictionary<string, string> vocabulary = new Dictionary<string, string>();
            if (!File.Exists(vocabularyFile))
            {
                throw new FileNotFoundException($"File {vocabularyFile} is not found");
            }
            string[] vocabularyRead = File.ReadAllLines(vocabularyFile);
            foreach (string item in vocabularyRead)
            {
                string[] line = item.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                vocabulary.Add(line[0], line[1]);
            }
            return vocabulary;
        }
        static public string ReadText(in string textFile)
        {
            if (!File.Exists(textFile))
            {
                throw new FileNotFoundException($"File {textFile} is not found");
            }
            return File.ReadAllText(textFile);
        }
        static public void WriteTranslation(in string translation, in string fileName)
        {
            using StreamWriter streamWriter = new StreamWriter(fileName);
            streamWriter.WriteLine(translation);
        }
    }
}
