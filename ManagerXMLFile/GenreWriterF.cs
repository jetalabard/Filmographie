using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using PersistanceXML;

namespace Metier.Parser
{
    /// <summary>
    /// écrit les genres pour une application bureau
    /// </summary>
    public class GenreWriterF : GenreWriter
    {
        
        /// <summary>
        /// met à jour le fichier des genres en ajoutant un genre
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="xml_file"></param>
        public override void saveData(Genre genre , string xmlFile)
        {
            try
            {
                mXDoc = XDocument.Load(xmlFile);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
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
