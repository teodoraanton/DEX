using LinqToDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEX
{
    class Dex
    {
        public ObservableCollection<Words> DexItems { get; set; }
        
        public Dex()
        {
            DexItems = new ObservableCollection<Words>()
            {
                new Words()
            };
        }

        public List<string> ReadFile()
        {
            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";

            string[] lines = File.ReadAllLines(path);

            List<string> comboBox = new List<string>();

            using (StreamReader str = new StreamReader(path))
            {
                while(str.Peek() >=0)
                {
                    string[] dexWords;
                    string sir = str.ReadLine();
                    dexWords = sir.Split('#');

                    DexItems.Add(new Words()
                    {
                        Id = dexWords[0],
                        Word = dexWords[1],
                        Description = dexWords[2],
                        Category = dexWords[3],
                        Path = dexWords[4]
                    });

                    comboBox.Add(dexWords[3]);
                }
            }

            return comboBox;
        }
    }
}
