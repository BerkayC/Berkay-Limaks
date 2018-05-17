using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup;
using Xamarin.Forms;
using static Akuzman.Pages.HomePage.Work;
using Rg.Plugins.Popup.Interfaces.Animations;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Animations;
using static Akuzman.Pages.BlogPage.BlogDetail;
using static Akuzman.Pages.AboutPage;

namespace Akuzman.Pages
{
    public class MPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {


        #region Internal Properties

        internal bool IsBeingDismissed { get; set; }

        #endregion

        #region Events

        public event EventHandler BackgroundClicked;

        #endregion

        #region Bindable Properties

       
        public static readonly BindableProperty IsAnimatingProperty = BindableProperty.Create(nameof(IsAnimating), typeof(bool), typeof(PopupPage), true);

       
        public bool IsAnimating
        {
            get { return (bool)GetValue(IsAnimatingProperty); }
            set { SetValue(IsAnimatingProperty, value); }
        }

        public static readonly BindableProperty IsAnimationEnabledProperty = BindableProperty.Create(nameof(IsAnimationEnabled), typeof(bool), typeof(PopupPage), true);

        public bool IsAnimationEnabled
        {
            get { return (bool)GetValue(IsAnimationEnabledProperty); }
            set { SetValue(IsAnimationEnabledProperty, value); }
        }

        public static readonly BindableProperty HasSystemPaddingProperty = BindableProperty.Create(nameof(HasSystemPadding), typeof(bool), typeof(PopupPage), true);

        public bool HasSystemPadding
        {
            get { return (bool)GetValue(HasSystemPaddingProperty); }
            set { SetValue(HasSystemPaddingProperty, value); }
        }

        public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(IPopupAnimation), typeof(PopupPage));

        public IPopupAnimation Animation
        {
            get { return (IPopupAnimation)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        public static readonly BindableProperty SystemPaddingProperty = BindableProperty.Create(nameof(SystemPadding), typeof(Thickness), typeof(PopupPage), default(Thickness), BindingMode.OneWayToSource);

        public Thickness SystemPadding
        {
            get { return (Thickness)GetValue(SystemPaddingProperty); }
            private set { SetValue(SystemPaddingProperty, value); }
        }

        public static readonly BindableProperty CloseWhenBackgroundIsClickedProperty = BindableProperty.Create(nameof(CloseWhenBackgroundIsClicked), typeof(bool), typeof(PopupPage), true);

        public bool CloseWhenBackgroundIsClicked
        {
            get { return (bool)GetValue(CloseWhenBackgroundIsClickedProperty); }
            set { SetValue(CloseWhenBackgroundIsClickedProperty, value); }
        }

        public static readonly BindableProperty BackgroundInputTransparentProperty = BindableProperty.Create(nameof(BackgroundInputTransparent), typeof(bool), typeof(PopupPage), false);

        public bool BackgroundInputTransparent
        {
            get { return (bool)GetValue(BackgroundInputTransparentProperty); }
            set { SetValue(BackgroundInputTransparentProperty, value); }
        }

        #endregion

        #region Main Methods

       

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(HasSystemPadding):
                    ForceLayout();
                    break;
                case nameof(IsAnimating):
                    IsAnimationEnabled = IsAnimating;
                    break;
                case nameof(IsAnimationEnabled):
                    IsAnimating = IsAnimationEnabled;
                    break;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        #endregion

        #region Size Methods

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (!HasSystemPadding)
            {
				base.LayoutChildren(x, y, width, height);
                return;
            }

            var systemPadding = SystemPadding;

            x += systemPadding.Left;
            y += systemPadding.Top;
            width -= systemPadding.Left + systemPadding.Right;
            height -= systemPadding.Top + systemPadding.Bottom;
            
			//base.LayoutChildren(width/6, y+40, width*2/3, height-60);
			base.LayoutChildren(x+30, y , width-60 , height);
            
        }

        #endregion

        #region Animation Methods

        internal void PreparingAnimation()
        {
            if (IsAnimationEnabled)
                Animation?.Preparing(Content, this);
        }

        internal void DisposingAnimation()
        {
            if (IsAnimationEnabled)
                Animation?.Disposing(Content, this);
        }

        internal async Task AppearingAnimation()
        {
            OnAppearingAnimationBegin();
            await OnAppearingAnimationBeginAsync();

            if (IsAnimationEnabled && Animation != null)
                await Animation.Appearing(Content, this);

            OnAppearingAnimationEnd();
            await OnAppearingAnimationEndAsync();
        }

        internal async Task DisappearingAnimation()
        {
            OnDisappearingAnimationBegin();
            await OnDisappearingAnimationBeginAsync();

            if (IsAnimationEnabled && Animation != null)
                await Animation.Disappearing(Content, this);

            OnDisappearingAnimationEnd();
            await OnDisappearingAnimationEndAsync();
        }

        #endregion

        #region Override Animation Methods

        protected virtual void OnAppearingAnimationBegin()
        {
        }

        protected virtual void OnAppearingAnimationEnd()
        {
        }

        protected virtual void OnDisappearingAnimationBegin()
        {
        }

        protected virtual void OnDisappearingAnimationEnd()
        {
        }

        protected virtual Task OnAppearingAnimationBeginAsync()
        {
            return Task.FromResult(0);
        }

        protected virtual Task OnAppearingAnimationEndAsync()
        {
            return Task.FromResult(0);
        }

        protected virtual Task OnDisappearingAnimationBeginAsync()
        {
            return Task.FromResult(0);
        }

        protected virtual Task OnDisappearingAnimationEndAsync()
        {
            return Task.FromResult(0);
        }

        #endregion

        #region Background Click

        protected virtual bool OnBackgroundClicked()
        {
            return CloseWhenBackgroundIsClicked;
        }

        #endregion

        #region Internal Methods



        internal void SetSystemPadding(Thickness systemPadding, bool forceLayout = true)
        {
            var systemPaddingWasChanged = SystemPadding != systemPadding;
            SystemPadding = systemPadding;

            if (systemPaddingWasChanged && forceLayout)
                ForceLayout();
        }

        #endregion
        /// <summary>
        /// WorkItem PopUp
        /// </summary>
        public WorkItem PopUpItem;
        public StackLayout stackLayout;
        public MPopUpPage(WorkItem workItem)
        {
            PopUpItem= workItem;
            BackgroundColor = Color.FromHex("#80000000");
            Animation = new ScaleAnimation();
			Image mImage = new Image { Source = PopUpItem.image, Aspect=Aspect.AspectFill };
            Label mLabel = new Label { Text = PopUpItem.explain, FontSize = 15 };
            stackLayout = new StackLayout { BackgroundColor = Color.White };
            stackLayout.Children.Add(mImage);
            stackLayout.Children.Add(mLabel);
            ScrollView scrollView = new ScrollView { Orientation = ScrollOrientation.Vertical, HorizontalOptions = LayoutOptions.Center };
            scrollView.Content = stackLayout;
            Content = scrollView;
        }

		/// <summary>
        /// BlogDetailItem PopUp
        /// </summary>
		public BlogDetailItem blogDetailItem;

		public MPopUpPage(BlogDetailItem blogDetail)
		{
			blogDetailItem = blogDetail;
			BackgroundColor = Color.FromHex("#80000000");
            Animation = new ScaleAnimation();
			Label title = new Label { Text=blogDetailItem.title,FontSize=25,TextColor=Color.Blue };
			Image mImage = new Image { Source = blogDetailItem.image };



            //html format
            MyView objMyView = new MyView();
			objMyView.MyHtml = blogDetailItem.content;

            HtmlWebViewSource objHtmlWebViewSource = new HtmlWebViewSource();
            objHtmlWebViewSource.SetBinding(HtmlWebViewSource.HtmlProperty, "MyHtml");
            objHtmlWebViewSource.BindingContext = objMyView;

            WebView objWebview = new WebView();
            objWebview.HorizontalOptions = LayoutOptions.FillAndExpand;
            objWebview.VerticalOptions = LayoutOptions.FillAndExpand;
            objWebview.Source = objHtmlWebViewSource;
            //html format

            
        

            StackLayout blogstackLayout = new StackLayout { BackgroundColor = Color.White };
			blogstackLayout.Children.Add(title);
			blogstackLayout.Children.Add(mImage);
			blogstackLayout.Children.Add(objWebview);
            ScrollView scrollView = new ScrollView { Orientation = ScrollOrientation.Vertical, HorizontalOptions = LayoutOptions.Center };
			scrollView.Content = blogstackLayout;
            Content = scrollView;
		}
		public MPopUpPage(AboutItem aboutItem)
        {
            
            BackgroundColor = Color.FromHex("#80000000");
            Animation = new ScaleAnimation();
			Label title = new Label { Text = aboutItem.title,FontSize=25,HorizontalTextAlignment=TextAlignment.Center };
			Image mImage = new Image { Source = aboutItem.image, Aspect = Aspect.AspectFit ,HeightRequest=100,WidthRequest=400, HorizontalOptions = LayoutOptions.Start };
			Label mLabel = new Label { Text = aboutItem.content, FontSize = 15 };
            StackLayout aboutStacklayout = new StackLayout { BackgroundColor = Color.White };
			aboutStacklayout.Children.Add(title);
			aboutStacklayout.Children.Add(mImage);
			aboutStacklayout.Children.Add(mLabel);
            ScrollView scrollView = new ScrollView { Orientation = ScrollOrientation.Vertical, HorizontalOptions = LayoutOptions.Center };
			scrollView.Content = aboutStacklayout;
            Content = scrollView;
        }

        public class MyView:View
        {
            public static readonly BindableProperty MyHtmlProperty = BindableProperty.Create<MyView, string>(p => p.MyHtml, default(string));

            public string MyHtml
            {
                get { return (string)GetValue(MyHtmlProperty); }
                set { SetValue(MyHtmlProperty, value); }
            }
        }

       

    }
}
