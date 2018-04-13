using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class aMenu : ContentPage
    {
        List<aMenuItem> menuItems = new List<aMenuItem>();
		public class aMenuItem
		{
			public int Id{get;set;}
			public string Text{get;set;}
		}
        public aMenu()
        {
            InitializeComponent();
            Title = "Akuzman";
            menuItems = new List<aMenuItem>();
            string[] menuItemNames = new string[] { "AnaSayfa", "Tanıtım", "Basında Biz","Duyuru & Haberler","Galeri","İnsan Kaynakları","İletişim","Sıkça Sorulan Sorular" };
            setMenuItems(menuItemNames);
            lstView.ItemsSource = menuItems;

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
