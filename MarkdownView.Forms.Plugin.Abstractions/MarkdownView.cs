using System;
using Xamarin.Forms;
using CommonMark;

namespace MarkdownView.Forms.Plugin.Abstractions
{
	public class MarkdownView : WebView
	{
		public MarkdownView (LinkRenderingOption linksOption = LinkRenderingOption.Underline)
		{
			var baseUrl = DependencyService.Get<IWebViewBaseUrl> ();


			if (linksOption == LinkRenderingOption.Underline)
				CommonMarkSettings.Default.OutputDelegate =
					(doc, output, settings) => 
						new UnderlineLinksHtmlFormatter(output, settings).WriteDocument(doc);
			
			if (linksOption == LinkRenderingOption.None)
				CommonMarkSettings.Default.OutputDelegate =
					(doc, output, settings) => 
						new NoneLinksHtmlFormatter(output, settings).WriteDocument(doc);
			
		}

		public static readonly BindableProperty StylesheetProperty =
			BindableProperty.Create<MarkdownView, String>(
				p => p.Stylesheet, "");
		
		public String Stylesheet
		{
			get { return (String)GetValue(StylesheetProperty); }
			set 
			{ 
				SetValue(StylesheetProperty, value); 
				SwapStylesheet (value);
			}
		}

		public static readonly BindableProperty MarkdownProperty =
			BindableProperty.Create<MarkdownView, String>(
				p => p.Markdown, "");

		public String Markdown
		{
			get { return (String)GetValue(MarkdownProperty); }
			set 
			{ 
				SetValue(MarkdownProperty, value); 
				SetWebViewSourceFromMarkdown (value); 
			}
		}

		private void SetWebViewSourceFromMarkdown(string markdown)
		{
			const string swapCssFunction =
				"function _sw(e){document.getElementById('_ss').setAttribute('href',e+'.css');}";
			const string head = "<head><meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=no'><link id='_ss' rel='stylesheet' /><script>" + swapCssFunction + "</script></head>";

			var htmlContent = "<body>" + CommonMarkConverter.Convert(Content) + "</body>";

			Source = new HtmlWebViewSource { Html = "<html>" + head + htmlContent + "</html>", BaseUrl = BaseUrl };

			SwapStylesheet (Stylesheet);
		}

		void SwapStylesheet(string stylesheetName)
		{
			Eval("_sw(\"" + stylesheetName + "\")");
		}
	}
}

