﻿#pragma checksum "U:\Windows Phone\Filmographie\Filmographie\WP7Filmographie\AjoutPersonne.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "09B82D8DEE25804CE6C9BEC0B1CB1433"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18063
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WP7Filmographie {
    
    
    public partial class AjoutPersonne : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Image mImage;
        
        internal System.Windows.Controls.TextBox TextBoxNom;
        
        internal System.Windows.Controls.TextBox TextBoxPrenom;
        
        internal System.Windows.Controls.CheckBox checkBoxActeur;
        
        internal System.Windows.Controls.CheckBox checkBoxRealisateur;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP7Filmographie;component/AjoutPersonne.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.mImage = ((System.Windows.Controls.Image)(this.FindName("mImage")));
            this.TextBoxNom = ((System.Windows.Controls.TextBox)(this.FindName("TextBoxNom")));
            this.TextBoxPrenom = ((System.Windows.Controls.TextBox)(this.FindName("TextBoxPrenom")));
            this.checkBoxActeur = ((System.Windows.Controls.CheckBox)(this.FindName("checkBoxActeur")));
            this.checkBoxRealisateur = ((System.Windows.Controls.CheckBox)(this.FindName("checkBoxRealisateur")));
        }
    }
}
