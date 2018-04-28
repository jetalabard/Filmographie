using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metier
{
    public class Acteur : Personne
    {
        /// <summary>
        /// création d'un acteur
        /// </summary>
        /// <param name="nom">nom de l'acteur</param>
        /// <param name="prenom">prenom de l'acteur</param>
        /// <param name="image">image representant l'acteur</param>
        public Acteur(string nom, string prenom, string image,IEnumerable<Film> films)
            : base(nom, prenom, image, films)
        {
        }

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public Acteur()
        {
        }

        /// <summary>
        /// Constructeur avec nom et prenom
        /// </summary>
        /// <param name="nom">Le nom de la personne</param>
        /// <param name="prenom">Le prénom</param>
        public Acteur(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }
    }
}
