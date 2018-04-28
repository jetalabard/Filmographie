using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Metier;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Metier.Parser;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using PersistanceXML;
using System.IO;

namespace ManagerXMLWP7
{
    /// <summary>
    /// manager permettant de récupérer ou de stocker les données de l'application en xml dans la chambre d'isolation
    /// </summary>
    public class ManagerXMLWindowsPhone : ManagerXML
    {
        /// <summary>
        /// constructeur permettant de remplir toutes les listes
        /// </summary>
        public ManagerXMLWindowsPhone()
            : base()
        {
            recupereGenres();
            recuperePersonne();
            recupereFilms();
        }
        /// <summary>
        /// liste de genres
        /// </summary>
        List<Genre> listeGenres = new List<Genre>();
        /// <summary>
        /// liste de toutes les personnes
        /// </summary>
        List<Personne> listePersonnes = new List<Personne>();
        /// <summary>
        /// liste de tous les films
        /// </summary>
        List<Film> listeFilms = new List<Film>();

        public override IEnumerable<Genre> Genres
        {
            get { return listeGenres; }
        }

        public override IEnumerable<Film> Films
        {
            get { return listeFilms; }
        }

        public override IEnumerable<Personne> Personnes
        {
            get { return listePersonnes; }
        }

        /// <summary>
        /// Récupère tous les genres dans le fichier genres.xml
        /// </summary>
        /// <returns>La liste de tous les genres</returns>
        private IEnumerable<Genre> recupereGenres()
        {
            listeGenres.Clear();

            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolatedStorage.FileExists(XMLTags.FICHIER_GENRES))
                {
                    using (IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorage.OpenFile(XMLTags.FICHIER_GENRES, System.IO.FileMode.Open))
                    {
                        System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                        settings.IgnoreWhitespace = true;
                        settings.IgnoreComments = true;
                        settings.IgnoreProcessingInstructions = true;

                        using (StreamReader reader = new StreamReader(isolatedStorageFileStream))
                        {
                            recupereDonneesGenresDansChambreIsolation(reader);
                        }
                    }
                }
                else
                {
                    sauvegarderData(isolatedStorage);
                }
            }

            return listeGenres;
        }

        /// <summary>
        /// parse le fichier genres.xml ligne par ligne et construit la liste
        /// </summary>
        /// <param name="reader"></param>
        private void recupereDonneesGenresDansChambreIsolation(StreamReader reader)
        {
            String xmlLu = "";
            String lignelu = null;
            lignelu = reader.ReadLine();
            while (lignelu != null)
            {
                xmlLu = xmlLu + lignelu;
                lignelu = reader.ReadLine();
            }
            XDocument xdom = XDocument.Parse(xmlLu);
            GenreReader genreReader = new GenreReader(xdom);
            foreach (Genre genre in genreReader.load())
            {
                listeGenres.Add(genre);
            }
            listeGenres = listeGenres
                .OrderBy(genre => genre.NomGenre)
                .ToList();
        }

        /// <summary>
        /// Récupère toutes les personnes du fichier personnes.xml
        /// </summary>
        /// <returns>La liste de toutes les personnes</returns>
        private IEnumerable<Personne> recuperePersonne()
        {
            listePersonnes.Clear();

            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolatedStorage.FileExists(XMLTags.FICHIER_PERSONNES))
                {
                    using (IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorage.OpenFile(XMLTags.FICHIER_PERSONNES, System.IO.FileMode.Open))
                    {
                        System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                        settings.IgnoreWhitespace = true;
                        settings.IgnoreComments = true;
                        settings.IgnoreProcessingInstructions = true;

                        using (StreamReader reader = new StreamReader(isolatedStorageFileStream))
                        {
                            recupereDonneesPersonnesDansChambreIsolation(reader);
                        }
                    }
                }
                else
                {
                    sauvegarderData(isolatedStorage);
                }
            }
            return listePersonnes;
        }
        /// <summary>
        /// parse le fichier personne et construit la liste de personnes
        /// </summary>
        /// <param name="reader"></param>
        private void recupereDonneesPersonnesDansChambreIsolation(StreamReader reader)
        {
            String xmlLu = "";
            String lignelu = null;
            lignelu = reader.ReadLine();
            while (lignelu != null)
            {
                xmlLu = xmlLu + lignelu.Trim();
                lignelu = reader.ReadLine();
            }
            XDocument xdom = XDocument.Parse(xmlLu);
            PersonneReader personnereader = new PersonneReader(xdom);
            foreach (Personne p in personnereader.load())
            {
                listePersonnes.Add(p);
            }
        }


        /// <summary>
        /// remplit les listes de données et sauvegarde les données dans la chambre d'isolation
        /// </summary>
        /// <param name="isolatedStorage"></param>
        private void sauvegarderData(IsolatedStorageFile isolatedStorage)
        {
            listeGenres = new List<Genre>();
            listePersonnes = new List<Personne>();
            listeFilms = new List<Film>();
            remplirListe();
            mettreDonneesDansIsolatedStorage(isolatedStorage);
        }
        /// <summary>
        /// sauvegarde les données dans la chambre d'isolation
        /// </summary>
        /// <param name="isolatedStorage"></param>
        private void mettreDonneesDansIsolatedStorage(IsolatedStorageFile isolatedStorage)
        {
            saveGenres(isolatedStorage);
            saveFilms(isolatedStorage);
            savePersonnes(isolatedStorage);

        }
        /// <summary>
        /// sauvegarde les personnes en chambre d'isolation
        /// </summary>
        /// <param name="isolatedStorage"></param>
        private void savePersonnes(IsolatedStorageFile isolatedStorage)
        {
            using (var isolatedStorageFileStream = isolatedStorage.OpenFile(XMLTags.FICHIER_PERSONNES, System.IO.FileMode.Create))
            {
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                settings.Indent = true;

                using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(isolatedStorageFileStream, settings))
                {
                    PersonneWriter writerPersonne = new PersonneWriterWP7();
                    writerPersonne.mXDoc = new XDocument();
                    writerPersonne.mXDoc.Add(new XElement(XMLTags.PERSONNES));
                    foreach (Personne p in listePersonnes)
                    {
                        writerPersonne.saveData(p, null);
                    }
                    writerPersonne.mXDoc.Save(writer);

                }
            }
        }
        /// <summary>
        /// sauvegarde les films en chambre d'isolation
        /// </summary>
        /// <param name="isolatedStorage"></param>
        private void saveFilms(IsolatedStorageFile isolatedStorage)
        {
            using (var isolatedStorageFileStream = isolatedStorage.OpenFile(XMLTags.FICHIER_FILMOTHEQUE, System.IO.FileMode.Create))
            {
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                settings.Indent = true;

                using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(isolatedStorageFileStream, settings))
                {
                    FilmWriter writerFilm = new FilmWriterWP7();
                    writerFilm.mXDoc = new XDocument();
                    writerFilm.mXDoc.Add(new XElement(XMLTags.FILMS));
                    foreach (Film f in listeFilms)
                    {
                        writerFilm.saveData(f, null);
                    }
                    writerFilm.mXDoc.Save(writer);

                }
            }
        }
        /// <summary>
        /// sauvegarde les genres en chambre d'isolation
        /// </summary>
        /// <param name="isolatedStorage"></param>
        private void saveGenres(IsolatedStorageFile isolatedStorage)
        {
            using (var isolatedStorageFileStream = isolatedStorage.OpenFile(XMLTags.FICHIER_GENRES, System.IO.FileMode.Create))
            {
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                settings.Indent = true;

                using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(isolatedStorageFileStream, settings))
                {
                    GenreWriter writerGenre = new GenreWriterWP7();
                    writerGenre.mXDoc = new XDocument();
                    writerGenre.mXDoc.Add(new XElement(XMLTags.GENRES));
                    foreach (Genre g in listeGenres)
                    {
                        writerGenre.saveData(g, null);
                    }
                    writerGenre.mXDoc.Save(writer);

                }
            }
        }
        /// <summary>
        /// si aucune données en chambre d'isolation alors récupere les données dans les fichiers xml de la solution WP7Filmographie -> Data
        /// </summary>
        /// <param name="isolatedStorage"></param>
        private void remplirListe()
        {
            GenreReader readerGenre = new GenreReader("/WP7Filmographie;component/Data/" + XMLTags.FICHIER_GENRES);
            foreach (Genre g in readerGenre.load())
            {
                listeGenres.Add(g);
            }
            listeGenres = listeGenres
                .OrderBy(genre => genre.NomGenre)
                .ToList();
            FilmReader filmReader = new FilmReader("/WP7Filmographie;component/Data/" + XMLTags.FICHIER_FILMOTHEQUE);
            foreach (Film f in filmReader.load())
            {
                listeFilms.Add(f);
            }
            listeFilms = listeFilms
                .OrderBy(film => film.Titre)
                .ToList();
            PersonneReader persReader = new PersonneReader("/WP7Filmographie;component/Data/" + XMLTags.FICHIER_PERSONNES);
            foreach (Personne pers in persReader.load())
            {
                listePersonnes.Add(pers);
            }
            listePersonnes = listePersonnes
                .OrderBy(personne => personne.Nom)
                .ToList();

        }

        /// <summary>
        /// Récupère tous les films du fichier filmothèque.xml
        /// </summary>
        /// <returns>La liste des films</returns>
        private IEnumerable<Film> recupereFilms()
        {
            listeFilms.Clear();

            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolatedStorage.FileExists(XMLTags.FICHIER_FILMOTHEQUE))
                {
                    using (IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorage.OpenFile(XMLTags.FICHIER_FILMOTHEQUE, System.IO.FileMode.Open))
                    {
                        System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                        settings.IgnoreWhitespace = true;
                        settings.IgnoreComments = true;
                        settings.IgnoreProcessingInstructions = true;


                        using (StreamReader reader = new StreamReader(isolatedStorageFileStream))
                        {
                            recupereDonneesFilmsDansChambreIsolation(reader);

                        }
                    }
                }
                else
                {
                    sauvegarderData(isolatedStorage);
                }

            }

            changeActeurFilmsAvecVraiActeur();
            changeRealisateurFilmsAvecVraiRealisateur();

            return listeFilms;
        }
        /// <summary>
        /// parse fichier films et construit la liste de films
        /// </summary>
        /// <param name="reader"></param>
        private void recupereDonneesFilmsDansChambreIsolation(StreamReader reader)
        {
            String xmlLu = "";
            String lignelu = null;
            lignelu = reader.ReadLine();
            while (lignelu != null)
            {
                xmlLu = xmlLu + lignelu;
                lignelu = reader.ReadLine();
            }
            XDocument xdom = XDocument.Parse(xmlLu);
            FilmReader filmReader = new FilmReader(xdom);
            foreach (Film f in filmReader.load())
            {
                listeFilms.Add(f);
            }
        }
        /// <summary>
        /// actualise la liste de réalisateur contenu dans le film avec les vrais données du fichier personne
        /// </summary>
        private void changeRealisateurFilmsAvecVraiRealisateur()
        {
            List<Realisateur> listeVraiRealisateur = listePersonnes
                .Where(pers => pers is Realisateur)
                .Select(pers => pers as Realisateur).ToList();

            foreach (Film film in listeFilms)
            {
                film.Realisateurs = listeVraiRealisateur.Intersect(film.Realisateurs).ToList();
            }

        }
        /// <summary>
        /// actualise la liste d'acteur contenu dans le film avec les vrais données du fichier personne
        /// </summary>
        private void changeActeurFilmsAvecVraiActeur()
        {
            List<Acteur> listeVraiActeurs = listePersonnes
                 .Where(pers => pers is Acteur)
                 .Select(pers => pers as Acteur).ToList();

            foreach (Film film in listeFilms)
            {
                film.Acteurs = listeVraiActeurs.Intersect(film.Acteurs).ToList();

            }
        }



        /// <summary>
        /// Ajoute un film au fichier filmothèque.xml
        /// </summary>
        /// <param name="film">Le film à ajouter</param>
        public override void ajouteFilm(Film film)
        {
            listeFilms.Add(film);
            List<Realisateur> listeVraiRealisateur = listePersonnes
               .Where(pers => pers is Realisateur)
               .Select(pers => pers as Realisateur).ToList();
            List<Acteur> listeVraiActeur = listePersonnes
              .Where(pers => pers is Acteur)
              .Select(pers => pers as Acteur).ToList();

            listeVraiRealisateur = film.Realisateurs.Intersect(listeVraiRealisateur).ToList();
            listeVraiActeur = film.Acteurs.Intersect(listeVraiActeur).ToList();

            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                saveFilms(isolatedStorage);
                savePersonnes(isolatedStorage);
            }

        }

        /// <summary>
        /// Ajoute un genre au fichier genres.xml
        /// </summary>
        /// <param name="genre">Le genre à ajouter</param>
        public override void ajouteGenre(Genre genre)
        {
            listeGenres.Add(genre);
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                saveGenres(isolatedStorage);
            }
        }

        /// <summary>
        /// Ajoute une personne au fichier personnes.xml
        /// </summary>
        /// <param name="personne">La personne</param>
        public override void ajoutePersonne(Personne personne)
        {
            listePersonnes.Add(personne);

            if (personne is Realisateur)
            {
                Realisateurs.ToList().Add(personne as Realisateur);
            }
            else
            {
                Acteurs.ToList().Add(personne as Acteur);
            }

            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                savePersonnes(isolatedStorage);
            }
        }

        /// <summary>
        /// Réecris le fichier filmothèque.xml
        /// </summary>
        public override void writeFilms()
        {
            ListeFilm liste = new ListeFilm();
            liste.initialiseListeFilms(this);

            FilmWriter writer = new FilmWriterWP7();

            writer.WriteFilms(liste.ListeFilms);
            writer.Save(XMLTags.FICHIER_FILMOTHEQUE);
        }

        /// <summary>
        /// Réécris le fichier personnes.xml
        /// </summary>
        public override void writePersonnes()
        {
            ListePersonne liste = new ListePersonne();
            liste.InitialiseListePersonne();

            PersonneWriter writer = new PersonneWriterWP7();

            writer.WritePersonnes(liste.listePersonne);
            writer.Save(XMLTags.FICHIER_PERSONNES);
        }

        /// <summary>
        /// Met à jour la liste des films d'une personne        
        /// </summary>
        /// <param name="pers">La personne</param>
        /// <param name="film">Le film a ajouter</param>
        public override Personne mettreAJourListeFilmsDeLaPersonne(Personne pers, Film film)
        {
            pers.listeFilms.Add(film);

            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorage.OpenFile(XMLTags.FICHIER_PERSONNES, System.IO.FileMode.Open))
                {

                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                    settings.Indent = true;

                    using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(isolatedStorageFileStream, settings))
                    {
                        PersonneWriter writerPersonne = new PersonneWriterWP7();
                        writerPersonne.mXDoc = new XDocument();
                        writerPersonne.mXDoc.Add(new XElement(XMLTags.PERSONNES));
                        foreach (Personne g in listePersonnes)
                        {
                            writerPersonne.saveData(g, null);
                        }
                        writerPersonne.UpdateListeFilmsPersonne(pers, film, null);

                        writerPersonne.mXDoc.Save(writer);

                    }


                }
            }
            return pers;
        }
    }
}
