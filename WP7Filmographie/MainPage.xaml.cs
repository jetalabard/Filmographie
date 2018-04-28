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
using ManagerXMLWP7;
using WP7Filmographie.VM;
using System.Windows.Resources;
using System.Windows.Media.Imaging;

namespace WP7Filmographie
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();
            if (((App)Application.Current).managerVM == null)
            {
                ((App)Application.Current).managerVM = new ManagerVM(new ManagerXMLWindowsPhone());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            chargeImageDansHubTitle();

            if (((App)Application.Current).managerVM.personneCourante != null)
            {
                Ajout.Visibility = Visibility.Visible;
                Favoris.Visibility = Visibility.Visible;
                Parameter.Visibility = Visibility.Visible;
                Connexion.Visibility = Visibility.Collapsed;
            }
            else
            {
                Ajout.Visibility = Visibility.Collapsed;
                Favoris.Visibility = Visibility.Collapsed;
                Parameter.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void chargeImageDansHubTitle()
        {
            Random r;
            BitmapImage image;
            chargeImageActeurs(out r, out image);

            image = chargeImageRealisateurs(r, image);

            image = chargeImageFilms(r, image);


        }

        private BitmapImage chargeImageFilms(Random r, BitmapImage image)
        {
            List<string> mesImagesFilms = ((App)Application.Current).managerVM.manager.Films.
                Where(film => !film.Image.Equals("")).
                Select(film => film.Image).ToList();

            int randomFilm = r.Next(0, mesImagesFilms.Count);

            image = new BitmapImage(new Uri(
                    string.Format("Data/Image/{0}", mesImagesFilms.ElementAt(randomFilm)),
                    UriKind.Relative));
            ListeFilms.Source = image;
            return image;
        }

        private BitmapImage chargeImageRealisateurs(Random r, BitmapImage image)
        {
            List<string> mesImagesRéalisateurs = ((App)Application.Current).managerVM.manager.Realisateurs.
                Where(real => !real.Image.Equals("")).
                Select(real => real.Image).ToList();

            int randomRealisateur = r.Next(0, mesImagesRéalisateurs.Count);

            image = new BitmapImage(new Uri(
                    string.Format("Data/Image/{0}", mesImagesRéalisateurs.ElementAt(randomRealisateur)),
                    UriKind.Relative));
            ListeRealisateur.Source = image;
            return image;
        }

        private void chargeImageActeurs(out Random r, out BitmapImage image)
        {
            List<string> mesImagesActeurs = ((App)Application.Current).managerVM.manager.Acteurs.
                            Where(acteur => !acteur.Image.Equals("")).
                            Select(acteur => acteur.Image).ToList();

            r = new Random();
            int randomActeur = r.Next(0, mesImagesActeurs.Count);

            image = new BitmapImage(new Uri(
                string.Format("Data/Image/{0}", mesImagesActeurs.ElementAt(randomActeur)),
                UriKind.Relative));
            ListeActeur.Source = image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonListeFilms(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((App)Application.Current).managerVM.ListefilmsSelectionné = ((App)Application.Current).managerVM.manager.Films;
            NavigationService.Navigate(new Uri("/PageListeFilms.xaml", UriKind.Relative));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonConnexion(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Connexion.xaml", UriKind.Relative));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonListeActeurs(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((App)Application.Current).managerVM.ListeacteursSelectionné = ((App)Application.Current).managerVM.manager.Acteurs;
            NavigationService.Navigate(new Uri("/ListeActeurs.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonListeRéalisateur(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((App)Application.Current).managerVM.ListeRéalisateursSelectionné = ((App)Application.Current).managerVM.manager.Realisateurs;
            NavigationService.Navigate(new Uri("/ListeRealisateurs.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonListeFilmsTop(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((App)Application.Current).managerVM.ListefilmsSelectionné = ((App)Application.Current).managerVM.manager.recupereTopFilms(((App)Application.Current).managerVM.NbTop);
            NavigationService.Navigate(new Uri("/PageListeFilms.xaml", UriKind.Relative));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonParametre(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Parametres.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonListeFavoris(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((App)Application.Current).managerVM.ListefilmsSelectionné = ((App)Application.Current).managerVM.personneCourante.listeFilms;
            NavigationService.Navigate(new Uri("/PageListeFilms.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonListeGenres(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ListeGenres.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicBoutonAjout(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Ajout.xaml", UriKind.Relative));
        }


    }
}