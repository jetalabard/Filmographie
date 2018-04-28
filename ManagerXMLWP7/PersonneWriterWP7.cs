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
using Metier.Parser;
using System.Linq;
using System.Xml.Linq;

namespace ManagerXMLWP7
{
    /// <summary>
    /// écrit les personnes pour une application windows phone - écrit dans la chambre d'isolation
    /// </summary>
    public class PersonneWriterWP7 :PersonneWriter
    {
        /// <summary>
        /// met à jour le fichier des personnes en ajoutant un personne
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="xml_file"></param>
        public override void saveData(Personne pers, string xml_file)
        {
            if (mXDoc.Descendants(XMLTags.PERSONNE).Count() == 0)
            {
                mXDoc.Element(XMLTags.PERSONNES)
                 .Add(WritePersonne(pers));
            }
            else
            {
                mXDoc.Descendants(XMLTags.PERSONNES)
                 .First()
                 .Add(WritePersonne(pers));
            }
        }
        /// <summary>
        /// constructeur qui initialise le XDocument
        /// </summary>
        /// <param name="mxdoc"></param>
        public PersonneWriterWP7(XDocument mxdoc)
        {
            mXDoc = mxdoc;
        }
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public PersonneWriterWP7()
        {
        }
        /// <summary>
        /// met à jour la liste de films d'une personnes
        /// </summary>
        /// <param name="pers"></param>
        /// <param name="favoris"></param>
        /// <param name="xml_file"></param>
        public override void UpdateListeFilmsPersonne(Personne pers,Film favoris,string xml_file)
        {
            mXDoc.Descendants(XMLTags.PERSONNE)
                 .Where(p=> p.Attribute(XMLTags.NOM).Value.Equals(pers.Nom))
                 .Descendants(XMLTags.FILMS).First()
                 .Add(new XElement(XMLTags.FILM, favoris.Titre));
        }
    }
}
