using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
namespace Metier.Parser
{
    public abstract class FilmWriter
    {
       
        /// <summary>
        /// document à écrire
        /// </summary>
        public XDocument mXDoc
        {
            get;
            set;
        }

        public abstract void saveData(Film film, string xmlFile);


        /// <summary>
        /// écrit un film dans le document
        /// </summary>
        /// <param name="film">un film</param>
        /// <returns>une répresentation xml de ce fil</returns>
        public XElement WriteFilm(Film film)
        {
            XElement filmElt =
               new XElement(XMLTags.FILM, new XAttribute(XMLTags.TITRE, film.Titre),
                                                    new XAttribute(XMLTags.DUREE, film.Duree),
                                                    new XAttribute(XMLTags.NOTE, film.Note),
                   new XElement(XMLTags.RESUME, film.Resume),
                   new XElement(XMLTags.GENRES, WriteGenres(film)),
                   new XElement(XMLTags.REALISATEURS, WriteRealisateurs(film)),
                   new XElement(XMLTags.ACTEURS, WriteActeurs(film)),
                   new XElement(XMLTags.IMAGE, film.Image));

            return filmElt;
        }
        /// <summary>
        /// écrit les acteurs du film
        /// </summary>
        /// <param name="film">film</param>
        /// <returns>liste de réprésentation xml d'acteurs</returns>
        private IEnumerable<XElement> WriteActeurs(Film film)
        {
            return film.Acteurs.Select(acteur => new XElement(XMLTags.ACTEUR, new XAttribute(XMLTags.NOM, acteur.Nom)
                                                                                               , new XAttribute(XMLTags.PRENOM, acteur.Prenom)));
        }
        /// <summary>
        /// écrit les réalisateurs du film
        /// </summary>
        /// <param name="film">film</param>
        /// <returns>liste de réprésentation xml des réalisateurs</returns>
        private IEnumerable<XElement> WriteRealisateurs(Film film)
        {
            return film.Realisateurs.Select(realisateur => new XElement(XMLTags.REALISATEUR, new XAttribute(XMLTags.NOM, realisateur.Nom)
                                                                                                , new XAttribute(XMLTags.PRENOM, realisateur.Prenom)));
        }
        /// <summary>
        /// écrit les genres du film
        /// </summary>
        /// <param name="film">film</param>
        /// <returns>liste de réprésentation xml des genres</returns>
        private IEnumerable<XElement> WriteGenres(Film film)
        {
            return film.Genres.Select(genre => new XElement(XMLTags.GENRE, genre.NomGenre));
        }
        /// <summary>
        /// sauvegarde le fichier
        /// </summary>
        /// <param name="xmlFile"></param>
        public void Save(string xmlFile)
        {
            mXDoc.Save(new StringWriter(new StringBuilder(xmlFile)));
        }
        /// <summary>
        /// écrit tous les films de la liste passé en parametre
        /// </summary>
        /// <param name="films"></param>
        public void WriteFilms(List<Film> films)
        {
            mXDoc = new XDocument(
                new XElement(XMLTags.FILMOTHEQUE, films.Select(film => WriteFilm(film))));
        }
        /// <summary>
        /// met à jour le fichier en ajoutant un film
        /// </summary>
        /// <param name="film"></param>
        /// <param name="xml_file"></param>
        public void UpdateListeFilms(Film film, string xml_file)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
            mXDoc.Descendants(XMLTags.FILM)
                 .First()
                 .Add(WriteFilm(film));
        }


    }
}
