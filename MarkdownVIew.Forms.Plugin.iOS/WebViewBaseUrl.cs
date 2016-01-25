using MarkdownView.Forms.Plugin.Abstractions;
using MarkdownVIew.Forms.Plugin.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebViewBaseUrl))]
namespace MarkdownVIew.Forms.Plugin.iOS
{
	public class WebViewBaseUrl : IWebViewBaseUrl
	{
		#region IWebViewBaseUrl implementation
		public string Url {
			get {
				return Foundation.NSBundle.MainBundle.BundlePath + "/";
			}
		}
		#endregion
	}
}

