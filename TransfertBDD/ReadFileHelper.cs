using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TransfertBDD
{
    class ReadFileHelper
    {

        /// <summary>
        /// Selectionne tous les fichiers .txt contenus dans un repertoire donné.
        /// </summary>
        /// <returns> Une liste de string qui sont les noms des fichiers</returns>
        public List<String> FindFiles()
        {
            DirectoryInfo MyRepository = new DirectoryInfo(Properties.Settings.Default.FilesRepository);
            FileInfo[] MyFile = MyRepository.GetFiles("*.txt");
            List<String> List = new List<string>();
            foreach (var file in MyFile)
            {
                List.Add(file.Name);
            }
            return List;
        }

        /// <summary>
        /// Read each line of the file into a string array. Each element of the array is one line of the file.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns> tableau de String correspondant aux lignes du fichier.txt</returns>
        public string[] Read(String FileName)
        {
            string[] content = System.IO.File.ReadAllLines(Properties.Settings.Default.FilesRepository + "\\" + FileName); //  \\ -> \   car \ est une séquence d'échappement
            return content;
        }

        /// <summary>
        /// Extrais le nom du client
        /// </summary>
        /// <param name="content"></param>
        /// <returns> un string : le nom du client</returns>
        public string ExtractClient(string[] content)
        {
            string name=null;
            string[] tempo = content[1].Split('\t');
            int i = 1;

            while(i!= tempo.Length)
            {
                name += tempo[i] + " ";
                i++;
            }
            return name;
        }
    }
}
