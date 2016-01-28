using System;
using Xamarin.Forms;
using CommonMark;

namespace ViewMarkdown.Forms.Plugin.Abstractions
{
	public class MarkdownView : WebView
	{
		private readonly string _baseUrl;
		public MarkdownView (LinkRenderingOption linksOption = LinkRenderingOption.Underline)
		{
			var baseUrlResolver = DependencyService.Get<IWebViewBaseUrl> ();
			_baseUrl = baseUrlResolver.Url;

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
				SetStylesheet ();
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
				SetWebViewSourceFromMarkdown (); 
			}
		}

		private void SetWebViewSourceFromMarkdown()
		{
			const string swapCssFunction =
				"function _sw(e){document.getElementById('_ss').setAttribute('href',e+'.css');}";
			const string head = "<head><meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=no'><link id='_ss' rel='stylesheet' /><script>" + swapCssFunction + "</script></head>";

			var body = "<body>" + CommonMarkConverter.Convert(Markdown) + "</body>";

			Source = new HtmlWebViewSource { Html = "<html>" + head + body + "</html>", BaseUrl = _baseUrl };

			SetStylesheet ();
		}

		void SetStylesheet()
		{
			Eval("_sw(\"" + Stylesheet + "\")");
		}
	}
}

