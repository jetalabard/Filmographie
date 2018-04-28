using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Metier
{
    /// <summary>
    /// fournit une liste de personnes par défaut
    /// utiliser pour les tests mais aussi pour remplir le fichier personnes.xml s'il est introuvable
    /// </summary>
    public class ListePersonne
    {
        /// <summary>
        /// liste de films
        /// </summary>
        public List<Personne> listePersonne
        {
            get;
            private set;
        }
        /// <summary>
        /// constructeur
        /// </summary>
        public ListePersonne()
        {
            listePersonne = new List<Personne>();
        }
        /// <summary>
        /// initialise une liste avec des réalisateurs
        /// </summary>
        public void InitialiseListePersonne()
        {
            listePersonne.Add(new Realisateur("Cameron", "James", "",null));
            listePersonne.Add(new Realisateur("Lucas", "George", "", null));
            listePersonne.Add(new Acteur("Aniston", "Jennifer", "", null));
            listePersonne.Add(new Personne("Septier", "Quentin", "", null));
            listePersonne.Add(new Acteur("Carell", "Steve", "", null));
            listePersonne.Add(new Personne("Talabard", "Jérémy", "", null));
        }
        /// <summary>
        /// récupere les réalisateurs parmi la liste de personnes
        /// </summary>
        /// <returns>liste de réalisateurs</returns>
        public IEnumerable<Realisateur> recupereRealisateur()
        {
            return listePersonne.Where(pers => pers is Realisateur).Select(pers => pers as Realisateur);
        }
        /// <summary>
        /// récupere les acteurs parmi la liste de personnes
        /// </summary>
        /// <returns>liste de acteurs</returns>
        public IEnumerable<Acteur> recupereActeur()
        {
            return listePersonne.Where(pers => pers is Acteur).Select(pers => pers as Acteur);
        }
    }
}
