using System;
using Akuzman.Pages;
using Xamarin.Forms;

namespace Akuzman.Views
{
    public class aMasterPage : MasterDetailPage
    {
        public aMasterPage()
        {
            
            Detail = new HomePage();
            aMenu menu = new aMenu();
            menu.maMasterPage = this;
            Master = menu;
        }
    }
}
