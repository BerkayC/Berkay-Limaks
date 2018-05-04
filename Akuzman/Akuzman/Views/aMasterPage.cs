using System;
using Akuzman.Pages;
using Xamarin.Forms;
using Akuzman.Logic;

namespace Akuzman.Views
{
    public class aMasterPage : MasterDetailPage
    {
        public HomePage hPage{ get; set; }
        public aMenu menu { get; set; }
        public aDetailPage mDetail{ get; set; }
        public PressListPage mPressList { get; set; }


        public aMasterPage()
        {
            
            hPage = new HomePage();
            menu = new aMenu();
            mDetail = new aDetailPage();
           // mPressList = new PressList();

			

            hPage.maMasterPage = this;
            menu.maMasterPage = this;
            mDetail.maMasterPage = this;
           // mPressList.maMasterPage = this;



            NavigationPage nA = new NavigationPage(hPage);

            Detail = nA;
            Master = menu;




        }
    }
}
