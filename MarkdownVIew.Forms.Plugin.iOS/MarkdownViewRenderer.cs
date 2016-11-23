using System;
using Xamarin.Forms;

namespace ViewMarkdown.Forms.Plugin.iOS
{
	public class MarkdownViewRenderer
	{
		public static void Init()
		{
			DependencyService.Register<WebViewBaseUrl>();
		}
	}
}
