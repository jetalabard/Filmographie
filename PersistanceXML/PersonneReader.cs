using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Metier.Parser
{
    public class PersonneReader
    {

        /// <summary>
        /// document
        /// </summary>
        XDocument mXDoc;

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="file">chemin du fichier</param>
        public PersonneReader(string file)
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

        public PersonneReader(XDocument xdom)
        {
            mXDoc = xdom;
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
        /// lire une personne dans le fichier
        /// </summary>
        /// <param name="personneElement"></param>
        /// <returns></returns>
        private Personne ReadPersonne(XElement personneElement)
        {
            Personne pers = null;
            if (personneElement.Attribute(XMLTags.TYPE).Value.Equals(XMLTags.REALISATEUR))
            {
                pers = new Realisateur();
            }
            else if (personneElement.Attribute(XMLTags.TYPE).Value.Equals(XMLTags.ACTEUR))
            {
                pers = new Acteur();
            }
            else { 
                pers = new Personne(); 
            }

            if (personneElement.Attribute(XMLTags.NOM) != null)
            {
                pers.Nom = personneElement.Attribute(XMLTags.NOM).Value;
              
            }
            if (personneElement.Attribute(XMLTags.PRENOM) != null)
            {
                pers.Prenom = personneElement.Attribute(XMLTags.PRENOM).Value;

            }
            if (personneElement.Element(XMLTags.IMAGE) != null)
            {
                pers.Image = personneElement.Element(XMLTags.IMAGE).Value;

            }
            if (!personneElement.Element(XMLTags.FILMS).IsEmpty)
            {
                var favoris = personneElement.Descendants(XMLTags.FILMS)
                                                       .Select(film => new Film(film.Element(XMLTags.FILM).Value))
                                                       .ToList();
                pers.listeFilms = new List<Film>(favoris);
            }

            return pers;
        }

        /// <summary>
        /// charge les genres à partir du fichier xml
        /// </summary>
        /// <returns>liste de personnes</returns>
        public IEnumerable<Personne> load()
        {
            var list = mXDoc.Descendants(XMLTags.PERSONNE).Select(elt => ReadPersonne(elt));
            return list;
        }
    }
}
