using ViewMarkdown.Forms.Plugin.Abstractions;
using ViewMarkdown.Forms.Plugin.WinPhone;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebViewBaseUrl))]
namespace ViewMarkdown.Forms.Plugin.WinPhone
{
    public class WebViewBaseUrl : IWebViewBaseUrl
    {
        public string Url
        {
            get { return "ms-appx-web:///Assets/"; }
        }
    }
}
