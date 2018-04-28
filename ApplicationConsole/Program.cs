using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Metier;
using ManagerXMLFile;
using Metier.Parser;

namespace Main
{
    class Program
    {
        /// <summary>
        /// méthode principale de l'application = démarrage
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           ///on récupere le répertoire parent
            //DirectoryInfo dirInfo = Directory.GetParent((Directory.GetCurrentDirectory()));
          //  string dirData = dirInfo.FullName + "\\";


            ///création du manager qui nous permet d'accèder aux données
            //Manager manager = new ManagerXMLF(dirData);


            ///regarde s'il y a un fichier de films sinon écrit un fichier par défaut
            try
            {
         //       XDocument mxdoc = XDocument.Load(dirData + XMLTags.FICHIER_FILMOTHEQUE);
            }
            catch 
            {
             //   manager.writeFilms();
            }
            ///regarde s'il y a un fichier de personnes sinon écrit un fichier par défaut
            try
            {
         //       XDocument mxdoc = XDocument.Load(dirData + XMLTags.FICHIER_PERSONNES);
            }
            catch 
            {
             //   manager.writePersonnes();
            }
            ///On lance l'application console
         //   ApplicationConsole interfaceConsole = new ApplicationConsole(manager);
        }
    }
}
