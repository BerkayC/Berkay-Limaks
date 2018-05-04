using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akuzman.Logic;
using Akuzman.Views;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace Akuzman.Pages
{
    public partial class GalleryCategoryPage : ContentPage
    {
        public class GalleryCategory
        {
            public string id{ get ; set ;}
            public string name { get; set; }
        }
        public aMasterPage maMasterPage;
        public DataRetriever GalleryCatRetriever;
        public List<GalleryCategory> GalleryCategories { get; set; }
        public aMenu mMenu;


        public string CategoryData;
        public static string UrlForData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_gallery_categories";
        public GalleryCategoryPage()
        {
            GalleryCatRetriever = new DataRetriever(UrlForData);
            GalleryCategories = new List<GalleryCategory>();
            InitializeComponent();
        }
        private async Task GetRawJson()
        {
            await GalleryCatRetriever.ReadByUrl(UrlForData);
            CategoryData = GalleryCatRetriever.RawJson;
        }
        private void InsertItems()
        {
            GalleryCategories = JsonConvert.DeserializeObject<List<GalleryCategory>>(CategoryData);
        }
        public async Task PopulateCatAsync()
        {
            await GetRawJson();
           
            InsertItems();

            GenerateView();

        }
        private void GenerateView()
        {
            var innerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { } };
            var scroller = new ScrollView { Orientation = ScrollOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Content = innerStack };

            var outerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { scroller } };

            //Clickable Label
			var tgr = new TapGestureRecognizer();
			tgr.Tapped += (s, e) => OnLabelClicked(s,e);
            //Clickable Label

            foreach (GalleryCategory catItem in GalleryCategories)
            {
                Label mlabel = new Label { Text = catItem.name, ClassId = catItem.id, TextColor = Color.FromHex("#00558f"), HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = 15 ,  };
                //Clickable Label
                mlabel.GestureRecognizers.Add(tgr);
                //Clickable Label

                if (mlabel.Text != null)
                {
                    innerStack.Children.Add(mlabel);
                }

            }
            Content = outerStack;
        }

        private async Task OnLabelClicked(object sender, EventArgs eventArgs)
        {
            Label label = (Label)sender;
           

            GalleryPage galleriesPage = new GalleryPage();
            await galleriesPage.PopulateGalleriesAsync();
            galleriesPage.categoryPage = this;
            galleriesPage.SubCategory(label.ClassId);
            //initiate inner gallery
        }
    }
}
