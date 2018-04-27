using System;
using System.Collections.Generic;
using Akuzman.Views;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class mBasePage : ContentPage
    {
        public int ID {get;set;}
        public string Name {get;set;}
        public aMasterPage maMasterPage;
        public mBasePage()
        {
            InitializeComponent();
        }
        void Handle_Activated(object sender, System.EventArgs e)
        {
           maMasterPage.IsPresented = true;
        }
    }
}
