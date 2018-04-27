using System;
using System.Collections.Generic;
using Akuzman.Views;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class HomePage : ContentPage
    {
        public aMasterPage maMasterPage;
        public class aItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public HomePage()
        {
            InitializeComponent();
            List<aItem> aItems = new List<aItem>();
            aItems.Add(new aItem { Id = 1, Name = "Hey" });
            lstView1.ItemsSource = aItems;
        }


        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ListView list = (ListView)sender;


            list.SelectedItem = null;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (maMasterPage.IsPresented != true)
            {
				maMasterPage.IsPresented = true;

            }
        }
    }
}