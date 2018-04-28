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
    public partial class ListeActeurs : PhoneApplicationPage
    {
        public ListeActeurs()
        {
            InitializeComponent();
        }

        private void ListeActeurs_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current).managerVM.ListeacteursSelectionné;
        }

        private void clicToShowDetailActeur(object sender, System.Windows.Input.GestureEventArgs e)
        {

            Acteur selectedItemData = (sender as ListBox).SelectedItem as Acteur;

            ((App)Application.Current).managerVM.acteurSelectionné = selectedItemData;
            NavigationService.Navigate(new Uri("/DetailActeur.xaml", UriKind.Relative));
        }
        
        
    }
}