using ViewMarkdown.Forms.Plugin.Abstractions;
using ViewMarkdown.Forms.Plugin.Droid;
using Xamarin.Forms;


[assembly: Dependency(typeof(WebViewBaseUrl))]
namespace ViewMarkdown.Forms.Plugin.Droid
{
	public class WebViewBaseUrl : IWebViewBaseUrl
	{
		#region IWebViewBaseUrl implementation
		string IWebViewBaseUrl.Url {
			get {
				return "file:///android_asset/";
			}
		}
		#endregion
		
	}
}

