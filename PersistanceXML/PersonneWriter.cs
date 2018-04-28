using Metier.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Metier
{
    public abstract class PersonneWriter
    {
        /// <summary>
        /// document à écrire
        /// </summary>
        public XDocument mXDoc
        {
            get;
            set;
        }


       
        public abstract void saveData(Personne pers, string xmlFile);


        /// <summary>
        /// écrit une personne 
        /// </summary>
        /// <param name="pers">une personne</param>
        /// <returns>une représentation xml d'un personne</returns>
        public XElement WritePersonne(Personne pers)
        {
            string type="";
            if(pers is Realisateur)
            {
                type = "Réalisateur";
            }
            else if(pers is Acteur)
            {
                type = "Acteur";
            }else{
                type = "Normal";
            }

            XElement filmElt =
               new XElement(XMLTags.PERSONNE, new XAttribute(XMLTags.NOM, pers.Nom),
                                                    new XAttribute(XMLTags.PRENOM, pers.Prenom),
                                                    new XAttribute(XMLTags.TYPE, type),
                   new XElement(XMLTags.IMAGE, pers.Image),
                    new XElement(XMLTags.FILMS, writeListeFilms(pers)));

            return filmElt;
        }
        /// <summary>
        /// sauvegarde le document
        /// </summary>
        /// <param name="xmlFile"></param>
        public void Save(string xmlFile)
        {
            mXDoc.Save(new StringWriter(new StringBuilder(xmlFile)));
        }
        /// <summary>
        /// ecrit tous les personnes passé en parametre
        /// </summary>
        /// <param name="personnes">liste de personnes</param>
        public void WritePersonnes(List<Personne> personnes)
        {
            mXDoc = new XDocument(
                new XElement(XMLTags.PERSONNES, personnes.Select(pers => WritePersonne(pers))));
        }
        /// <summary>
        /// écrit les films d'une personne
        /// </summary>
        /// <param name="pers"></param>
        /// <returns></returns>
        public IEnumerable<XElement> writeListeFilms(Personne pers)
        {
            if (pers.listeFilms == null)
            {
                return null;
            }
            return pers.listeFilms.Select(film => new XElement(XMLTags.FILM, film.Titre));
        }

        /// <summary>
        /// écrit une personne dans le document
        /// </summary>
        /// <param name="personne">une personne</param>
        /// <param name="xml_file">le chemin du fichier</param>
        public void WriteUnePersonne(Personne personne,string xml_file)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
            XElement element = WritePersonne(personne);
            mXDoc.Element(XMLTags.PERSONNES).Add(element);
        }


        /// <summary>
        /// met à jour la liste de personnes, en ajoutant un film à une personne
        /// </summary>
        /// <param name="pers">une personne</param>
        /// <param name="favoris">le film a ajouter</param>
        /// <param name="xml_file">le chemin du fichier</param>
        public abstract void UpdateListeFilmsPersonne(Personne pers,Film favoris,string xml_file);
        
    }
}
