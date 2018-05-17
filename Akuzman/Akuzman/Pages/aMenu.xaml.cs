using System;
using System.Collections.Generic;
using Akuzman.Logic;
using Akuzman.Views;
using Rg.Plugins.Popup.Extensions;
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
            string[] menuItemNames = new string[] { "AnaSayfa", "Tanıtım", "Basında Biz","Galeri","Blog","İletişim"};
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
            maMasterPage.Detail = new NavigationPage(GetPage(sItem));
            maMasterPage.IsPresented = false;

        }
        public void PopUp (Page page)
        {
            Navigation.PushPopupAsync((Rg.Plugins.Popup.Pages.PopupPage)page);
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
        public Page GetPage(aMenuItem mItem)
        {
            Page mPage ;
            switch (mItem.Id) 
            {
				case 0:
					HomePage homePage = new HomePage();
					homePage.mMenu = this;
					mPage = homePage;

                    break;
                case 1:
					AboutPage aboutPage = new AboutPage();
					aboutPage.mMenu = this;
					mPage = aboutPage;
                    break;
                case 2:
                    PressList pressPage = new PressList();
                    pressPage.PopulatePressItemAsync();
                    mPage = pressPage;
                    break;
                case 3:
                    GalleryCategoryPage categoryPage = new GalleryCategoryPage();
                    categoryPage.PopulateCatAsync();
                    mPage = categoryPage;
                    break;
                case 4:
					BlogPage blogPage = new BlogPage();
					blogPage.mMenu = this;
					mPage = blogPage;
                    
                    break;
                case 5:
					mPage = new ContactPage();
                    break;
                default :
                    mPage = maMasterPage.hPage;
                    break;
                
            } 


            return mPage;
        }
    }
}
