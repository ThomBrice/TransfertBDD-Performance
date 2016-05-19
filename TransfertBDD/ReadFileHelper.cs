using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TransfertBDD
{
    class ReadFileHelper
    {
        #region variable
        public string[] content;
        #endregion

        /// <summary>
        /// Selectionne tous les fichiers .txt contenus dans un repertoire donné.
        /// </summary>
        /// <returns> Une liste de string qui sont les noms des fichiers</returns>
        public List<String> FindFiles()
        {
            DirectoryInfo MyRepository = new DirectoryInfo(Properties.Settings.Default.FilesRepository);
            FileInfo[] MyFile = MyRepository.GetFiles("*.txt");
            List<FileInfo> test = new List<FileInfo>();
            List<String> List = new List<string>();
            foreach (var file in MyFile)
            {
                test.Add(file);
            }
            test = test.OrderByDescending(f=>f.CreationTime).ToList();
            foreach (var file in test)
            {
                List.Add(file.ToString());
            }
            return List;
        }

        /// <summary>
        /// Read each line of the file into a string array. Each element of the array is one line of the file.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns> tableau de String correspondant aux lignes du fichier.txt</returns>
        public void Read(String FileName)
        {
            content = System.IO.File.ReadAllLines(Properties.Settings.Default.FilesRepository + "\\" + FileName); //  \\ -> \   car \ est une séquence d'échappement
        }

        /// <summary>
        /// Extrait le nom du client
        /// </summary>
        /// <returns> un string : le nom du client</returns>
        public string ExtractClient()
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

        /// <summary>
        /// Extraction des valeurs qui nous interessent
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public String[] ExtractionValeurs(int index)
        {
            String[] details;
            String[] valeurs = new string[4];

            details = content[index].Split('\t');

                valeurs[0] = details[7]; // position
                valeurs[1] = details[8]; // Force.Vérin
                valeurs[2] = details[17]; // Vitesse cal
                valeurs[3] = details[18]; // Accélération
            return valeurs;
        }
    }
}
