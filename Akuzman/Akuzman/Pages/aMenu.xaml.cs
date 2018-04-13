using System;
using System.Collections.Generic;
using Akuzman.Views;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class aMenu : ContentPage
    {
        List<aMenuItem> menuItems = new List<aMenuItem>();
        public aMasterPage maMasterPage;

		public class aMenuItem
		{
			public int Id{get;set;}
			public string Text{get;set;}
		}
        internal aMenu()
        {
           
            InitializeComponent();
            Title = "Akuzman";
            menuItems = new List<aMenuItem>();
            string[] menuItemNames = new string[] { "AnaSayfa", "Tanıtım", "Basında Biz","Duyuru & Haberler","Galeri","İnsan Kaynakları","İletişim","Sıkça Sorulan Sorular" };
            setMenuItems(menuItemNames);
            lstView.ItemsSource = menuItems;

        }
        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) 
				return ;
                
            
            aMenuItem sItem = (aMenuItem)e.SelectedItem;
            ListView list = (ListView)sender;


            list.SelectedItem = null;
            maMasterPage.IsPresented = false;

        }
    
        public void setMenuItems(string[] menuItemNames)
        {
            int i = 0;
            foreach (string item in menuItemNames)
            {
                menuItems.Add(new aMenuItem { Id = i, Text = item });
                i++;
            }
        }
    }
}
