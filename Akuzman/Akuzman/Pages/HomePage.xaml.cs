using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akuzman.Views;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class HomePage : ContentPage
    {
        public class SliderItem
        {
            public string ImageUrl { get; set; }
            public string Name { get; set; }
        }
        public aMasterPage maMasterPage;
        public aMenu mMenu;
        public List<SliderItem> sliderItems;

        public HomePage()
        {
            InitializeComponent();
			populateSlider();
            //caro();
        }
        public void populateSlider()
        {
            
            sliderItems = new List<SliderItem>
         {
                new SliderItem
            {
                ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/23c1dd13-333a-459e-9e23-c3784e7cb434/2016-06-02_1049.png",
                Name = "Woodland Park Zoo"
            },
                new SliderItem
            {
                ImageUrl =    "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png",
                Name = "Cleveland Zoo"
                },
                new SliderItem
            {
                ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/e8179889-8189-4acb-bac5-812611199a03/2016-06-02_1053.png",
                Name = "Phoenix Zoo"
            }
         };
          
           
  
        }
        public void populateSlider(string [] ImageUrls)
        {
            var myCarousel = new CarouselViewControl();
            myCarousel.ItemsSource = new ObservableCollection<int> { 1, 2, 3, 4, 5 }; // ADD/REMOVE PAGES FROM CAROUSEL ADDING/REMOVING ELEMENTS FROM THE COLLECTION
            //myCarousel.ItemTemplate = new DataTemplate (typeof(HomePage));
            myCarousel.Position = 0; //default
            myCarousel.InterPageSpacing = 10;
            myCarousel.Orientation = CarouselViewOrientation.Horizontal;
            Content = myCarousel;
        }

    }
}