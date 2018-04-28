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
    public partial class ListeGenres : PhoneApplicationPage
    {
        public ListeGenres()
        {
            InitializeComponent();
        }

        private void ListeGenres_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current).managerVM.manager.Genres;
        }

        private void clickGenreToGetFilms(object sender, RoutedEventArgs e)
        {
            Genre selectedItemData = (sender as ListBox).SelectedItem as Genre;

            ((App)Application.Current).managerVM.genreSelectionné = selectedItemData;
            ((App)Application.Current).managerVM.ListefilmsSelectionné = ((App)Application.Current).managerVM.manager.recupereFilmsParGenre(selectedItemData);
            NavigationService.Navigate(new Uri("/PageListeFilms.xaml", UriKind.Relative));
        }

        
    }
}