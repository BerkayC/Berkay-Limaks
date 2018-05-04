using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using Foundation;
using UIKit;
using Xamd.ImageCarousel.Forms.Plugin.iOS;

namespace Akuzman.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            UIApplication.Main(args, null, "AppDelegate");
            global::Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();

        

        }
    }
}
