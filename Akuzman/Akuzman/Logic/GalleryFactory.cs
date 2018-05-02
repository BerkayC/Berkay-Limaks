using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akuzman.Pages;
using Akuzman.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Akuzman.Logic
{
    public class GalleryFactory
    {
        public DataRetriever galleryRetriever;
        public static string UrlOfData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_galleries";
        public List<GalleryItem> GalleryItems { get; set; }
        public string GalleryData;


        public class GalleryItem
        {
            public string id { get; set; }
            public string category_id { get; set; }
            public string name { get; set; }
            public List<GalleryImage> images { get; set; }

        }
        public class GalleryImage 
        {
            public string image { get; set; }
            public string title { get; set; }
            public string content { get; set; }
        }
        public GalleryFactory()
        {
            galleryRetriever = new DataRetriever(UrlOfData);
        }
        private async Task GetRawJson()
        {
            await galleryRetriever.ReadByUrl(UrlOfData);
            GalleryData = galleryRetriever.RawJson;
        }
        private void InsertItems()
        {
            GalleryItems = JsonConvert.DeserializeObject<List<GalleryItem>>(GalleryData);
        }
        public async Task PopulateGalleriesAsync()
        {
            await GetRawJson();

            InsertItems();

           // GenerateView();
        }
            //private void GenerateView()
            //{
            //    var innerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { } };
            //    var scroller = new ScrollView { Orientation = ScrollOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Content = innerStack };

            //    var outerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { scroller } };

            //    //Clickable Label
            //    var tgr = new TapGestureRecognizer();
            //    tgr.Tapped += (s, e) => OnLabelClicked(s, e);
            //    //Clickable Label

            //    foreach (GalleryCategory catItem in GalleryCategories)
            //    {
            //        Label mlabel = new Label { Text = catItem.name, ClassId = catItem.id, TextColor = Color.FromHex("#00558f"), HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = 15, };
            //        //Clickable Label
            //        mlabel.GestureRecognizers.Add(tgr);
            //        //Clickable Label

            //        if (mlabel.Text != null)
            //        {
            //            innerStack.Children.Add(mlabel);
            //        }

            //    }
            //    Content = outerStack;
            //}

        }
    }


    //"id": "1",
    //"category_id": "1",
    //"name": "Açık Havuzlar",
    //"images": [