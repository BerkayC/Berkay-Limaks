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
            aDetailPage mDetail = new aDetailPage();
            hPage.maMasterPage = this;
            menu.maMasterPage = this;
            mDetail.maMasterPage = this;

            //Detail = hPage;
            // Detail = mDetail;
            NavigationPage nA = new NavigationPage(hPage);
            NavigationPage.SetTitleIcon(nA, "AkuzLogo.png");
            Detail = nA;
            Master = menu;
        }
    }
}
