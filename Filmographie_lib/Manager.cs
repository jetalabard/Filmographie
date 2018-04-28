using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Metier
{
    public interface Manager
    {
        /// <summary>
        /// Récupère tous les genres dans le fichier genres.xml
        /// </summary>
        /// <returns>La liste de tous les genres</returns>        
        IEnumerable<Genre> Genres
        {
            get;
        }

        /// <summary>
        /// Récupère toutes les personnes du fichier personnes.xml
        /// </summary>
        /// <returns>La liste de toutes les personnes</returns>
        IEnumerable<Personne> Personnes
        {
            get;
        }

        /// <summary>
        /// Récupère la liste de tous les réalisateurs
        /// </summary>
        /// <returns>Une collection de Réalisateurs</returns>        
        IEnumerable<Realisateur> Realisateurs
        {
            get;
        }

        /// <summary>
        /// Récupère la liste de tous les acteurs
        /// </summary>
        /// <returns>Une collection d'Acteurs</returns>        
        IEnumerable<Acteur> Acteurs
        {
            get;
        }

        /// <summary>
        /// Récupère tous les films du fichier filmothèque.xml
        /// </summary>
        /// <returns>La liste des films</returns>        
        IEnumerable<Film> Films
        {
            get;
        }

        /// <summary>
        /// Retourne une collection de films d'un genre donné
        /// </summary>
        /// <param name="genre">Le genre des films recherchés</param>
        /// <returns>La collection de films</returns>        
        IEnumerable<Film> recupereFilmsParGenre(Genre genre);

        /// <summary>
        /// Retourne une collection de films d'un réalisateur donné
        /// </summary>
        /// <param name="realisateur">Le réalisateur des films recherchés</param>
        /// <returns>La collection de films</returns>        
        IEnumerable<Film> recupereFilmsParRealisateur(Realisateur realisateur);
        
        /// <summary>
        /// Retourne une collection des films d'un acteur donné
        /// </summary>
        /// <param name="acteur">L'acteur qui joue dans les films recherchés</param>
        /// <returns>La collection de films</returns>        
        IEnumerable<Film> recupereFilmsParActeurs(Acteur acteur);

        /// <summary>
        /// Retourne les "nombreTop" films ayant la meilleure note
        /// </summary>
        /// <param name="nombreTop">Le nombre de films a retourner</param>
        /// <returns>La collection de films</returns>        
        IEnumerable<Film> recupereTopFilms(int nombreTop);

        /// <summary>
        /// Ajoute un film au fichier filmothèque.xml
        /// </summary>
        /// <param name="film">Le film à ajouter</param>        
        void ajouteFilm(Film film);

        /// <summary>
        /// Ajoute un genre au fichier genres.xml
        /// </summary>
        /// <param name="genre">Le genre à ajouter</param>        
        void ajouteGenre(Genre genre);

        /// <summary>
        /// Ajoute une personne au fichier personnes.xml
        /// </summary>
        /// <param name="personne">La personne</param>       
        void ajoutePersonne(Personne personne);

        /// <summary>
        /// Réecris le fichier filmothèque.xml
        /// </summary>        
        void writeFilms();

        /// <summary>
        /// Réécris le fichier personnes.xml
        /// </summary>        
        void writePersonnes();

        /// <summary>
        /// Retourne une liste de genre correspondant au clé passé en parametres
        /// </summary>
        /// <param name="tousLesGenres">liste de clés</param>
        /// <returns>liste de genres</returns>        
        List<Genre> getListeGenreEnFonctionDesClefsPassees(params string[] tousLesGenres);

        /// <summary>
        /// Retourne la personne d'un nom donné
        /// </summary>
        /// <param name="nom">Le nom de la personne recherchée</param>
        /// <returns>La personne recherchée</returns>        
        Personne recupereUnePersonne(string nom);

        /// <summary>
        /// Met à jour la liste des films d'une personne        
        /// </summary>
        /// <param name="pers">La personne</param>
        /// <param name="film">Le film a ajouter</param>        
        Personne mettreAJourListeFilmsDeLaPersonne(Personne pers, Film film);
        
    }
}
