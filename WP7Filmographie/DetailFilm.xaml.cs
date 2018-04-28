using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Metier;

namespace WP7Filmographie
{
    public partial class DetailFilm : PhoneApplicationPage
    {
        public DetailFilm()
        {
            InitializeComponent();
        }

        private void voir_acteurs_films(object sender, RoutedEventArgs e)
        {

            ((App)Application.Current).managerVM.ListeacteursSelectionné = ((App)Application.Current).managerVM.filmSelectionné.Acteurs;

            NavigationService.Navigate(new Uri("/ListeActeurs.xaml", UriKind.Relative));
        }

        private void voir_realisateurs_films(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).managerVM.ListeRéalisateursSelectionné = ((App)Application.Current).managerVM.filmSelectionné.Realisateurs;
            NavigationService.Navigate(new Uri("/ListeRealisateurs.xaml", UriKind.Relative));
        }

        private void detail_loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current).managerVM.filmSelectionné;
            if (((App)Application.Current).managerVM.personneCourante != null)
            {
                int filmRetourne = ((App)Application.Current).managerVM.personneCourante.listeFilms.Where(f => f.Titre.Equals(((App)Application.Current).managerVM.filmSelectionné.Titre)).Count();
                if (filmRetourne == 0)
                {
                    boutonAjoutFavoris.Visibility = Visibility.Visible;
                }
                else
                {
                    boutonAjoutFavoris.Visibility = Visibility.Collapsed;
                }

            }
            else
            {
                boutonAjoutFavoris.Visibility = Visibility.Collapsed;
            }

        }

        private void ajoute_favoris(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).managerVM.personneCourante = ((App)Application.Current).managerVM.manager.mettreAJourListeFilmsDeLaPersonne(((App)Application.Current).managerVM.personneCourante, ((App)Application.Current).managerVM.filmSelectionné);
            MessageBox.Show("Favoris ajouté");
        }


    }
}