using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Linq;

namespace Metier.Parser
{
    public class FilmReader
    {

        /// <summary>
        /// document
        /// </summary>
        XDocument mXDoc;

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="file">chemin du fichier</param>
        public FilmReader(string file)
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

        public FilmReader(XDocument xdom)
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
        /// lire un film dans le fichier
        /// </summary>
        /// <param name="filmElement"></param>
        /// <returns></returns>
        private Film ReadFilm(XElement filmElement)
        {

            Film film = new Film();
            if (filmElement.Attribute(XMLTags.TITRE) != null)
            {
                film.Titre = filmElement.Attribute(XMLTags.TITRE).Value;
            }
            if (filmElement.Attribute(XMLTags.DUREE) != null)
            {
                film.Duree = XmlConvert.ToTimeSpan(filmElement.Attribute(XMLTags.DUREE).Value);
            }
            if (filmElement.Attribute(XMLTags.NOTE) != null)
            {
                film.Note = XmlConvert.ToInt32(filmElement.Attribute(XMLTags.NOTE).Value);
            }

            if (filmElement.Element(XMLTags.RESUME) != null)
            {
                film.Resume = filmElement.Element(XMLTags.RESUME).Value;
            }

            if (filmElement.Element(XMLTags.IMAGE) != null)
            {
                film.Image = filmElement.Element(XMLTags.IMAGE).Value;
            }

            var realisateurs = filmElement.Descendants(XMLTags.REALISATEUR)
                                                       .Select(real => new Realisateur(real.Attribute(XMLTags.NOM).Value, real.Attribute(XMLTags.PRENOM).Value))
                                                       .ToList();
            film.Realisateurs = new List<Realisateur>(realisateurs);

            var acteurs = filmElement.Descendants(XMLTags.ACTEUR)
                                                       .Select(acteur => new Acteur(acteur.Attribute(XMLTags.NOM).Value, acteur.Attribute(XMLTags.PRENOM).Value))
                                                       .ToList();
            film.Acteurs = new List<Acteur>(acteurs);

            if (!filmElement.Element(XMLTags.GENRES).IsEmpty)
            {
                var genres = filmElement.Descendants(XMLTags.GENRES)
                                                       .Select(genre => new Genre(genre.Element(XMLTags.GENRE).Value))
                                                       .ToList();
                film.Genres = new List<Genre>(genres);
            }
            return film;
        }

        /// <summary>
        /// charge les genres à partir du fichier xml
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Film> load()
        {
            var list = mXDoc.Descendants(XMLTags.FILM).Select(elt => ReadFilm(elt));

            return list;
        }
    }
}
