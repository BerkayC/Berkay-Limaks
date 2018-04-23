using System;
using System.Collections.Generic;
using Akuzman.Views;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class aDetailPage : ContentPage
    {
        public aMasterPage maMasterPage;
        public aDetailPage()
        {
            InitializeComponent();
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
