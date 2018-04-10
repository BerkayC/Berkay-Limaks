using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FreeToPlay.mViews
{
    public partial class ListViewPage : ContentPage
    {
        public class MyObject
        {
            public int Id{get;set;}
            public string Name{get;set;}
        }


        public ListViewPage()
        {
            InitializeComponent();
            List<MyObject> myObjects = new List<MyObject>();
            myObjects.Add(new MyObject { Id = 1, Name="Kitap" });
            myObjects.Add(new MyObject { Id = 2, Name = "Fare" });
            myObjects.Add(new MyObject { Id = 3, Name = "Telefon" });
            myObjects.Add(new MyObject { Id = 4, Name = "Cüzdan" });
            lstView.ItemsSource = myObjects;
        }
    }
}
