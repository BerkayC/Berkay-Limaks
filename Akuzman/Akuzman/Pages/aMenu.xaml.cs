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
            maMasterPage.Detail = new NavigationPage(GetPage(sItem)); 
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
        public Page GetPage(aMenuItem mItem)
        {
            Page mPage ;
            switch (mItem.Id) 
            {
				case 0:
                    mPage = maMasterPage.hPage;
                    break;
                case 1:
                    mPage = maMasterPage.mDetail;
                    break;
                case 2:
                    
                    mPage = new MyPage();
                    break;
                case 3:
                    PressList pressPage = new PressList();
                    pressPage.PopulatePressItemAsync();
                    mPage = pressPage;
                    break;
                case 4:
                    mPage = maMasterPage.hPage;
                    break;
                case 5:
                    mPage = maMasterPage.mDetail;;
                    break;
                case 6:
                    mPage = maMasterPage.hPage;
                    break;
                case 7:
                    mPage = maMasterPage.mDetail;
                    break;
                default :
                    mPage = maMasterPage.hPage;
                    break;
                
            } 


            return mPage;
        }
    }
}
