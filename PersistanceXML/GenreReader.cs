using Metier.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;
using System.Xml;

namespace Metier
{
    public class GenreReader
    {
        /// <summary>
        /// document
        /// </summary>
        XDocument mXDoc;

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="file">chemin du fichier</param>
        public GenreReader(string file)
        {
            try
            {
                LoadXMLFile(file);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// constructeur par defaut
        /// </summary>
        public GenreReader(XDocument doc)
        {
            mXDoc = doc;
        }

        /// <summary>
        /// charge un fichier xml
        /// </summary>
        /// <param name="xml_file">chemin du fichier</param>
        private void LoadXMLFile(string xml_file)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
        }   
   
        /// <summary>
        /// lire un genre dans le fichier
        /// </summary>
        /// <param name="genreElement"></param>
        /// <returns></returns>
        public Genre ReadGenre(XElement genreElement)
        {
            Genre genre=new Genre();
            if (genreElement.Attribute(XMLTags.NOM) != null)
            {
                genre.NomGenre = genreElement.Attribute(XMLTags.NOM).Value;
              
            }
            if (genreElement.Element(XMLTags.DESCRIPTION) != null)
            {
                genre.Description = genreElement.Element(XMLTags.DESCRIPTION).Value;

            }

            return genre;
        }

        /// <summary>
        /// charge les genres à partir du fichier xml
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Genre> load()
        {
            var list = mXDoc.Descendants(XMLTags.GENRE).Select(elt => ReadGenre(elt));
            return list;
        }
    }
}
