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
        public static string UrlForData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_press_list";
        ActivityIndicator ndicator;
        public PressList()
        {
            InitializeComponent();
            pressItems = new List<PressItem>();

            PressListRetriever = new DataRetriever(UrlForData);

            ndicator = new ActivityIndicator();
            ndicator.IsRunning = true;

            //Content = ndicator;


            // pressItems = JsonConvert.DeserializeObject<List<PressItem>>(PressListRetriever.JsonToBeSend);
        }
        private async Task GetRawJson()
        {
            await PressListRetriever.ReadByUrl(UrlForData);
            PressData = PressListRetriever.RawJson;
        }

        private void InsertItems()
        {
            pressItems = JsonConvert.DeserializeObject<List<PressItem>>(PressData);
        }
        public async Task PopulatePressItemAsync()
        {
            await GetRawJson();           
            ndicator.IsRunning = false;
            InsertItems();
            lstView.ItemsSource = pressItems;
        }
    }
}
    //"name": "Diva – 26 Aralık 2014",
    //"title": "Basında Biz",
    //"content": "Diva – 26 Aralık 2014",
    //"image": "http://limaxbilisim.com/demo/akuzman/admin/upload/akuzman_25yil_23x30cm_1524657869.jpg",
    //"web_url": "http://limaxbilisim.com/demo/akuzman/basinda_biz_bbv-1.html"