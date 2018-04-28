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
    public partial class Inscription : PhoneApplicationPage
    {
        public Inscription()
        {
            InitializeComponent();
        }
        private void validation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIdentifiant.Text))
            {
                MessageBox.Show("identifiant vide");
            }
            else
            {
                Personne personneCourante = ((App)Application.Current).managerVM.manager.recupereUnePersonne(textBoxIdentifiant.Text);
                if (personneCourante == null)
                {
                    ((App)Application.Current).managerVM.personneCourante = personneCourante;
                    ((App)Application.Current).managerVM.manager.ajoutePersonne(personneCourante);
                    MessageBox.Show("Vous avez été enregistré.");
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Cet utilisateur existe déjà");
                }
            }
        }
        
    }
}