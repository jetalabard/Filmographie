using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metier
{
    /// <summary>
    /// classe qui renseigne un genre de film
    /// </summary>
    public class Genre :IEquatable<Genre>
    {
        /// <summary>
        /// nom du genre
        /// </summary>
        public string NomGenre
        {
            get;
            set;
        }
        /// <summary>
        /// description du genre
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// constructeur d'un genre
        /// </summary>
        /// <param name="nom">le nom du genre</param>
        /// <param name="description">la description du genre</param>
        public Genre(string nom, string description)
        {
            NomGenre = nom;
            Description = description;
        }
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public Genre()
        {
        }

        /// <summary>
        /// Constructeur d'un genre
        /// </summary>
        /// <param name="nom">Le nom du genre</param>
        public Genre(string nom)
        {
            NomGenre = nom;
        }

        /// <summary>
        /// affiche un genre
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return NomGenre;
        }

       
        /// <summary>
        /// redéfinition de la méthode getHashCode q
        /// </summary>
        /// <returns>le hash du NomGenre</returns>
        public override int GetHashCode()
        {
            return NomGenre.GetHashCode();
        }

       /// <summary>
       /// redefinition du protocole d'égalité
       /// </summary>
       /// <param name="right">l'objet à comparer</param>
       /// <returns>egale ou pas</returns>
        public override bool Equals(object right)
        {
            //check null
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

            return this.Equals(right as Genre);
        }

        /// <summary>
        /// redefinition du protocole d'égalité
        /// </summary>
        /// <param name="right">l'objet à comparer</param>
        /// <returns>egale ou pas</returns>
        public bool Equals(Genre other)
        {
            return (this.NomGenre.ToUpper() == other.NomGenre.ToUpper());
        }
    }
}
