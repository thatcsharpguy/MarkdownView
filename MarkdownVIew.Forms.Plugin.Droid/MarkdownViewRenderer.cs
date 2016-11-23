using System;
using Xamarin.Forms;

namespace ViewMarkdown.Forms.Plugin.Droid
{
	public class MarkdownViewRenderer
	{
		public static void Init()
		{
			DependencyService.Register<WebViewBaseUrl>();
		}
	}
}
