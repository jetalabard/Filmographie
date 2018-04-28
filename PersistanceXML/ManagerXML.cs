using Metier.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Metier
{
    public abstract class ManagerXML : Manager
    {
        

        /// <summary>
        /// Chemin des fichiers écriture/lecture
        /// </summary>
        public string path
        {
            get;
            set;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="pathDuFichier">Chemin du fichier</param>
        public ManagerXML(string pathDuFichier)
        {
            path=pathDuFichier;
            //recupereGenres();
            //recupereFilms();
            //recuperePersonne();
        }
        public ManagerXML()
        {
           
        }
        /// <summary>
        /// propriété definie dans ManagerXMLF et ManagerXMLWP7
        /// </summary>
        public abstract IEnumerable<Personne> Personnes
        {
            get;
        }
        /// <summary>
        /// propriété definie dans ManagerXMLF et ManagerXMLWP7
        /// </summary>
        public abstract IEnumerable<Film> Films
        {
            get;
        }
        /// <summary>
        /// propriété definie dans ManagerXMLF et ManagerXMLWP7
        /// </summary>
        public abstract IEnumerable<Genre> Genres
        {
            get;
        }

      
       
        /// <summary>
        /// Retourne une collection de films d'un genre donné
        /// </summary>
        /// <param name="genre">Le genre des films recherchés</param>
        /// <returns>La collection de films</returns>
        public IEnumerable<Film> recupereFilmsParGenre(Genre genre)
        {
            //var listeFilmsDeCeGenre = listeFilms
            //    .Where(film => film.listeGenre
            //        .Where(g => g.NomGenre.Equals(genre.NomGenre))
            //        .Count() > 0);

            var listeFilmsDeCeGenre = Films
               .Where(film => film.Genres.Contains(genre));
            return listeFilmsDeCeGenre;
        }

        /// <summary>
        /// Retourne une collection de films d'un réalisateur donné
        /// </summary>
        /// <param name="realisateur">Le réalisateur des films recherchés</param>
        /// <returns>La collection de films</returns>
        public IEnumerable<Film> recupereFilmsParRealisateur(Realisateur realisateur)
        {
           
            //var listeFilmDeCeRealisateur = listeFilms
            //    .Where(film => film.listeRealisateur
            //        .Where(r => r.Nom.Equals(realisateur.Nom))
            //        .Count() > 0);

            var listeFilmDeCeRealisateur = Films
               .Where(film => film.Realisateurs.Contains(realisateur));

            return listeFilmDeCeRealisateur;
        }

        /// <summary>
        /// Retourne une collection des films d'un acteur donné
        /// </summary>
        /// <param name="acteur">L'acteur qui joue dans les films recherchés</param>
        /// <returns>La collection de films</returns>
        public IEnumerable<Film> recupereFilmsParActeurs(Acteur acteur)
        {
            //var listeFilmDeCetActeur = listeFilms
            //    .Where(film => film.listeActeurs
            //        .Where(r => r.Nom.Equals(acteur.Nom))
            //        .Count() > 0);
            var listeFilmDeCetActeur = Films
               .Where(film => film.Acteurs.Contains(acteur));

            return listeFilmDeCetActeur;
        }

        /// <summary>
        /// Retourne les "nombreTop" films ayant la meilleure note
        /// </summary>
        /// <param name="nombreTop">Le nombre de films a retourner</param>
        /// <returns>La collection de films</returns>
        public IEnumerable<Film> recupereTopFilms(int nombreTop)
        {
            if (nombreTop > Films.Count())
            {
                nombreTop = Films.Count();
            }
            return Films.OrderByDescending(f => f.Note).Take(nombreTop);
        }  

        /// <summary>
        /// Ajoute un film au fichier filmothèque.xml
        /// </summary>
        /// <param name="film">Le film à ajouter</param>
        public abstract  void ajouteFilm(Film film);
        

        /// <summary>
        /// Ajoute un genre au fichier genres.xml
        /// </summary>
        /// <param name="genre">Le genre à ajouter</param>
        public abstract void ajouteGenre(Genre genre);
       
        /// <summary>
        /// Ajoute une personne au fichier personnes.xml
        /// </summary>
        /// <param name="personne">La personne</param>
        public abstract void ajoutePersonne(Personne personne);
        
        /// <summary>
        /// Réecris le fichier filmothèque.xml
        /// </summary>
        public abstract void writeFilms();
        

        /// <summary>
        /// Réécris le fichier personnes.xml
        /// </summary>
        public abstract void writePersonnes();
        

        /// <summary>
        /// Retourne une liste de genre correspondant au clé passé en parametres
        /// </summary>
        /// <param name="tousLesGenres">liste de clés</param>
        /// <returns>liste de genres</returns>
        public List<Genre> getListeGenreEnFonctionDesClefsPassees(params string[] tousLesGenres)
        {
            List<Genre> listeGenresARetourner = new List<Genre>();
            foreach (string key in tousLesGenres)
            {
                foreach (Genre g in Genres)
                {
                    if (g.NomGenre.Equals(key))
                    {
                        listeGenresARetourner.Add(g);
                    }
                }
            }
            return listeGenresARetourner;
        }

        /// <summary>
        /// Retourne la personne d'un nom donné
        /// </summary>
        /// <param name="nom">Le nom de la personne recherchée</param>
        /// <returns>La personne recherchée</returns>
        public Personne recupereUnePersonne(string nom)
        {
            List<Personne> personnes =  Personnes
                .Where(pers => pers.Nom.ToUpper()
                    .Equals(nom.ToUpper()))
                    .ToList();

            if (personnes.Count() == 0)
            {
                return null;
            }
            else
            {
                return personnes.First();
            }
        }

        /// <summary>
        /// Met à jour la liste des films d'une personne        
        /// </summary>
        /// <param name="pers">La personne</param>
        /// <param name="film">Le film a ajouter</param>
        public abstract Personne mettreAJourListeFilmsDeLaPersonne(Personne pers,Film film);


        /// <summary>
        /// Récupère la liste de tous les réalisateurs
        /// </summary>
        /// <returns>Une collection de Réalisateurs</returns>
        public IEnumerable<Realisateur> Realisateurs
        {
            get
            {
                var temp = Personnes
                    .Where(pers => pers is Realisateur);
                return Personnes
                    .Where(pers => pers is Realisateur)
                    .Select(pers => pers as Realisateur);
            }
        }
        /// <summary>
        /// Récupère la liste de tous les acteurs
        /// </summary>
        /// <returns>Une collection d'Acteurs</returns>
        public IEnumerable<Acteur> Acteurs
        {
            get { 
                return Personnes
                .Where(pers => pers is Acteur)
                .Select(pers => pers as Acteur); 
            }
        }
    }
}
