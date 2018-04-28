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

namespace WP7Filmographie
{
    public partial class Ajout : PhoneApplicationPage
    {
        public Ajout()
        {
            InitializeComponent();
        }
        /// <summary>
        /// redirige vers la page d'ajout de film
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void click_ajoutFilm(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AjoutFilm.xaml", UriKind.Relative));
        }
        /// <summary>
        /// redirige vers la page d'ajout de personne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void click_ajoutPersonne(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AjoutPersonne.xaml", UriKind.Relative));
        }
        /// <summary>
        /// redirige vers la page d'ajout de genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void click_ajoutGenre(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new Uri("/AjoutGenre.xaml", UriKind.Relative));
        }

        

            

        
    }
}