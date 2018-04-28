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
    public partial class Parametres : PhoneApplicationPage
    {
        public Parametres()
        {
            InitializeComponent();
        }
        private void validation_nb_top_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                MessageBox.Show("nombre vide");
            }
            else
            {
                try
                {
                    int anInteger = Convert.ToInt32(textBoxNombre.Text);
                    ((App)Application.Current).managerVM.NbTop = anInteger;
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                catch (Exception exception)
                {
                    MessageBox.Show("nombre invalide : " + exception.Message);
                }

            }
        }
    }
}