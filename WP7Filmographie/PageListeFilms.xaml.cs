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
    public partial class PageListeFilms : PhoneApplicationPage
    {
        public PageListeFilms()
        {
            InitializeComponent();
        }

        private void clicToShowDetail(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Film selectedItemData = (sender as ListBox).SelectedItem as Film;
            ((App)Application.Current).managerVM.filmSelectionné = selectedItemData;

            if (selectedItemData != null)
            {
                NavigationService.Navigate(new Uri("/DetailFilm.xaml", UriKind.Relative));
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = ((App)Application.Current).managerVM.ListefilmsSelectionné;
        }
    }
}