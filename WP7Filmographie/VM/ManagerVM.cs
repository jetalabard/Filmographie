using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Metier;
using System.Collections.Generic;

namespace WP7Filmographie.VM
{
    public class ManagerVM
    {
        /// <summary>
        /// personne connecté à l'application
        /// </summary>
        public Personne personneCourante
        {
            get;
            set;
        }


        /// <summary>
        /// nombre de top à afficher
        /// </summary>
        public int NbTop
        {
            get;
            set;
        }

        /// <summary>
        /// permet accès aux données
        /// </summary>
        public Manager manager
        {
            get;
            set;
        }
        /// <summary>
        /// film actuellement selectionné
        /// </summary>
        public Film filmSelectionné
        {
            get;
            set;
        }
        /// <summary>
        /// genre actuellement selectionné
        /// </summary>
        public Genre genreSelectionné
        {
            get;
            set;
        }
        /// <summary>
        /// liste films sélectionné
        /// </summary>
        public IEnumerable<Film> ListefilmsSelectionné
        {
            get;
            set;
        }

        /// <summary>
        /// liste acteurs selectionné
        /// </summary>
        public IEnumerable<Acteur> ListeacteursSelectionné
        {
            get;
            set;
        }

        /// <summary>
        /// liste réalisateurs selectionné
        /// </summary>
        public IEnumerable<Realisateur> ListeRéalisateursSelectionné
        {
            get;
            set;
        }

        /// <summary>
        /// acteur selectionné
        /// </summary>
        public Acteur acteurSelectionné
        {
            get;
            set;
        }
        /// <summary>
        /// realisateur selectionné
        /// </summary>
        public Realisateur realisateurSelectionné
        {
            get;
            set;
        }

        /// <summary>
        /// constructeur initialise le nombre de top
        /// </summary>
        /// <param name="m"></param>
        public ManagerVM(Manager m)
        {
            manager = m;
            NbTop = 10;
        }
    }
}
