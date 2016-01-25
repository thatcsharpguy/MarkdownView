using System;
using MarkdownView.Forms.Plugin.Abstractions;

namespace MarkdownVIew.Forms.Plugin.Droid
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

