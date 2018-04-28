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
using PersistanceXML;
using Metier;
using System.Xml.Linq;
using Metier.Parser;
using System.Linq;

namespace ManagerXMLWP7
{
    /// <summary>
    /// écrit les genres pour une application windows phone - écrit dans la chambre d'isolation
    /// </summary>
    public class GenreWriterWP7 : GenreWriter
    {
        /// <summary>
        /// met à jour le fichier des genres en ajoutant un genre
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="xml_file"></param>
        public override void saveData(Genre genre, string xmlFile)
        {
            if (mXDoc.Descendants(XMLTags.GENRE).Count() == 0)
            {
                mXDoc.Element(XMLTags.GENRES)
                     .Add(new XElement(XMLTags.GENRE,
                     new XAttribute(XMLTags.NOM, genre.NomGenre),
                     new XElement(XMLTags.DESCRIPTION, genre.Description)));
            }
            else
            {
                mXDoc.Descendants(XMLTags.GENRE)
                     .First()
                     .Add(new XElement(XMLTags.GENRE,
                         new XAttribute(XMLTags.NOM, genre.NomGenre),
                        new XElement(XMLTags.DESCRIPTION, genre.Description)));
            }
        }

    }
}
