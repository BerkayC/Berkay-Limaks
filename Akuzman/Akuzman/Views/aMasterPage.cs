using System;
using Akuzman.Pages;
using Xamarin.Forms;

namespace Akuzman.Views
{
    public class aMasterPage : MasterDetailPage
    {
        public aMasterPage()
        {
            Master = new aMenu();
            Detail = new HomePage();
        }
    }
}
