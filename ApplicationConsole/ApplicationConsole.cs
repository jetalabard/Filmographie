
using System;
using System.Collections.Generic;
using System.Linq;

using Metier;
using System.Text.RegularExpressions;

namespace Main
{
    public class ApplicationConsole
    {
        /// <summary>
        /// gere les données
        /// </summary>
        public Manager manager
        {
            get;
            private set;
        }
        /// <summary>
        /// personne connecté à l'application
        /// </summary>
        public Personne personneCourante
        {
            get;
            private set;
        }
        /// <summary>
        /// nombre de top à afficher
        /// </summary>
        public int NbTop
        {
            get;
            private set;
        }
        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="m"></param>
        public ApplicationConsole(Manager m)
        {
            manager = m;
            NbTop = 10;
            Connexion();

        }
        /// <summary>
        /// premier écran de l'application où l'on peut se connecter
        /// </summary>
        private void Connexion()
        {
            Console.WriteLine("********************** CONNEXION ***************************\n");

            Console.WriteLine("1. Nouvel utilisateur");
            Console.WriteLine("2. Connexion");
            Console.WriteLine("0. Quitter");
            string choix = Console.ReadLine();  
            switch (choix)
            {
                case "1": CreerNouvelUtilsateur();
                    break;
                case "2": DemanderIdentifiant();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// permet de créer un nouvel utilisateur
        /// </summary>
        private void CreerNouvelUtilsateur()
        {
            Console.WriteLine("********************** NOUVEL UTILISATEUR ***************************\n");
            Console.WriteLine("\nSaisissez votre nom : ");
            String nom = Console.ReadLine();
            Console.WriteLine("\nSaisissez votre prénom : ");
            String prenom = Console.ReadLine();
            Personne pers = new Personne(nom, prenom, " ", null);
            if (manager.Personnes.Where(personne => personne.Equals(pers)).Count() > 0)
            {
                Console.WriteLine("Utilisateur déjà enregistré");
                CreerNouvelUtilsateur();
            }
            else
            {
                manager.ajoutePersonne(pers);
                personneCourante = pers;
                AfficheMenuDebutApplication();
            }

        }
        /// <summary>
        /// demande les identifiants d'un personne déjà enregistrée
        /// </summary>
        private void DemanderIdentifiant()
        {
            Console.WriteLine("********************** IDENTIFICATION ***************************\n");
            Console.WriteLine("\nSaisir nom");
            string nom = Console.ReadLine();
            personneCourante = manager.recupereUnePersonne(nom);
            if (personneCourante == null)
            {
                Console.WriteLine("Ce nom n'est pas enregistré");
                Console.WriteLine("\n1. Recommencer saisie");
                Console.WriteLine("0. Quitter");
                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1": DemanderIdentifiant();
                        break;
                    default:
                        Connexion();
                        break;
                }
            }
            else
            {
                AfficheMenuDebutApplication();
            }
        }

        /// <summary>
        /// affiche le menu du début de l'application
        /// </summary>
        public void AfficheMenuDebutApplication()
        {
            Console.WriteLine("********************** MENU ***************************\n");
            Console.WriteLine("1. Afficher tous les films");
            Console.WriteLine("2. Afficher par réalisateur");
            Console.WriteLine("3. Afficher par genre");
            Console.WriteLine("4. Afficher les favoris");
            Console.WriteLine("5. Afficher le top 50");
            Console.WriteLine("6. Effectuer un ajout");
            Console.WriteLine("7. Paramètres");
            Console.WriteLine("0. Quitter");
            Console.WriteLine("\n\n Saisissez un chiffre");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1": AfficheTousLesfilms();
                    break;
                case "2": AfficheParRealisateur();
                    break;
                case "3": AfficheParGenre();
                    break;
                case "4": AfficheFavoris();
                    break;
                case "5": AfficheTop();
                    break;
                case "6": AfficheMenuAjouter();
                    break;
                case "7": ChangerParametre();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// affiche tous les films enregistrés
        /// </summary>
        private void AfficheTousLesfilms()
        {
            Console.WriteLine("********************** TOUS LES FILMS ***************************\n");
            List<Film> listeFilms = manager.Films.ToList();
            AfficheFilms(listeFilms);
        }
        /// <summary>
        /// affiche la liste de films passé en parametres
        /// </summary>
        /// <param name="listeFilms"></param>
        private void AfficheFilms(IEnumerable<Film> listeFilms)
        {
            int i = 1;
            if (listeFilms != null && listeFilms.Count() > 0)
            {

                foreach (Film f in listeFilms)
                {
                    Console.WriteLine(i + ". " + f.Titre);
                    i++;
                }
                Console.WriteLine("\n\nSaisissez le film : " + "(0. Retour)");
                string choixFilm = Console.ReadLine();
                if (choixFilm.Equals("0"))
                {
                    AfficheMenuDebutApplication();
                }
                else
                {
                    int num = 0;
                    bool valide = true;
                    try
                    {
                        num = Convert.ToInt16(choixFilm);
                        
                    }
                    catch
                    {
                        valide = false;
                    }


                    if (valide == false)
                    {
                        Console.WriteLine("Valeur incorrect\n");
                        AfficheFilms(listeFilms);
                    }
                    else
                    {
                        AfficheDetailFilm(listeFilms.ToList()[num - 1]);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nIl n'y a pas de films\n");
                AfficheMenuDebutApplication();
            }
        }


        /// <summary>
        /// affiche le détail d'un film
        /// </summary>
        /// <param name="film"></param>
        private void AfficheDetailFilm(Film film)
        {
            Console.WriteLine("********************** " + film.Titre.ToUpper() + " ***************************\n");
            Console.WriteLine(film.ToString());
            AfficherMenuDetailFilm(film);

        }
        /// <summary>
        /// affiche le menu permettant d'aller vers le détails des acteurs ou des réalisateurs d'un film
        /// </summary>
        /// <param name="film"></param>
        private void AfficherMenuDetailFilm(Film film)
        {
            Console.WriteLine("1. Detail réalisateur(s)");
            Console.WriteLine("2. Detail Acteur(s)");

            if (personneCourante.listeFilms == null)
            {
                personneCourante.listeFilms = new List<Film>();
            }

            //test si le film est déjà dans les favoris
            bool existe = false;
            int filmRetourne = personneCourante.listeFilms.Where(f => f.Titre.Equals(film.Titre)).Count();
            if (filmRetourne == 0)
            {
                Console.WriteLine("3. Ajouter ce film à mes favoris");
            }
            else
            {
                existe = true;
            }

            Console.WriteLine("0. Retour");
            Console.WriteLine("\n\n Choississez un chiffre");

            string choix = Console.ReadLine();
            switch (choix)
            {
                case "1": Console.WriteLine("\nQuel réalisateur ?");
                    AfficherRealisateurs(film.Realisateurs);
                    break;

                case "2": Console.WriteLine("\nQuel acteur ?");
                    AfficherActeurs(film.Acteurs.ToList());
                    break;
                case "3":
                    if (!existe)
                    {
                        personneCourante = manager.mettreAJourListeFilmsDeLaPersonne(personneCourante, film);
                        Console.WriteLine("Favoris ajouté");
                    }
                    AfficheTousLesfilms();
                    break;
                default:
                    AfficheTousLesfilms();
                    break;
            }
        }
        /// <summary>
        /// affiche les films en fonctions d'un genre saisi
        /// </summary>
        private void AfficheParGenre()
        {
            Console.WriteLine("********************** TOUS LES GENRES ***************************\n");

            List<Genre> listeGenres = manager.Genres.ToList();
            int i = 1;
            foreach (Genre g in listeGenres)
            {
                Console.WriteLine(i + ". " + g);
                i++;
            }
            Console.WriteLine("\n\nSaisissez le genre : (0 pour retour)");

            string genreChoisi = Console.ReadLine();

            int num = Convert.ToInt16(genreChoisi);
            if (num == 0)
            {
                AfficheMenuDebutApplication();
            }
            List<Film> listeFilmParGenre = manager.recupereFilmsParGenre(listeGenres[num - 1]).ToList();
            AfficheFilms(listeFilmParGenre);
        }
        /// <summary>
        /// affiche les films par réalisateurs
        /// </summary>
        private void AfficheParRealisateur()
        {

            Console.WriteLine("********************** TOUS LES REALISATEURS ***************************\n");

            List<Realisateur> listeRealisateurs = manager.Realisateurs.ToList();
            int i = 1;
            foreach (Realisateur r in listeRealisateurs)
            {
                Console.WriteLine(i + ". " + r);
                i++;
            }
            Console.WriteLine("\n\nSaisissez le réalisateur : ");

            string choixRealisateur = Console.ReadLine();

            int num = Convert.ToInt16(choixRealisateur);

            AfficheDetailRealisateur(listeRealisateurs[num - 1]);
        }


        /// <summary>
        /// affiche les acteurs
        /// </summary>
        /// <param name="list"></param>
        private void AfficherActeurs(List<Acteur> list)
        {
            int i = 1;
            foreach (Acteur a in list)
            {
                Console.WriteLine(i + ". " + a.Nom);
                i++;
            }
            Console.WriteLine("\n\nSaisissez l'acteur : ");
            string choix = Console.ReadLine();
            int num = Convert.ToInt16(choix);
            AfficheDetailActeur(list[num - 1]);
        }
        /// <summary>
        /// affiche le détail de l'acteur
        /// </summary>
        /// <param name="acteur"></param>
        private void AfficheDetailActeur(Acteur acteur)
        {
            Console.WriteLine("********************** " + acteur.Nom.ToUpper() + " ***************************\n");
            Console.WriteLine(acteur.ToString());
            List<Film> listeFilmsActeur = manager.recupereFilmsParActeurs(acteur).ToList();
            Console.WriteLine("\n\nListe des films joués : ");
            AfficheFilms(listeFilmsActeur);
        }
        /// <summary>
        /// affiche les réalisateurs
        /// </summary>
        /// <param name="list"></param>
        private void AfficherRealisateurs(List<Realisateur> list)
        {
            int i = 1;
            foreach (Realisateur r in list)
            {
                Console.WriteLine(i + ". " + r.Nom);
                i++;
            }

            Console.WriteLine("\n\nSaisissez le chiffre du réalisateur : ");
            string choix = Console.ReadLine();
            int num = Convert.ToInt16(choix);
            AfficheDetailRealisateur(list[num - 1]);
        }

        /// <summary>
        /// affiche le détail d'un réalisateur
        /// </summary>
        /// <param name="realisateur"></param>
        private void AfficheDetailRealisateur(Realisateur realisateur)
        {
            Console.WriteLine("********************** " + realisateur.Nom.ToUpper() + " ***************************\n");
            Console.WriteLine(realisateur.ToString());
            List<Film> listeFilmsRealisateur = manager.recupereFilmsParRealisateur(realisateur).ToList();
            Console.WriteLine("\n\nListe des films réalisés : ");
            AfficheFilms(listeFilmsRealisateur);
        }

        /// <summary>
        /// affiche le top des films
        /// </summary>
        private void AfficheTop()
        {
            Console.WriteLine("********************** TOP DES FILMS ***************************\n");
            AfficheFilms(manager.recupereTopFilms(NbTop).ToList());
        }

        private void AfficheFavoris()
        {
            Console.WriteLine("********************** FAVORIS ***************************\n");
            AfficheFilms(personneCourante.listeFilms);
        }


        /// <summary>
        /// affiche le menu pour ajouter un élément
        /// </summary>
        private void AfficheMenuAjouter()
        {
            Console.WriteLine("********************** AJOUT ***************************\n");
            Console.WriteLine("1. Ajouter un nouveau film");
            Console.WriteLine("2. Ajouter genre");
            Console.WriteLine("0. Retour");

            Console.WriteLine("\n\n Saisissez un nombre : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1": AfficheAjoutUnFilm();
                    break;
                case "2": AfficheAjoutUnGenre(0);
                    break;
                default: AfficheMenuDebutApplication();
                    break;
            }

        }
        /// <summary>
        /// ajoute un nouveau genre
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private Genre AfficheAjoutUnGenre(int mode)
        {
            bool existe = true;
            Console.WriteLine("********************** NOUVEAU GENRE ***************************\n");
            Console.WriteLine("Quel est le nom du genre ?");
            string nom = Console.ReadLine();
            List<Genre> listTmp = manager.Genres.ToList();
            while (existe)
            {
                foreach (Genre g in listTmp)
                {
                    if (g.NomGenre.Equals(nom))
                    {
                        Console.WriteLine("Ce genre existe déjà!\n");
                        Console.WriteLine("Insérer un nouveau genre ou appuyer sur 0 pour quitter\n");
                        nom = Console.ReadLine();
                        if (nom.Equals("0"))
                        {
                            AfficheMenuAjouter();
                        }
                    }
                    else
                    {
                        existe = false;
                    }

                }
            }
            Console.WriteLine("Quelle est la description de ce genre ?\n");
            string description = Console.ReadLine();
            Genre genre = new Genre(nom, description);
            manager.ajouteGenre(genre);
            if (mode == 0)
            {
                AfficheMenuAjouter();
            }
            return genre;

        }
        /// <summary>
        /// ajoute un nouveau film
        /// </summary>
        private void AfficheAjoutUnFilm()
        {
            bool existe = true;
            Console.WriteLine("********************** NOUVEAU FILM ***************************\n");
            Console.WriteLine("Quel est le Titre du film ?");
            string titre = Console.ReadLine();
            List<Film> listTmp = manager.Films.ToList();
            while (existe)
            {
                foreach (Film g in listTmp)
                {
                    if (g.Titre.Equals(titre))
                    {
                        Console.WriteLine("Ce film existe déjà!\n");
                        Console.WriteLine("Insérer un nouveau film ou appuyer sur 0 pour quitter\n");
                        titre = Console.ReadLine();
                        if (titre.Equals("0"))
                        {
                            AfficheMenuAjouter();
                        }
                    }
                    else
                    {
                        existe = false;
                    }

                }
            }

            //saisie de la durée du film
            string duree;
            // TimeSpan val;
            //bool valide = false;
            //while (!false)
            // {
            Console.WriteLine("Quelle est la durée de ce film ? (HH:MM:SS) \n");
            duree = Console.ReadLine();
            //valide = TimeSpan.TryParse(duree,out val);
            //}

            // saisie de la note du film
            Console.WriteLine("Quelle est la note de ce film (/10) ?\n");
            string note = Console.ReadLine();

            while (!regardeSiNoteSaisieValide(note))
            {
                Console.WriteLine("Quelle est la note de ce film (/10) ?\n");
                note = Console.ReadLine();
            }

            //saisie du résumé
            Console.WriteLine("Quelle est le résumé de ce film ?\n");
            string resume = Console.ReadLine();

            //saisie de l'image
            string image = "";
            //Console.WriteLine("Quelle est le le chemin de l'image de ce film ?\n");
            //image = Console.ReadLine();

            //saisie des genres du film
            List<Genre> genres = gereGenre();

            //saisie des acteurs
            List<Acteur> acteurs = gereActeurs();


            //saisie des réalisateurs
            List<Realisateur> realisateurs = geresRealisateurs();

            Film f = new Film(titre, resume, Int16.Parse(note), TimeSpan.Parse(duree), genres, realisateurs, acteurs, image);
            manager.ajouteFilm(f);


            Console.WriteLine("Le film " + f.Titre + " a été ajouté avec succées\n");
            AfficheMenuAjouter();
        }
        /// <summary>
        /// enregistre les genres du film qui est entrain d'etre créé si les genres n'existe pas on les créés et les enregistres
        /// autant de fois qu'on veut rajouter un genre
        /// </summary>
        /// <returns>liste de genres</returns>
        private List<Genre> gereGenre()
        {
            bool encoreGenre = true;
            int cpt = 0;
            List<Genre> genresAAjouterAuFilm = new List<Genre>();
            List<Genre> genres = manager.Genres.ToList();
            while (encoreGenre)
            {
                if (cpt == 0)
                {
                    Console.WriteLine("Quelle sont le(s) genre(s) de ce film ?\n");
                }
                else
                {
                    Console.WriteLine("Quelle sont le(s) autres genre(s) de ce film ?\n");
                }

                List<Genre> tmp = genres.Intersect(genresAAjouterAuFilm).ToList();
                foreach (Genre g in tmp)
                {
                    genres.Remove(g);
                }

                int i = 1;
                foreach (Genre g in genres)
                {
                    Console.WriteLine(i + ". " + g.ToString());
                    i++;
                }
                Console.WriteLine("* : Ajouter un nouveau genre\n");

                Console.WriteLine("\n\n Saisissez un caractère : ");
                string choixGenre = Console.ReadLine();

                if (choixGenre.Equals("*"))
                {
                    genresAAjouterAuFilm.Add(AfficheAjoutUnGenre(1));
                }
                else
                {
                    int num = Convert.ToInt16(choixGenre);
                    genresAAjouterAuFilm.Add(genres[num - 1]);
                }

                cpt++;
                Console.WriteLine("Est-ce qu'il y a d'autres genres pour ce film (y/n) ?\n\n");
                string reponse = Console.ReadLine();
                if (reponse.Equals("n") || reponse.Equals("no") || reponse.Equals("non"))
                {
                    encoreGenre = false;
                }
            }
            return genresAAjouterAuFilm;
        }


        /// <summary>
        /// enregistre les acteurs du film qui est entrain d'etre créé si les acteurs n'existe pas on les créés et les enregistres
        /// autant de fois qu'on veut rajouter un acteur
        /// </summary>
        /// <returns>liste de acteurs</returns>
        private List<Acteur> gereActeurs()
        {
            bool encoreActeur = true;
            int cpt = 0;
            List<Acteur> listeDActeursAAjouterAuFilm = new List<Acteur>();
            List<Acteur> acteurs = manager.Acteurs.ToList();
            while (encoreActeur)
            {
                if (cpt == 0)
                {
                    Console.WriteLine("Quelle sont le(s) acteur(s) de ce film ?\n");
                }
                else
                {
                    Console.WriteLine("Quelle sont le(s) autres acteur(s) de ce film ?\n");
                }

                List<Acteur> tmp = acteurs.Intersect(listeDActeursAAjouterAuFilm).ToList();
                foreach (Acteur a in tmp)
                {
                    acteurs.Remove(a);
                }

                int i = 1;
                foreach (Acteur a in acteurs)
                {
                    Console.WriteLine(i + ". " + a.ToString());
                    i++;
                }
                Console.WriteLine(" * : Ajouter un nouvel acteur ");
                Console.WriteLine("\n\n Saisissez un caractère : ");

                string choixActeurs = Console.ReadLine();

                if (choixActeurs.Equals("*"))
                {
                    Acteur ac = creerNouvelActeur();
                    listeDActeursAAjouterAuFilm.Add(ac);
                }
                else
                {
                    int num = Convert.ToInt16(choixActeurs);
                    listeDActeursAAjouterAuFilm.Add(acteurs[num - 1]);
                }
                cpt++;
                Console.WriteLine("Est-ce qu'il y a d'autres acteurs pour ce film (y/n) ?\n\n");
                string reponse = Console.ReadLine();
                if (reponse.Equals("n") || reponse.Equals("no") || reponse.Equals("non"))
                {
                    encoreActeur = false;
                }
            }
            return listeDActeursAAjouterAuFilm;
        }
        /// <summary>
        /// enregistre les réalisateurs du film qui est entrain d'etre créé si les réalisateurs n'existe pas on les créés et les enregistres
        /// autant de fois qu'on veut rajouter un réalisateur
        /// </summary>
        /// <returns>liste de réalisateurs</returns>
        private List<Realisateur> geresRealisateurs()
        {
            bool encoreRealisateur = true;
            int cpt = 0;
            List<Realisateur> listeDeRealisateursAAjouterAuFilm = new List<Realisateur>();
            List<Realisateur> realisateurs = manager.Realisateurs.ToList();
            while (encoreRealisateur)
            {
                if (cpt == 0)
                {
                    Console.WriteLine("Quelle sont le(s) realisateurs(s) de ce film ?\n");
                }
                else
                {
                    Console.WriteLine("Quelle sont le(s) autres realisateurs(s) de ce film ?\n");
                }

                List<Realisateur> tmp = realisateurs.Intersect(listeDeRealisateursAAjouterAuFilm).ToList();
                foreach (Realisateur r in tmp)
                {
                    realisateurs.Remove(r);
                }

                int i = 1;
                foreach (Realisateur r in realisateurs)
                {
                    Console.WriteLine(i + ". " + r.ToString());
                    i++;
                }
                Console.WriteLine(" * : Ajouter un nouveau réalisateur ");
                Console.WriteLine("\n\n Saisissez un caractère : ");

                string choixRealisateur = Console.ReadLine();

                if (choixRealisateur.Equals("*"))
                {
                    Realisateur ac = creerNouveauRealisateur();
                    listeDeRealisateursAAjouterAuFilm.Add(ac);
                }
                else
                {
                    int num = Convert.ToInt16(choixRealisateur);
                    listeDeRealisateursAAjouterAuFilm.Add(realisateurs[num - 1]);
                }
                cpt++;
                Console.WriteLine("Est-ce qu'il y a d'autres realisateurs pour ce film (y/n) ?\n\n");
                string reponse = Console.ReadLine();
                if (reponse.Equals("n") || reponse.Equals("no") || reponse.Equals("non"))
                {
                    encoreRealisateur = false;
                }
            }
            return listeDeRealisateursAAjouterAuFilm;
        }
        /// <summary>
        /// 
        /// permet de créer et d'enregistrer un nouveau réalisateur
        /// </summary>
        /// <returns>un réalisateur</returns>
        private Realisateur creerNouveauRealisateur()
        {
            Console.WriteLine("********************** NOUVEAU REALISATEUR ***************************\n");


            Console.WriteLine("Saisissez le nom du réalisateur : ");
            string nom = Console.ReadLine();

            Console.WriteLine("Saisissez le prénom du réalisateur : ");
            string prenom = Console.ReadLine();

            //Console.WriteLine("Saisissez le chemin de l'image du réalisateur : ");
            //string path = Console.ReadLine();

            Realisateur realisateurTmp = new Realisateur(nom, prenom);

            List<Realisateur> realisateurs = manager.Realisateurs.ToList();

            foreach (Realisateur r in realisateurs)
            {
                if (r.Equals(realisateurTmp))
                {
                    Console.WriteLine("Ce réalisateur existe déjà");
                    return r;
                }
            }
            manager.ajoutePersonne(realisateurTmp);

            return realisateurTmp;

        }


        /// <summary>
        /// permet de créer et d'enregistrer un nouvel acteur
        /// </summary>
        /// <returns>un acteur</returns>
        private Acteur creerNouvelActeur()
        {

            Console.WriteLine("********************** NOUVEL ACTEUR ***************************\n");

            Console.WriteLine("Saisissez le nom de l'acteur : ");
            string nom = Console.ReadLine();

            Console.WriteLine("Saisissez le prénom de l'acteur : ");
            string prenom = Console.ReadLine();

            //Console.WriteLine("Saisissez le chemin de l'image de l'acteur : ");
            //string path = Console.ReadLine();

            Acteur acteurTmp = new Acteur(nom, prenom);

            List<Acteur> acteurs = manager.Acteurs.ToList();

            foreach (Acteur a in acteurs)
            {
                if (a.Equals(acteurTmp))
                {
                    Console.WriteLine("Cet acteur existe déjà");
                    return a;
                }
            }

            manager.ajoutePersonne(acteurTmp);

            return acteurTmp;

        }
        /// <summary>
        /// change les parametres de l'application
        /// </summary>
        private void ChangerParametre()
        {

            bool valide = false;
            while (!valide)
            {
                Console.WriteLine("Saisissez le nouveau nombre de top à afficher : \n");
                string nb = Console.ReadLine();

                int nombre = 0;
                try
                {
                    nombre = Int16.Parse(nb);
                    valide = true;
                }
                catch
                {
                    valide = false;
                }
            }
            AfficheMenuDebutApplication();
        }
        /// <summary>
        /// regarde si la note passé en parametre est un nombre et si elle est inférieur à 10
        /// </summary>
        /// <param name="note">string note</param>
        /// <returns>boolean valide</returns>
        private bool regardeSiNoteSaisieValide(string note)
        {
            int n;
            try
            {
                n = int.Parse(note);
            }
            catch
            {
                return false;
            }
            if (n <= 10)
            {
                return true;
            }
            else return false;
        }

    }

}
