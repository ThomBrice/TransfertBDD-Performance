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
        /// Read each line of the file into a string array. Each element of the array is one line of the file.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns> tableau de String correspondant aux lignes du fichier.txt</returns>
        public void Read(String FileName)
        {
            content = System.IO.File.ReadAllLines(FileName); //  \\ -> \   car \ est une séquence d'échappement
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

        public int DetectionStart(String[] document)
        {
            int i = 0;

            while (!(document[i][0].Equals('1') & document[i][1].Equals('\t')))
            {
                i++;
            }
            return i;
        }
    }
}
