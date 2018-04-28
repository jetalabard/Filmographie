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
using Microsoft.Phone.Tasks;
using Metier;

namespace WP7Filmographie
{
    public partial class AjoutPersonne : PhoneApplicationPage
    {

        CameraCaptureTask mCameraCaptureTask;

        public AjoutPersonne()
        {
            InitializeComponent();
            mCameraCaptureTask = new CameraCaptureTask();
            mCameraCaptureTask.Completed += new EventHandler<PhotoResult>(mCameraCaptureTask_Completed);
        }

        private void ajout_personne_loaded(object sender, RoutedEventArgs e)
        {
            checkBoxActeur.IsChecked = true;
        }

        private void Take_photo(object sender, RoutedEventArgs e)
        {
            try
            {
                mCameraCaptureTask.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void mCameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                mImage.Source = bmp;
            }
        }

        private void gotFocusNom(object sender, RoutedEventArgs e)
        {
            if (TextBoxNom.Text.Equals("Nom"))
            {
                TextBoxNom.Text = "";
            }
        }
        private void lostFocusNom(object sender, RoutedEventArgs e)
        {
            if (TextBoxNom.Text.Length == 0)
            {
                TextBoxNom.Text = "Nom";
            }
        }


        private void gotFocusPrénom(object sender, RoutedEventArgs e)
        {
            if (TextBoxPrenom.Text.Equals("Prénom"))
            {
                TextBoxPrenom.Text = "";
            }
        }
        private void lostFocusPrénom(object sender, RoutedEventArgs e)
        {
            if (TextBoxPrenom.Text.Length == 0)
            {
                TextBoxPrenom.Text = "Prénom";
            }


        }


        private void click_ValidationAjoutPersonne(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(TextBoxNom.Text))
            {

                if (!string.IsNullOrWhiteSpace(TextBoxPrenom.Text))
                {
                    Personne pers;
                    if (mImage.Source == null)
                    {
                        if (checkBoxActeur.IsChecked.Value)
                        {
                            pers = new Acteur(TextBoxNom.Text, TextBoxPrenom.Text, "", new List<Film>());
                        }
                        else
                        {
                            pers = new Realisateur(TextBoxNom.Text, TextBoxPrenom.Text, "", new List<Film>());
                        }
                    }
                    else
                    {
                        if (checkBoxActeur.IsChecked.Value)
                        {
                            pers = new Acteur(TextBoxNom.Text, TextBoxPrenom.Text, mImage.Source.ToString(), new List<Film>());
                        }
                        else
                        {
                            pers = new Realisateur(TextBoxNom.Text, TextBoxPrenom.Text, mImage.Source.ToString(), new List<Film>());
                        }
                    }

                    regardeSiPersonneExisteEtAjoute(pers);

                }
                else
                {
                    MessageBox.Show("Remplissez le prénom.");
                }
            }
            else
            {
                MessageBox.Show("Remplissez le nom.");
            }


        }

        private void regardeSiPersonneExisteEtAjoute(Personne pers)
        {
            bool existe = false;
            foreach (Personne p in ((App)Application.Current).managerVM.manager.Personnes)
            {
                if (p.Equals(pers))
                {
                    existe = true;
                }
            }

            if (existe == true)
            {
                if (pers is Realisateur)
                {
                    MessageBox.Show("Ce réalisateur existe déjà.");
                }
                else
                {
                    MessageBox.Show("Cet acteur existe déjà.");
                }
            }
            else
            {
                ((App)Application.Current).managerVM.manager.ajoutePersonne(pers);

                ((App)Application.Current).managerVM.ListeacteursSelectionné = ((App)Application.Current).managerVM.manager.Acteurs;
                if (pers is Realisateur)
                {
                    NavigationService.Navigate(new Uri("/ListeRealisateurs.xaml", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri("/ListeActeurs.xaml", UriKind.Relative));
                }

            }
        }

        private void click_ckeckBoxActeur(object sender, RoutedEventArgs e)
        {

            checkBoxActeur.IsChecked = true;
            checkBoxRealisateur.IsChecked = false;

        }
        private void click_ckeckBoxRealisateur(object sender, RoutedEventArgs e)
        {
            checkBoxActeur.IsChecked = false;
            checkBoxRealisateur.IsChecked = true;
        }



    }
}