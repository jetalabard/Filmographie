using System.Collections.Generic;
using Metier;
using Metier.Parser;
using System.Xml.Linq;
using System.Linq;
using PersistanceXML;

namespace ManagerXMLFile
{
    /// <summary>
    /// manager XML qui charge les données pour une application bureau
    /// </summary>
    public class ManagerXMLF : ManagerXML
    {
        /// <summary>
        /// liste de tous les genres
        /// </summary>
        List<Genre> listeGenres = new List<Genre>();
        /// <summary>
        /// liste de toutes les personnes
        /// </summary>
        List<Personne> listePersonnes = new List<Personne>();
        /// <summary>
        /// liste de tous les films
        /// </summary>
        List<Film> listeFilms = new List<Film>();


        /// <summary>
        /// constructeur --- rempli toutes les listes de données
        /// </summary>
        /// <param name="dirData"></param>
        public ManagerXMLF(string dirData)
            :base(dirData)
        {
            recupereFilms();
            recuperePersonne();
            recupereGenres();
        }

        public override IEnumerable<Genre> Genres
        {
            get { return listeGenres; }
        }

        public override IEnumerable<Film> Films
        {
            get { return listeFilms; }
        }

        public override IEnumerable<Personne> Personnes
        {
            get { return listePersonnes; }
        }

        /// <summary>
        /// Récupère tous les genres dans le fichier genres.xml
        /// </summary>
        /// <returns>La liste de tous les genres</returns>
        private IEnumerable<Genre> recupereGenres()
        {
            listeGenres.Clear();

            GenreReader read = new GenreReader(path + XMLTags.FICHIER_GENRES);

            foreach (Genre genre in read.load())
            {
                listeGenres.Add(genre);
            }
            listeGenres = listeGenres
                .OrderBy(genre => genre.NomGenre)
                .ToList();
            return listeGenres;
        }

        /// <summary>
        /// Récupère toutes les personnes du fichier personnes.xml
        /// </summary>
        /// <returns>La liste de toutes les personnes</returns>
        private IEnumerable<Personne> recuperePersonne()
        {
            listePersonnes.Clear();

            PersonneReader read = new PersonneReader(path + XMLTags.FICHIER_PERSONNES);

            foreach (Personne personne in read.load())
            {
                listePersonnes.Add(personne);
            }
            listePersonnes = listePersonnes
                .OrderBy(pers => pers.Nom)
                .ToList();
            return listePersonnes;
        }

        /// <summary>
        /// Récupère tous les films du fichier filmothèque.xml
        /// </summary>
        /// <returns>La liste des films</returns>
        private IEnumerable<Film> recupereFilms()
        {
            listeFilms.Clear();

            FilmReader read = new FilmReader(path + XMLTags.FICHIER_FILMOTHEQUE);

            foreach (Film film in read.load())
            {
                listeFilms.Add(film);
            }
            listeFilms = listeFilms
                .OrderBy(film => film.Titre)
                .ToList();

            return listeFilms;
        }


        /// <summary>
        /// Ajoute un film au fichier filmothèque.xml
        /// </summary>
        /// <param name="film">Le film à ajouter</param>
        public override void ajouteFilm(Film film)
        {
            FilmWriter writer = new FilmWriterF();
            writer.UpdateListeFilms(film, path + XMLTags.FICHIER_FILMOTHEQUE);
            writer.Save(path + XMLTags.FICHIER_FILMOTHEQUE);
            recupereFilms();
        }

        /// <summary>
        /// Ajoute un genre au fichier genres.xml
        /// </summary>
        /// <param name="genre">Le genre à ajouter</param>
        public override void ajouteGenre(Genre genre)
        {
            GenreWriter writer = new GenreWriterF();
            writer.saveData(genre, path + XMLTags.FICHIER_GENRES);
            writer.Save(path + XMLTags.FICHIER_GENRES);
            recupereGenres();
        }

        /// <summary>
        /// Ajoute une personne au fichier personnes.xml
        /// </summary>
        /// <param name="personne">La personne</param>
        public override void ajoutePersonne(Personne personne)
        {
            PersonneWriter writer = new PersonneWriterF();
            writer.WriteUnePersonne(personne, path + XMLTags.FICHIER_PERSONNES);
            writer.Save(path + XMLTags.FICHIER_PERSONNES);
            recuperePersonne();
        }

        /// <summary>
        /// Réecris le fichier filmothèque.xml
        /// </summary>
        public override void writeFilms()
        {
            ListeFilm liste = new ListeFilm();
            liste.initialiseListeFilms(this);

            FilmWriter writer = new FilmWriterF();

            writer.WriteFilms(liste.ListeFilms);
            writer.Save(path + XMLTags.FICHIER_FILMOTHEQUE);
        }

        /// <summary>
        /// Réécris le fichier personnes.xml
        /// </summary>
        public override void writePersonnes()
        {
            ListePersonne liste = new ListePersonne();
            liste.InitialiseListePersonne();

            PersonneWriter writer = new PersonneWriterF();

            writer.WritePersonnes(liste.listePersonne);
            writer.Save(path + XMLTags.FICHIER_PERSONNES);
        }

        /// <summary>
        /// Met à jour la liste des films d'une personne        
        /// </summary>
        /// <param name="pers">La personne</param>
        /// <param name="film">Le film a ajouter</param>
        public override Personne mettreAJourListeFilmsDeLaPersonne(Personne pers, Film film)
        {
            pers.listeFilms.Add(film);
            PersonneWriter writer = new PersonneWriterF();
            writer.UpdateListeFilmsPersonne(pers, film, path + XMLTags.FICHIER_PERSONNES);
            writer.Save(path + XMLTags.FICHIER_PERSONNES);
            return pers;
        }
    }
}
