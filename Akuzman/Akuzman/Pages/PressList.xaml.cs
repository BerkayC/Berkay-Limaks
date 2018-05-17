using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akuzman.Logic;
using Akuzman.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public class PressItem 
    {
        public string name { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public string web_url { get; set; }

        public PressItem()
        {
            
        }
    }

    public partial class PressList : ContentPage
    {
        public aMasterPage maMasterPage;
        public List<PressItem> pressItems;
        public DataRetriever PressListRetriever;
        public string PressData;
        public static string UrlOfData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_press_list";

		public void Handle_Activated(object sender, System.EventArgs e)
        {
            maMasterPage.IsPresented = true;
        }
        public PressList()
        {
            InitializeComponent();
            pressItems = new List<PressItem>();

            PressListRetriever = new DataRetriever(UrlOfData);

            //ndicator = new ActivityIndicator();
            //ndicator.IsRunning = true;
            //Content = ndicator;
           
          
        }

        public async Task PopulatePressItemAsync()
        {
            await GetRawJson();

            InsertItems();

            GenerateView();
        }
        private async Task GetRawJson()
        {
            await PressListRetriever.ReadByUrl();
            PressData = PressListRetriever.RawJson;
        }
        private void InsertItems()
        {
            pressItems = JsonConvert.DeserializeObject<List<PressItem>>(PressData);
        }

        private void GenerateView()
        {
            var innerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { } };
            var scroller = new ScrollView { Orientation = ScrollOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Content = innerStack };

            var outerStack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Children = { scroller } };

            //Clickable Label
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) => OnLabelClicked(s, e);
            //Clickable Label

            foreach (PressItem pItem in pressItems)
            {
                Label mlabel = new Label { Text = pItem.name, TextColor = Color.FromHex("#00558f"), HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = 15, };
                Image mImage = new Image { Source = pItem.image };
                //Clickable Label
                mlabel.GestureRecognizers.Add(tgr);
                //Clickable Label

                if (mlabel.Text != null)
                {
                    innerStack.Children.Add(mlabel);
                    innerStack.Children.Add(mImage);
                }

            }
            Content = outerStack;
        }

        private void OnLabelClicked(object sender, EventArgs eventArgs)
        {
            Label label = (Label)sender;
            DisplayAlert("hey" + "  " + label.ClassId, "you did it", "get lost");

        }
    }
}
    //"name": "Diva – 26 Aralık 2014",
    //"title": "Basında Biz",
    //"content": "Diva – 26 Aralık 2014",
    //"image": "http://limaxbilisim.com/demo/akuzman/admin/upload/akuzman_25yil_23x30cm_1524657869.jpg",
    //"web_url": "http://limaxbilisim.com/demo/akuzman/basinda_biz_bbv-1.html"