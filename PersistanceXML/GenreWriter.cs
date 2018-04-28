using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Metier.Parser;
using Metier;
using System.Text;
using System.IO;

namespace PersistanceXML
{
    public abstract class GenreWriter
    {

        /// <summary>
        /// document à écrire
        /// </summary>
        public XDocument mXDoc
        {
            get;
            set;
        }


        public abstract void saveData(Genre genre , string xmlFile);


        /// <summary>
        /// écrit un genre dans le document
        /// </summary>
        /// <param name="genre">un genre</param>
        /// <returns>une répresentation xml de ce genre</returns>
        public XElement WriteGenre(Genre genre)
        {
            XElement genreElt =
               new XElement(XMLTags.GENRE,
                 new XAttribute(XMLTags.NOM, genre.NomGenre),
                 new XElement(XMLTags.DESCRIPTION, genre.Description));
            return genreElt;
        }

        /// <summary>
        /// sauvegarde le fichier
        /// </summary>
        /// <param name="xmlFile"></param>
        public void Save(string xmlFile)
        {
            mXDoc.Save(new StringWriter(new StringBuilder(xmlFile)));
        }


    }
}
