using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Metier
{
    /// <summary>
    /// représente une personne
    /// </summary>
    public class Personne : IEquatable<Personne>
    {
        /// <summary>
        /// nom d'une personne
        /// </summary>
        public string Nom
        {
            get;
            set;
        }
       /// <summary>
       /// prenom d'une personne
       /// </summary>
        public string Prenom
        {
            get;
            set;
        }

        /// <summary>
        /// liste de favoris de la personne
        /// </summary>
        public List<Film> listeFilms
        {
            get;
            set;
        }

        /// <summary>
        /// chemin de l'image de la personne
        /// </summary>
        public string Image
        {
            get;
            set;
        }

        /// <summary>
        /// constructeur de personnes
        /// </summary>
        /// <param name="nom">nom</param>
        /// <param name="prenom">prenom</param>
        /// <param name="pathImage">chemin image</param>
        public Personne(string nom, string prenom,string pathImage,IEnumerable<Film> films)
        {
            Nom = nom;
            Prenom = prenom;
            Image = pathImage;
            listeFilms = films.ToList();
        }
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public Personne()
        {
        }

        /// <summary>
        /// Méthode ToString d'une personne
        /// </summary>
        /// <returns>la chaine de caractères du nom et du prénom de la personne</returns>
        public override string ToString()
        {
            return Nom +" "+ Prenom;
        }


        /// <summary>
        /// redéfini la méthode getHash Code
        /// </summary>
        /// <returns>le hash du nom</returns>
        public override int GetHashCode()
        {
            return Nom.GetHashCode();
        }

        /// <summary>
        /// refinition du protocole d'égalité
        /// </summary>
        /// <param name="right">objet à comparer</param>
        /// <returns>egale ou pas</returns>
        public override bool Equals(object right)
        {
            if (object.ReferenceEquals(right, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, right))
            {
                return true;
            }

            if (this.GetType() != right.GetType())
            {
                return false;
            }

            return this.Equals(right as Personne);
        }

        /// <summary>
        /// refinition du protocole d'égalité
        /// </summary>
        /// <param name="right">objet à comparer</param>
        /// <returns>egale ou pas</returns>
        public bool Equals(Personne other)
        {
            if (this.Nom.ToUpper() == other.Nom.ToUpper())
            {
                if (this.Prenom.ToUpper() == other.Prenom.ToUpper())
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }


    }
}
