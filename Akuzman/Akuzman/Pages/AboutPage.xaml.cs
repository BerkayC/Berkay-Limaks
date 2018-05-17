using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akuzman.Logic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Akuzman.Pages
{
    public partial class AboutPage : ContentPage
    {
		public class AboutItem
		{
			public string id { get; set; }
			public string name { get; set; }
			public string title { get; set; }
			public string content { get; set; }
			public string image { get; set; }
		}
		public DataRetriever aboutDataRetriever;
		public string UrlForData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_about";
		public string aboutData;
		public List<AboutItem> aboutItems;
		public StackLayout aboutCategoriesStack;
		public aMenu mMenu;
        public AboutPage()
        {
			aboutItems = new List<AboutItem>();
			aboutCategoriesStack = new StackLayout{VerticalOptions=LayoutOptions.Center};
            InitializeComponent();
			getAboutItemsAsync();

        }
        
		public async Task getAboutItemsAsync()
		{
			aboutDataRetriever = new DataRetriever(UrlForData);
			await aboutDataRetriever.ReadByUrl();
			aboutData = aboutDataRetriever.RawJson;
			aboutItems = JsonConvert.DeserializeObject<List<AboutItem>>(aboutData);

			foreach (AboutItem aitem in aboutItems)
			{
				//Clickable Label
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => OnAboutLabelClicked(s, e);
                //Clickable Label
				Label mLabel = new Label { Text=aitem.title,TextColor=Color.Blue, ClassId=aitem.id , HorizontalTextAlignment= TextAlignment.Center, HorizontalOptions= LayoutOptions.CenterAndExpand,VerticalOptions=LayoutOptions.Center,FontSize=20 };
				//Clickable Label
				mLabel.GestureRecognizers.Add(tgr);
                //Clickable Label
				aboutCategoriesStack.Children.Add(mLabel);
			}
			Content = aboutCategoriesStack;
		}

		private void OnAboutLabelClicked(object s, EventArgs e)
		{
			Label label = (Label)s;
			AboutItem itemToPop = aboutItems.Find(X=>X.id == label.ClassId);
			MPopUpPage mPopUpPage = new MPopUpPage(itemToPop);
            mMenu.PopUp(mPopUpPage);
		}
	}
}
//    "id": "6",
//    "name": "Misyonumuz",
//    "title": "Misyonumuz",
//    "content": "Müşterinin bugünkü beklentileri ve gereksinimleri tam ve zamanında karşılayıp, onlara gelecekte beklentilerini aşan ürün ve hizmetler sunmaktır.\r\n\r\nMüşterilerimize güvenilir ve rekabetçi işleme hizmeti ,hızlı cevap veren bir işletme olmaktır.\r\n\r\nSürekli gelişim felsefesini ilke edinmek, bu yolla hizmet ve ürünlerin kalitesini sürekli olarak arttırmaktır.. \r\n\r\n\r\n\r\n\r\n",
//    "image": "http://limaxbilisim.com/demo/akuzman/admin/upload/logo_1524656998.png"