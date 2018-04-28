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
using Metier;
using Metier.Parser;
using System.Linq;

namespace ManagerXMLWP7
{
    /// <summary>
    /// écrit les films pour une application windows phone - écrit dans la chambre d'isolation
    /// </summary>
    public class FilmWriterWP7 :FilmWriter
    {
        /// <summary>
        /// met à jour le fichier des genres en ajoutant un genre
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="xml_file"></param>
        public override void saveData(Film film, string xml_file)
        {
            if (mXDoc.Descendants(XMLTags.FILM).Count() == 0)
            {
                mXDoc.Element(XMLTags.FILMS)
                 .Add(WriteFilm(film));
            }
            else
            {
                mXDoc.Descendants(XMLTags.FILMS)
                  .First()
                  .Add(WriteFilm(film));
            }
            
        }
    }
}
