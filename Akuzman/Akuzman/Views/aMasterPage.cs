using System;
using Akuzman.Pages;
using Xamarin.Forms;

namespace Akuzman.Views
{
    public class aMasterPage : MasterDetailPage
    {
        public aMasterPage()
        {
            
            HomePage hPage = new HomePage();
            aMenu menu = new aMenu();
            hPage.maMasterPage = this;
            menu.maMasterPage = this;
            Detail = hPage;
            Master = menu;
        }
    }
}
