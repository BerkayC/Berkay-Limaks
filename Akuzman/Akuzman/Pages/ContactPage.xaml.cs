
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Net.Http;
using System.Threading.Tasks;


namespace Akuzman.Pages
{
    public partial class ContactPage : ContentPage
    {
        public StackLayout ContactPageLayout;
        public EntryCell name ;
        public EntryCell email ;
        public EntryCell telephone ;
        public EntryCell mTopic ;
        public EntryCell message ;
       
        public class ContactItem
        {
            public string name { get; set; }
            public string email { get; set; }
            public string number { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
        }
      
        public class MapItem
        {
            public string Adress { get; set; }
            //google maps api webview 
        }

        public ContactPage()
        {
            ContactPageLayout = new StackLayout();
            InitializeComponent();
            SetPage();
            Content = ContactPageLayout;
        }



        public void SetPage()
        {
            name = new EntryCell
            {
                Label = "İsim&Soyisim:",
                Placeholder = "İsim Soyisim",
                ClassId = "0" ,
                //Keyboard = Keyboard.Default
            };
           email = new EntryCell
            {
                Label = "E-Mail:",
                Placeholder = "örn123@gmail.com",
                ClassId = "1" ,
               // Keyboard = Keyboard.Email
            };
            telephone = new EntryCell
            {
                Label = "Telefon Numarası:",
                Placeholder = "+9XXXXXXXXX",
                ClassId = "2" ,
                Keyboard = Keyboard.Numeric
            };
             mTopic = new EntryCell
            {
                Label = "Konu:",
                Placeholder = "Konuyu Özetleyiniz.",
                ClassId = "3" ,
                Keyboard = Keyboard.Default
            };
            message = new EntryCell
            {
                Label = "Mesaj:",
                Placeholder = "Mesajınızı Yazınız.",
                ClassId = "4" , 
                Keyboard = Keyboard.Default
            };
            Label topic = new Label { Text = "İletişim" , FontSize=30,TextColor=Color.Blue};
            Label detail = new Label { Text="Size özel teklifimizi almak ve referanslarımızı görmek için aşağıdaki butonları kullanarak bizimle iletişime geçebilirsiniz." , FontSize = 15,TextColor= Color.Black};
            Button SendButton = new Button { Text="Gönder",FontSize=20,TextColor=Color.Black, Image="favicon1.png" ,   };
            SendButton.Clicked += Handle_Clicked;
            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        name,email,telephone,mTopic,message
                    }
                }
            };
                    View[] views = {topic,detail,tableView, SendButton};
                    foreach (View item in views)
                    {
                        ContactPageLayout.Children.Add(item);
                    }  
          }

        async void Handle_Clicked(object sender, System.EventArgs e)
		{
		//	/ Users / hasancelebi / Desktop / Akuzman kopyası / Akuzman / CSC: Error CS1703: Multiple assemblies with equivalent identity have been imported: '/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/System.dll' and '/Library/Frameworks/Mono.framework/Versions/5.10.1/lib/mono/xbuild-frameworks/.NETPortable/v4.5/Profile/Profile111/System.dll'.Remove one of the duplicate references. (CS1703)(Akuzman)
            ContactItem contactItem = new ContactItem();
            contactItem.name = name.Text;
            contactItem.email = email.Text;
            contactItem.number = telephone.Text;
            contactItem.subject = mTopic.Text;
            contactItem.message = message.Text;
            string json = JsonConvert.SerializeObject(contactItem, Formatting.Indented);
			//Push json to server
			await PostContact(json);
        }  

		public async Task PostContact (string json)
		{    
			var uri = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_contact_1";

            HttpClient myClient = new HttpClient();
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await myClient.PostAsync(uri,content);

			response.EnsureSuccessStatusCode();

            string content1 = await response.Content.ReadAsStringAsync();

			if (content1 != "OK")
			{

				bool sendAgain = await DisplayAlert("Hata oluştu", "Gönderiniz başarısız oldu", "Tekrar Dene", "Vazgeç");
				if (sendAgain)

					await PostContact(json);
				else
					return;
			}
			else if (content1 == "OK")
				await DisplayAlert("Mesaj Gönderme Başarılı","Mesajınız bize ulaşmıştır. İlginiz için teşekkür ederiz.","Tamam");



		}
    }       
}