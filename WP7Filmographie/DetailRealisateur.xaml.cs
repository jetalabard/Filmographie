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
    public partial class DetailRealisateur : PhoneApplicationPage
    {
        public DetailRealisateur()
        {
            InitializeComponent();
        }
        private void click_films(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).managerVM.ListefilmsSelectionné = ((App)Application.Current).managerVM.manager.recupereFilmsParRealisateur(((App)Application.Current).managerVM.realisateurSelectionné);
            NavigationService.Navigate(new Uri("/PageListeFilms.xaml", UriKind.Relative));
        }
        private void detail_réalisateur_loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current).managerVM.realisateurSelectionné;
        }
        
    }
}