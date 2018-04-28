using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Metier
{
    public class Realisateur : Personne
    {
        /// <summary>
        /// création d'un réalisateur
        /// </summary>
        /// <param name="nom">nom du réalisateur</param>
        /// <param name="prenom">prenom du réalisateur</param>
        /// <param name="image">image representant le réalisateur</param>
        public Realisateur(string nom, string prenom, string image, IEnumerable<Film> films)
            : base(nom, prenom, image,films)
        {
        }
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public Realisateur()
        {

        }

        /// <summary>
        /// Constructeur d'une réalisateur avec son nom et prénom
        /// </summary>
        /// <param name="nom">Le nom </param>
        /// <param name="prenom">Le prénom</param>
        public Realisateur(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }

    }
}
