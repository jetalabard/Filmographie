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
    public partial class ListeRealisateurs : PhoneApplicationPage
    {
        public ListeRealisateurs()
        {
            InitializeComponent();
        }
        private void ListeRealisateurs_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current).managerVM.ListeRéalisateursSelectionné;
        }

        private void clicToShowDetailRealisateur(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Realisateur selectedItemData = (sender as ListBox).SelectedItem as Realisateur;
            ((App)Application.Current).managerVM.realisateurSelectionné = selectedItemData;
            NavigationService.Navigate(new Uri("/DetailRealisateur.xaml", UriKind.Relative));
        }
        
    }
}