using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEX
{
    class RemoveAndModifyFile
    {
        public void RemoveItem(string idToRemove)
        {
            string fileName = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";

            string[] lines = File.ReadAllLines(fileName).ToArray();

            int indexFile = 0;

            while (indexFile < lines.Length)
            {
                string[] dexList = lines[indexFile].Split('#');

                if (dexList[0] == idToRemove)
                {
                    //int copyIndex = indexFile - 1;
                    for (int index = indexFile; index < lines.Length - 1; index++)
                    {
                        lines[index] = lines[index + 1];
                        string[] newString = lines[index].Split('#');
                        newString[0] = (index + 1).ToString();
                        //copyIndex++;
                        string forFile = newString[0] + "#" + newString[1] + "#" + newString[2] + "#" + newString[3] + "#" + newString[4];
                        lines[index] = forFile;
                    }
                    lines = lines.Take(lines.Count() - 1).ToArray();
                    break;
                }
                indexFile++;
            }

            File.WriteAllLines(fileName, lines);
        }

        public void ModifyItem(string idToModify, string modifyItems)
        {
            int index = int.Parse(idToModify);

            string fileName = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
            string[] lines = File.ReadAllLines(fileName).ToArray();

            lines[index - 1] = modifyItems;

            File.WriteAllLines(fileName, lines);
        }
    }
}
