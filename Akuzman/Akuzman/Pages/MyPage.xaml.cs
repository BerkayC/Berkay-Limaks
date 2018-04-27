using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
			InitializeComponent();
           // var uri = new Uri("http://192.168.1.108:8080/akuzman/api/mobile1/main.php?router=xml_press_detail&id=2");
            web = new WebView();
            web.Source = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_gallery_categories";
           // web.Source = "http://www.google.com";
            Content = web;
        }
    }
}
