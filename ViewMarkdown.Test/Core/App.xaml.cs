using ViewMarkdown.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace ViewMarkdown.Test
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			var _webView = new MarkdownView
			{
				Markdown = @"# MarkdownView
A control that allows you to show formatted markdown in your Xamarin Forms application.",
				VerticalOptions = LayoutOptions.FillAndExpand
			};


			MainPage = new ContentPage()
			{
				// Accomodate iPhone status bar.
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
				Content = _webView
			};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
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
