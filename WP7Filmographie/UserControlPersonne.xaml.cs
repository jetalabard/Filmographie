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

namespace WP7Filmographie
{
    public partial class UserControlPersonne : UserControl
    {
        public UserControlPersonne()
        {
            InitializeComponent();
        }

        public string Nom
        {
            get { return (string)GetValue(NomProperty); }
            set { SetValue(NomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Nom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomProperty =
            DependencyProperty.Register("Nom", typeof(string), typeof(UserControlPersonne), new PropertyMetadata("nom"));


        public string Prénom
        {
            get { return (string)GetValue(PrénomProperty); }
            set { SetValue(PrénomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Prénom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrénomProperty =
            DependencyProperty.Register("Prénom", typeof(string), typeof(UserControlPersonne), new PropertyMetadata("prénom"));


        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(UserControlPersonne), new PropertyMetadata("default.jpg"));

    }
}
