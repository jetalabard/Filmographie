using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Metier
{
    /// <summary>
    /// représente un film
    /// </summary>
    public class Film : IEquatable<Film>
    {
        /// <summary>
        /// titre du film
        /// </summary>
        public string Titre
        {
            get;
            set;
        }
        /// <summary>
        /// resume du film
        /// </summary>
        public string Resume
        {
            get;
            set;
        }
        /// <summary>
        /// note du film
        /// </summary>
        public int Note
        {
            get;
            set;
        }
        /// <summary>
        /// duree du film
        /// </summary>
        public TimeSpan Duree
        {
            get;
            set;
        }
        /// <summary>
        /// genres du film
        /// </summary>
        public List<Genre> Genres
        {
            get;
            set;
        }

        public string GenresFormatToString
        {
            get {
                string genres="";
                foreach (Genre genre in Genres)
                {
                    genres = genres + " " + genre.ToString();
                }
                return genres;
            }
        }

        /// <summary>
        /// chemin de l'image de l'affiche du film
        /// </summary>
        public string Image
        {
            get;
            set;
        }

        /// <summary>
        /// liste d'acteurs
        /// </summary>
        public IEnumerable<Acteur> Acteurs
        {
            get;
            set;
        }

        /// <summary>
        /// liste de réalisateur
        /// </summary>
        public List<Realisateur> Realisateurs
        {
            get;
            set;
        }
        
        /// <summary>
        /// constructeur du film
        /// </summary>
        /// <param name="titre">titre </param>
        /// <param name="resume">resume</param>
        /// <param name="note">note</param>
        /// <param name="duree">duree</param>
        /// <param name="genres">les genres du films</param>
        public Film(string titre, string resume, int note, TimeSpan duree, List<Genre> genres,List<Realisateur> realisateurs,List<Acteur> acteurs, string cheminImage)
        {
            Titre = titre;
            Resume = resume;
            Note = note;
            Duree = duree;
            Genres = genres;
            Acteurs = acteurs;
            Realisateurs = realisateurs;
            Image = cheminImage;

        }
        /// <summary>
        /// constructeur par defaut
        /// </summary>
        public Film()
        {
        }

        /// <summary>
        /// Constructeur d'un film
        /// </summary>
        /// <param name="titre">Le titre du film</param>
        public Film(string titre)
        {
            Titre = titre;
        }

        /// <summary>
        /// affiche un film
        /// </summary>
        /// <returns>le film affiché</returns>
        public override string ToString()
        { /*
            string genres = "";
            foreach (Genre genre in listeGenre)
            {
                genres +="\t"+genre.ToString() +"\n";
            }

            string acteurs = "";
            foreach (Acteur acteur in listeActeurs)
            {
                acteurs += "\t" + acteur.ToString() + "\n";
            }

            string realisateurs = "";
            foreach (Realisateur realisateur in listeRealisateur)
            {
                realisateurs += "\t" + realisateur.ToString() + "\n";
            }

            string s = string.Format("films : {0} \n Réalisateur(s) :\n {1}\n Acteurs :\n {2} \n Genres :\n {3} \n Resumé :\n\t {4} \n Note : {5} \n ", Titre, realisateurs, acteurs, genres, Resume, Note);
            return s;
           */

            return this.Titre;
        }


        /// <summary>
        /// returns a hash code in order to use this class in hash table
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return Titre.GetHashCode();
        }

        /// <summary>
        /// checks if the "right" object is equal to this Nounours or not
        /// </summary>
        /// <param name="right">the other object to be compared with this Nounours</param>
        /// <returns>true if equals, false if not</returns>
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

            return this.Equals(right as Film);
        }

        /// <summary>
        /// checks if this Nounours is equal to the other Nounours
        /// </summary>
        /// <param name="other">the other Nounours to be compared with</param>
        /// <returns>true if equals</returns>
        public bool Equals(Film other)
        {
            return (this.Titre.ToUpper() == other.Titre.ToUpper());
        }
    }
}
