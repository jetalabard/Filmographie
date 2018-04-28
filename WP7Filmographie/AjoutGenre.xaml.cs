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
    public partial class AjoutGenre : PhoneApplicationPage
    {
        public AjoutGenre()
        {
            InitializeComponent();
        }

        private void gotFocusNom(object sender, RoutedEventArgs e)
        {
            if (TextBoxNom.Text.Equals("Nom du genre"))
            {
                TextBoxNom.Text = "";
            }
        }
        private void lostFocusNom(object sender, RoutedEventArgs e)
        {
            if (TextBoxNom.Text.Length == 0)
            {
                TextBoxNom.Text = "Nom du genre";
            }
        }

        private void gotFocusDescription(object sender, RoutedEventArgs e)
        {
            if (TextBoxDescription.Text.Equals("Description"))
            {
                TextBoxDescription.Text = "";
            }
        }

        private void lostFocusDescription(object sender, RoutedEventArgs e)
        {
            if (TextBoxDescription.Text.Length == 0)
            {
                TextBoxDescription.Text = "Description";
            }
        }

        private void click_ValidationAjoutGenre(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxNom.Text))
            {
                if (!string.IsNullOrWhiteSpace(TextBoxDescription.Text))
                {
                    Genre g = new Genre(TextBoxNom.Text, TextBoxDescription.Text);
                    regardeSiGenreExisteEtAjoute(g);
                }
                else
                {
                    MessageBox.Show("Remplissez la description du genre.");
                }
            }
            else
            {
                MessageBox.Show("Remplissez le nom du genre.");
            }
        }

        private void regardeSiGenreExisteEtAjoute(Genre g)
        {
            bool existe = false;
            foreach (Genre genre in ((App)Application.Current).managerVM.manager.Genres)
            {
                if (g.Equals(genre))
                {
                    existe = true;
                }
            }
            if (existe == false)
            {
                ((App)Application.Current).managerVM.manager.ajouteGenre(g);
                NavigationService.Navigate(new Uri("/ListeGenres.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Ce genre existe déjà.");
            }
        }
    }
}