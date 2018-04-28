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
    public partial class AjoutFilm : PhoneApplicationPage
    {
        /// <summary>
        /// constructeur qui initialise les composants et qui s'abonne à l'évenement pour prendre la photo
        /// </summary>
        public AjoutFilm()
        {
            InitializeComponent();
            mCameraCaptureTask = new CameraCaptureTask();
            mCameraCaptureTask.Completed += new EventHandler<PhotoResult>(mCameraCaptureTask_Completed);
        }

        CameraCaptureTask mCameraCaptureTask;

        /// <summary>
        /// prend la photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// une fois que la photo est prise on l'affiche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mCameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                mImage.Source = bmp;
            }
        }
        /// <summary>
        /// evenement qui s'execute quand le text titre prend le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gotFocusTitre(object sender, RoutedEventArgs e)
        {
            if (TextBoxtitre.Text.Equals("Titre"))
            {
                TextBoxtitre.Text = "";
            }
        }
        /// <summary>
        /// evenement qui s'execute quand le text titre perd le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lostFocusTitre(object sender, RoutedEventArgs e)
        {
            if (TextBoxtitre.Text.Length == 0)
            {
                TextBoxtitre.Text = "Titre";
            }
        }

        /// <summary>
        /// evenement qui s'execute quand le text Resumé prend le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gotFocusResume(object sender, RoutedEventArgs e)
        {
            if (TextBoxresume.Text.Equals("Résumé"))
            {
                TextBoxresume.Text = "";
            }
        }
        /// <summary>
        /// evenement qui s'execute quand le text Resumé perd le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lostFocusResume(object sender, RoutedEventArgs e)
        {
            if (TextBoxresume.Text.Length == 0)
            {
                TextBoxresume.Text = "Résumé";
            }
        }
        /// <summary>
        /// evenement qui s'execute quand le text Note prend le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gotFocusNote(object sender, RoutedEventArgs e)
        {
            if (TextBoxnote.Text.Equals("Note"))
            {
                TextBoxnote.Text = "";
            }
        }
        /// <summary>
        /// evenement qui s'execute quand le text Note perd le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lostFocusNote(object sender, RoutedEventArgs e)
        {
            if (TextBoxnote.Text.Length == 0)
            {
                TextBoxnote.Text = "Note";
            }
        }
        /// <summary>
        /// méthode appeller pour ajouter un film
        /// test le contenu des champs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void click_ValidationAjoutFilm(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxtitre.Text))
            {
                if (!string.IsNullOrWhiteSpace(TextBoxresume.Text))
                {
                    if (string.IsNullOrWhiteSpace(TextBoxnote.Text))
                    {
                        MessageBox.Show("Remplissez la note");
                    }
                    else
                    {
                        if (BoxGenres.SelectedItems.Count == 0)
                        {
                            MessageBox.Show("Saisir un genre");
                        }
                        else
                        {
                            if (BoxRealisateurs.SelectedItems.Count == 0)
                            {
                                MessageBox.Show("Saisir un réalisateur");
                            }
                            else
                            {
                                if (BoxActeurs.SelectedItems.Count == 0)
                                {
                                    MessageBox.Show("Saisir un Acteur");
                                }
                                else
                                {
                                    ajoutFilm();
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Remplissez le résumé");
                }
            }
            else
            {
                MessageBox.Show("Remplissez le titre");
            }
        }

        /// <summary>
        /// ajoute un film
        /// </summary>
        private void ajoutFilm()
        {
            List<Genre> listeGenres = new List<Genre>();
            foreach (var element in BoxGenres.SelectedItems)
            {
                listeGenres.Add(element as Genre);
            }

            List<Acteur> listeActeurs = new List<Acteur>();
            foreach (var element in BoxActeurs.SelectedItems)
            {
                listeActeurs.Add(element as Acteur);
            }
            List<Realisateur> listeRealisateurs = new List<Realisateur>();
            foreach (var element in BoxRealisateurs.SelectedItems)
            {
                listeRealisateurs.Add(element as Realisateur);
            }


            TimeSpan time = DateTimeToTimeSpan(timePickerDuree.Value);
            Film f;
            if (mImage.Source != null)
            {
                f = new Film(TextBoxtitre.Text, TextBoxresume.Text, Int16.Parse(TextBoxnote.Text), time, listeGenres, listeRealisateurs, listeActeurs, mImage.Source.ToString());
            }
            else
            {
                f = new Film(TextBoxtitre.Text, TextBoxresume.Text, Int16.Parse(TextBoxnote.Text), time, listeGenres, listeRealisateurs, listeActeurs, "");
            }
            regardeSiExisteEtAjoute(f);
        }
        /// <summary>
        /// regarde si le film existe et l'ajoute
        /// </summary>
        /// <param name="f"></param>
        private void regardeSiExisteEtAjoute(Film f)
        {
            bool existe = false;
            foreach (Film film in ((App)Application.Current).managerVM.manager.Films)
            {
                if (film.Equals(f))
                {
                    existe = true;
                }
            }

            if (existe == false)
            {
                ((App)Application.Current).managerVM.manager.ajouteFilm(f);
            }
            else
            {
                MessageBox.Show("Le film existe déjà.");
            }

            ((App)Application.Current).managerVM.ListefilmsSelectionné = ((App)Application.Current).managerVM.manager.Films;
            NavigationService.Navigate(new Uri("/PageListeFilms.xaml", UriKind.Relative));
        }

        /// <summary>
        /// méthode de conversion de datetime en timeSpan
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        TimeSpan DateTimeToTimeSpan(DateTime? ts)
        {
            if (!ts.HasValue) return TimeSpan.Zero;
            else return new TimeSpan(0, ts.Value.Hour, ts.Value.Minute, ts.Value.Second, ts.Value.Millisecond);
        }

        /// <summary>
        /// evenement qui s'éxécute au chargement de la page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void load_ajoutFilms(object sender, RoutedEventArgs e)
        {
            DataContext = ((App)Application.Current).managerVM.manager;
        }


    }
}