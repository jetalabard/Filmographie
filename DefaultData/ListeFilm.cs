using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metier
{
    /// <summary>
    /// fournit une liste de films par défaut
    /// utiliser pour les tests mais aussi pour remplir le fichier filmothèque.xml s'il est introuvable
    /// </summary>
    public class ListeFilm
    {

        /// <summary>
        /// liste de films
        /// </summary>
        public List<Film> ListeFilms
        {
            get;
            private set;
        }
        /// <summary>
        /// constructeur
        /// </summary>
        public ListeFilm()
        {
            ListeFilms = new List<Film>();
        }
        /// <summary>
        /// initialise une liste de films par défaut
        /// </summary>
        public void initialiseListeFilms(Manager manager)
        {
            ListePersonne personnes = new ListePersonne();
            personnes.InitialiseListePersonne();

            var realisateurs = personnes.recupereRealisateur().Select(r => new { Nom = "Jean", Prenom = "Patrick", Image = " " });

            ListeFilms.Add(new Film("Les dents de la mer", "A quelques jours du début de la saison estivale...", 5, new TimeSpan(12,20,30)
                , manager.getListeGenreEnFonctionDesClefsPassees("Drame", "Action"),
                new List<Realisateur>(personnes.recupereRealisateur()), new List<Acteur>(personnes.recupereActeur()), ""));

            ListeFilms.Add(new Film("Les aristochats", "Une bande de petits chats qui ...", 8, new TimeSpan(12, 20, 30)
                , manager.getListeGenreEnFonctionDesClefsPassees("Animation"),
                new List<Realisateur>(personnes.recupereRealisateur()), new List<Acteur>(personnes.recupereActeur()), ""));

            ListeFilms.Add(new Film("Il faut sauver le soldat Ryan", "En fait c'était un instituteur...", 6, new TimeSpan(12, 20, 30)
                , manager.getListeGenreEnFonctionDesClefsPassees("Action"),
               new List<Realisateur>(personnes.recupereRealisateur()), new List<Acteur>(personnes.recupereActeur()), ""));

            ListeFilms.Add(new Film("Forest Gump", "Un homme qui court vite...", 8, new TimeSpan(12, 20, 30)
                 , manager.getListeGenreEnFonctionDesClefsPassees("Thriller"),
                 new List<Realisateur>(personnes.recupereRealisateur()), new List<Acteur>(personnes.recupereActeur()), ""));

            ListeFilms.Add(new Film("La ligne verte", "Dans une prison, un grand homme...", 4, new TimeSpan(12, 20, 30)
                , manager.getListeGenreEnFonctionDesClefsPassees("Fantastique", "Thriller"),
                new List<Realisateur>(personnes.recupereRealisateur()), new List<Acteur>(personnes.recupereActeur()), ""));
        }
       
    }
}
