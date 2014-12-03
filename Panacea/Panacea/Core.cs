using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Panacea
{
    public class Core
    {
        public List<string> WordList { get; set; }

        public Core()
        {
            WordList = new List<string>();
            LoadFromFile();
            LoadShuffleBag();

        }

        private void LoadShuffleBag()
        {
            Dictionary<char, double> letterFrequencies = new Dictionary<char, double>
            {
                {'E', 12.702},
                {'T', 9.056},
                {'A', 8.167},
                {'O', 7.507},
                {'I', 6.966},
                {'N', 6.769},
                {'S', 6.327},
                {'H', 6.094},
                {'R', 5.987},
                {'D', 4.253},
                {'L', 4.025},
                {'C', 2.782},
                {'U', 2.758},
                {'M', 2.406},
                {'W', 2.306},
                {'F', 2.228},
                {'G', 2.015},
                {'Y', 1.974},
                {'P', 1.929},
                {'B', 1.492},
                {'V', 0.978},
                {'K', 0.772},
                {'J', 0.153},
                {'X', 0.150},
                {'Q', 0.095},
                {'Z', 0.074}
            };

            ShuffleBag = new ShuffleBag(88000);

            int amount = 0;
            foreach (var letter in letterFrequencies)
            {
                amount = (int)letter.Value * 1000;
                ShuffleBag.Add(letter.Key, amount);
            }

        }

        private void LoadFromFile()
        {
            string path = "D:/wordList.txt";

            foreach (var word in File.ReadAllLines(path))
            {
                WordList.Add(word.Trim().ToUpper());
            }
        }

        public bool CheckWord(string word)
        {
            return WordList.Contains(word.Trim().ToUpper());
        }

        public ShuffleBag ShuffleBag { get; set; }


    }
}
