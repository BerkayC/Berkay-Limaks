using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace FreeToPlay.mViews
{
    public partial class ListViewPage : ContentPage
    {
        public class MyObject
        {
            public int Id {get;set;}
            public String Name{get;set;}
        }

        public List<MyObject> myObjects{ get; set; }

        public ListViewPage()
        {
            InitializeComponent();
            myObjects = new List<MyObject>();

            if (myObjects !=null)
            {
				myObjects.Add(new MyObject { Id = 1, Name="Kitap" });
				myObjects.Add(new MyObject { Id = 2, Name = "Fare" });
                myObjects.Add(new MyObject { Id = 5, Name = "Fare" });
				myObjects.Add(new MyObject { Id = 3, Name = "Telefon" });
				myObjects.Add(new MyObject { Id = 4, Name = "Cüzdan" });

            }
            lstView.ItemsSource = myObjects;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            string idtobedeleted = (string)btn.CommandParameter;
            List<MyObject> mDeletionList = new List<MyObject>();
			List<MyObject> mEa = new List<MyObject>();
		//	mDeletionList.Add((FreeToPlay.mViews.ListViewPage.MyObject)myObjects.Where(p => p.Name == idtobedeleted));
            var mitem = myObjects.Where(p => p.Name == idtobedeleted);

            //        foreach (MyObject item in myObjects)
            //            {
            //            if (item.Name == idtobedeleted)
            //                {
            //                    mEa.Add(item);
            //                }
            //            }
            //        lstView.ItemsSource = null;
            //        lstView.ItemsSource = myObjects;
            //        foreach (var item in mEa)
            //        {

            //myObjects.Remove(item);
            //}
            //myObjects.Remove((FreeToPlay.mViews.ListViewPage.MyObject)myObjects.Where(p => p.Name == idtobedeleted));
            foreach (MyObject item in mitem)
            {
                mEa.Add(item);               
            }
            foreach (var item in mEa)
               {

                myObjects.Remove(item);
               }

            lstView.ItemsSource = null;
            lstView.ItemsSource = myObjects;
        }
    }
}
