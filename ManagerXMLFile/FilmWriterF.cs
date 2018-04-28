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
using System.Xml.Linq;

using System.Linq;

namespace ManagerXMLFile
{
    /// <summary>
    /// écrit les films pour une application bureau
    /// </summary>
    public class FilmWriterF :FilmWriter
    {

        /// <summary>
        /// met à jour le fichier des genres en ajoutant un genre
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="xml_file"></param>
        public override void saveData(Film film, string xml_file)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
            if (mXDoc.Descendants(XMLTags.FILM).Count() == 0)
            {
                mXDoc.Element(XMLTags.FILMS)
                 .Add(WriteFilm(film));
            }
            else
            {
                mXDoc.Descendants(XMLTags.FILM)
                  .First()
                  .Add(WriteFilm(film));
            }
        }
    }
}
