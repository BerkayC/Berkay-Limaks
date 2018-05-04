using Akuzman.Views;
using Xamarin.Forms;
using Akuzman.Logic;

namespace Akuzman
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new aMasterPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //DataRetriever dataretriever = new DataRetriever();

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
