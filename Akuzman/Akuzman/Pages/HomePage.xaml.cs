using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akuzman.Views;
using Xamarin.Forms;
using Akuzman.Logic;
using Newtonsoft.Json;
using CarouselView.FormsPlugin.Abstractions;

namespace Akuzman.Pages
{
    public partial class HomePage : ContentPage
    {
        public aMasterPage maMasterPage;

        public StackLayout OverAllStack;

        public aMenu mMenu;


        //Slider Block
        public class Slider
        {
			public class SliderItem 
			{
				public string id{get;set;}
				public string resim { get; set; }
				public string baslik { get; set; }
				public string aciklama { get; set; }
			}
            public DataRetriever hSliderRetriever;
			public static string UrlOfData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_slider1";
            public string hSliderData;
			public List<SliderItem> sliderItems;
			public StackLayout sliderLayout;
			public int SliderPosition;
			CarouselViewControl mSlider;

			public Slider ()
			{
				hSliderRetriever = new DataRetriever(UrlOfData);
				sliderLayout = new StackLayout();
				sliderItems = new List<SliderItem>();
				SliderPosition = 0;

			}

			public async Task FetchSliderItems()
            {

				await hSliderRetriever.ReadByUrl();
				this.hSliderData = hSliderRetriever.RawJson;
				sliderItems = JsonConvert.DeserializeObject<List<SliderItem>>(hSliderData);
				sliderLayout =(StackLayout) Slide(sliderItems);   


				//// Auto Slide 
			    Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
					mSlider.Position++;
                    if (mSlider.Position == sliderItems.Count-1) 
						mSlider.Position = 0;                        
                    
                   // mSlider.Position = SliderPosition;
                    return true;
                });
                ////

            }
			public Layout Slide(List<SliderItem> sliders)
            {
				mSlider = new CarouselViewControl();
                List<View> children = new List<View> { };
				mSlider.HeightRequest = 200;
				//asdf.ShowArrows = true;
				mSlider.FlowDirection = FlowDirection.LeftToRight;
				mSlider.AnimateTransition = true;
				mSlider.ShowIndicators = true;
				mSlider.IndicatorsTintColor = Color.Blue;
				mSlider.CurrentPageIndicatorTintColor = Color.White;

                foreach (SliderItem item in sliders)
                {
                    Image image = new Image {Source=item.resim , Aspect=Aspect.Fill , ClassId = item.id,HeightRequest=200,HorizontalOptions=LayoutOptions.Fill };
                    children.Add(image);
                }

                
                mSlider.ItemsSource = children;

                StackLayout stack = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                stack.Children.Add(mSlider);

                return stack;
            }
            
        }

        //Info Block
        public class Info
        {
            public class InfoItem
            {
                public string title { get; set; }
                public string content { get; set; }
            }
           
            public DataRetriever hInfoRetriever;
            public static string UrlOfData = "http://192.168.1.108:8080/akuzman/api/mobile1/main.php?router=json_home_data";
            public string hInfoData;
            public List<InfoItem> infoItems { get; set; }
            public StackLayout InfoStackLayout;
            public Info()
            {
                hInfoRetriever = new DataRetriever(UrlOfData);
                infoItems = new List<InfoItem>();
            }
            public async Task FetchInfo () //Fetchle başlayan methodlar overallstack'in içerisine kendi layoutlarını atarlar
            {
                
                await hInfoRetriever.ReadByUrl();
                hInfoData = hInfoRetriever.RawJson;
                InfoItem info = JsonConvert.DeserializeObject<InfoItem>(hInfoData);
                InfoStackLayout = new StackLayout { BackgroundColor = Color.White, Padding = new Thickness(20,0,20,0) };
                Label label = new Label{ Text = info.title, TextColor = Color.FromHex("#00558f"), HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = 30,BackgroundColor=Color.White };
                Label detailLabel = new Label {Text=info.content,HorizontalOptions=LayoutOptions.Start,BackgroundColor=Color.White,TextColor= Color.Black};
                
                InfoStackLayout.Children.Add(label);
                InfoStackLayout.Children.Add(detailLabel);
                
            }
        }
       


        //work block
		public class Work
        {
            public class WorkItem
            {
                public string id { get; set; }
                public string name { get; set; }
                public string explain { get; set; }
                public string image { get; set; }
            }

            public DataRetriever hWorkRetriever;
            public static string UrlOfData = "http://192.168.1.108:8080/akuzman/api/mobile1/main.php?router=json_work_service";
            public string mWorkData;
            public List<WorkItem> workItems { get; set; }
            public StackLayout WorkLayout;
            public HomePage home;
            public Work()
            {
                hWorkRetriever = new DataRetriever(UrlOfData);
                workItems = new List<WorkItem>();
                WorkLayout = new StackLayout { };
            }
            public async Task FetchWorkItems ()
            {
                
                await hWorkRetriever.ReadByUrl();
                this.mWorkData = hWorkRetriever.RawJson;
                workItems = JsonConvert.DeserializeObject<List<WorkItem>>(mWorkData);
                StackLayout workStacklayout = new StackLayout { BackgroundColor = Color.White, Padding = new Thickness(20,0,20,0)};
                Label mTitle = new Label {Text="Çalışmalarımız",BackgroundColor=Color.White,FontSize = 20 ,TextColor = Color.FromHex("#00558f")};
                workStacklayout.Children.Add(mTitle);
                //Clickable Label
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => OnWorkLabelClicked(s, e);
                //Clickable Label
                foreach (WorkItem item in workItems)
                {
                    Label label = new Label {TextColor=Color.Blue,FontSize=14,Text= item.name,ClassId=item.id };
                    //Clickable Label
                    label.GestureRecognizers.Add(tgr);
                    //Clickable Label
                    workStacklayout.Children.Add(label);
                }

                WorkLayout = workStacklayout;
            }
            private void OnWorkLabelClicked(object s, EventArgs e) //popup happens
            {
                Label label = (Label)s;
                WorkItem workItem = workItems.Find(x=> x.id == label.ClassId );
                MPopUpPage mPopUpPage = new MPopUpPage(workItem);
                home.mMenu.PopUp(mPopUpPage);
            }
        }


        //Footer

		public Layout FooterLayouts()
		{
			StackLayout FooterLayout = new StackLayout();
			Image mImage = new Image {Source= "https://yoyoimage.com/wp-content/uploads/2017/08/1-14-52-800x303.png" };
			FooterLayout.Children.Add(mImage);
			return FooterLayout;
		}





        //Main Block
        public HomePage()
        {
            OverAllStack = new StackLayout {BackgroundColor= Color.White };
            FetchAll();
           
            InitializeComponent();


            ScrollView scrollView = new ScrollView();
            scrollView.Content = OverAllStack;
            Content = scrollView;
            
        }
        public async void FetchAll()
        {
			Slider slider = new Slider();
            Info info = new Info();
            Work mWork = new Work();

            mWork.home = this;
			await slider.FetchSliderItems();
            await info.FetchInfo();
            await mWork.FetchWorkItems();
			InsertLayouts(new Layout [] {slider.sliderLayout,info.InfoStackLayout,mWork.WorkLayout,FooterLayouts()});
        }
        public void InsertLayouts(Layout[] layouts)
        {
            if (layouts != null)
            {

                 foreach (Layout item in layouts)
                 {
                    OverAllStack.Children.Add(item);
                 }
            }
        }
        
    }
}