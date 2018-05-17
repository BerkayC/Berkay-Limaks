using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akuzman.Logic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class GalleryPage : ContentPage
    {
        public GalleryPage()
        {
            galleryRetriever = new DataRetriever(UrlOfData);
            matchingItems = new List<GalleryItem>();
            //InitializeComponent();
        }

        public DataRetriever galleryRetriever;
        public static string UrlOfData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_galleries";
        public List<GalleryItem> GalleryItems { get; set; }
        public string GalleryData;
        public GalleryCategoryPage categoryPage;
        public aMenu mMenu;
        public List<GalleryItem> matchingItems;

        public class GalleryItem
        {
            public string id { get; set; }
            public string category_id { get; set; }
            public string name { get; set; }
            public string img { get; set; }
            public List<GalleryImage> images { get; set; }

        }
        public class GalleryImage
        {
            public string image { get; set; }
            public string title { get; set; }
            public string content { get; set; }
        }

        private async Task GetRawJson()
        {
            await galleryRetriever.ReadByUrl();
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
        }
        private void GenerateView(List<GalleryItem> matchingItems) //Sub Category
        {
            var innerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { } };
            var scroller = new ScrollView { Orientation = ScrollOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Content = innerStack };

            var outerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { scroller } };

            //Clickable Label
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) => OnLabelClicked(s, e);
            //Clickable Label

            foreach (GalleryItem gItem in matchingItems)
            {
                // Label mlabel = new Label { Text = gItem.name, ClassId = gItem.id, TextColor = Color.FromHex("#00558f"), HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = 15, };
                Image mImage = new Image { Source = gItem.img, ClassId = gItem.id };
                //Clickable Label
                mImage.GestureRecognizers.Add(tgr);
                //Clickable Label

                if (mImage.Source != null)
                {
                    innerStack.Children.Add(mImage);
                }

            }
            int c = innerStack.Children.Count;
            categoryPage.Content = outerStack;
        }
        private void OnLabelClicked(object s, EventArgs e)
        {
            Image mImage = (Image)s;
            string id = mImage.ClassId;
            GalleryItem mgItem = matchingItems.Find(x => x.id == id);
            PublishImages(mgItem.images);

        }

        private void PublishImages(List<GalleryImage> galleryImages) //Images
        {
            var innerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { } };
            var scroller = new ScrollView { Orientation = ScrollOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Content = innerStack };

            var outerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { scroller } };


            foreach (GalleryImage gItem in galleryImages)
            {
                Label mlabel = new Label { Text = gItem.title, TextColor = Color.FromHex("#00558f"), HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = 15, };
                Image mImage = new Image { Source = gItem.image };
                if (mlabel.Text != null)
                {
                    innerStack.Children.Add(mlabel);
                    innerStack.Children.Add(mImage);
                }

            }
            int c = innerStack.Children.Count;
            categoryPage.Content = outerStack;
        }


        public void SubCategory(string Cat_id)
        {
            matchingItems = GalleryItems.FindAll(x => x.category_id == Cat_id);
            GenerateView(matchingItems);

            // mMenu.maMasterPage.Detail = new NavigationPage(this);
        }
    }
}


