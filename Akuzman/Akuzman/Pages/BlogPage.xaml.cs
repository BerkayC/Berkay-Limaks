using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Akuzman.Logic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Akuzman.Pages
{
	public partial class BlogPage : ContentPage
	{
		public DataRetriever blogRetriever;
		public string UrlForData = "http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_blog_list";
		public string blogsData;
		public List<blogItem> blogsPageItems;
		public StackLayout blogspageStack;
		public aMenu mMenu;
		public class blogItem
		{
			public string id { get; set; }
			public string title { get; set; }
			public string summary { get; set; }
			public string image { get; set; }
		}
		public BlogPage()
		{
			blogsPageItems = new List<blogItem>();
			blogspageStack = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
			blogspageStack.Children.Add(new Label { Text = "Blog Yazıları", FontSize = 20, TextColor = Color.Gray });
			InitializeComponent();
			buildBlogpageAsync();
			Content = blogspageStack;
		}
		public async Task buildBlogpageAsync()
		{
			blogRetriever = new DataRetriever(UrlForData);
			await blogRetriever.ReadByUrl();
			blogsData = blogRetriever.RawJson;

			blogsPageItems = JsonConvert.DeserializeObject<List<blogItem>>(blogsData);
			foreach (var blogitem in blogsPageItems)
			{
				//Clickable layout
				var tgr = new TapGestureRecognizer();
				tgr.Tapped += (s, e) => OnClicked(s, e);
				//Clickable layout
				Image blogsImage = new Image { Source = blogitem.image, HorizontalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.AspectFill, ClassId = blogitem.id };
				Label title = new Label { Text = blogitem.title, FontSize = 25, TextColor = Color.Blue, HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, ClassId = blogitem.id };
				Label summary = new Label { Text = blogitem.summary, FontSize = 15, ClassId = blogitem.id };
				StackLayout blogitemStack = new StackLayout { ClassId = blogitem.id };
				blogitemStack.Children.Add(blogsImage);
				blogitemStack.Children.Add(title);
				blogitemStack.Children.Add(summary);
				blogspageStack.Children.Add(blogitemStack);
				blogitemStack.GestureRecognizers.Add(tgr);
			}
		}

		private void OnClicked(object s, EventArgs e)
		{
			StackLayout blogStack = (StackLayout)s;
			string id = blogStack.ClassId;
			//blog detail
			BlogDetail blogDetail = new BlogDetail(id);
			blogDetail.mBlogpage = this;      
		}
		public class BlogDetail 
		{
			public class BlogDetailItem
			{
				public string id { get; set; }
				public string title { get; set; }
				public string content { get; set; }
				public string image { get; set; }                
            }
			public DataRetriever blogDetailRetriever;
            public string BlogDetailData { get; set; }
			public List<BlogDetailItem> BlogDetailItems { get; set; }
			public string UrlForData ="http://limaxbilisim.com/demo/akuzman/api/mobile1/main.php?router=json_blog_single&id=" ;
			public BlogPage mBlogpage;
			public BlogDetail(string id)
			{
				UrlForData = UrlForData + id;
				BlogDetailItems = new List<BlogDetailItem>();
				buildBlogDetail();
			}
			public async Task buildBlogDetail() 
			{
				blogDetailRetriever = new DataRetriever(UrlForData);
				await blogDetailRetriever.ReadByUrl();
				BlogDetailData = blogDetailRetriever.RawJson;
				BlogDetailItem blogDetailitem = JsonConvert.DeserializeObject<BlogDetailItem>(BlogDetailData);




				MPopUpPage mPopUpPage = new MPopUpPage(blogDetailitem);
				mBlogpage.mMenu.PopUp(mPopUpPage);
			}           
        }
	}
}
