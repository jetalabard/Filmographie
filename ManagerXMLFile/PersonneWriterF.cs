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
using System.Xml.Linq;
using Metier;
using Metier.Parser;
using System.Linq;

namespace ManagerXMLFile
{
    /// <summary>
    /// écrit les personnes pour une application bureau
    /// </summary>
    public class PersonneWriterF :PersonneWriter
    {
        /// <summary>
        /// met à jour le fichier des genres en ajoutant un genre
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="xml_file"></param>
        public override void saveData(Personne pers, string xml_file)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
            if (mXDoc.Descendants(XMLTags.PERSONNE).Count() == 0)
            {
                mXDoc.Element(XMLTags.PERSONNES)
                 .Add(WritePersonne(pers));
            }
            else
            {
                mXDoc.Descendants(XMLTags.PERSONNE)
                 .First()
                 .Add(WritePersonne(pers));
            }
        }
        /// <summary>
        /// met à jour la liste de films d'une personne - s'applique pour les favoris
        /// </summary>
        /// <param name="pers"></param>
        /// <param name="favoris"></param>
        /// <param name="xml_file"></param>
        public override void UpdateListeFilmsPersonne(Personne pers, Film favoris, string xml_file)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e);
            }
            mXDoc.Descendants(XMLTags.PERSONNE)
                 .Where(p => p.Attribute(XMLTags.NOM).Value.Equals(pers.Nom))
                 .Descendants(XMLTags.FILMS).First()
                 .Add(new XElement(XMLTags.FILM, favoris.Titre));
        }

    }
}
