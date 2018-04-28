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
    public partial class Connexion : PhoneApplicationPage
    {
        public Connexion()
        {
            InitializeComponent();
        }


        private void gotFocusID(object sender, RoutedEventArgs e)
        {
            if (textBoxIdentifiant.Text.Equals("Identifiant"))
            {
                textBoxIdentifiant.Text = "";
            }
        }
        private void lostFocusID(object sender, RoutedEventArgs e)
        {
            if (textBoxIdentifiant.Text.Length == 0)
            {
                textBoxIdentifiant.Text = "Identifiant";
            }


        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIdentifiant.Text)) 
            {
                MessageBox.Show("Identifiant invalide");
            }
            else
            {
                Personne personneCourante = ((App)Application.Current).managerVM.manager.recupereUnePersonne(textBoxIdentifiant.Text);
                if (personneCourante == null)
                {
                    MessageBox.Show("Identifiant non valide");
                }
                else
                {
                    ((App)Application.Current).managerVM.personneCourante = personneCourante;
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            } 
        }

        private void inscription_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Inscription.xaml", UriKind.Relative));
        }
    }
}