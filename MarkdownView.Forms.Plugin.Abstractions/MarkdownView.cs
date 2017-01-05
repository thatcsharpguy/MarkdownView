using System;
using Xamarin.Forms;
using CommonMark;

namespace ViewMarkdown.Forms.Plugin.Abstractions
{
    /// <summary>
    /// A View that presents Markdown content.
    /// </summary>
    public class MarkdownView : WebView
    {
        private readonly string _baseUrl;

        /// <summary>
        /// Creates a new MarkdownView
        /// </summary>
        /// <param name="linksOption">Tells the view how to render links.</param>
        public MarkdownView(LinkRenderingOption linksOption = LinkRenderingOption.Underline)
        {
            var baseUrlResolver = DependencyService.Get<IWebViewBaseUrl>();
            _baseUrl = baseUrlResolver?.Url;

            if (linksOption == LinkRenderingOption.Underline)
                CommonMarkSettings.Default.OutputDelegate =
                    (doc, output, settings) =>
                        new UnderlineLinksHtmlFormatter(output, settings).WriteDocument(doc);

            if (linksOption == LinkRenderingOption.None)
                CommonMarkSettings.Default.OutputDelegate =
                    (doc, output, settings) =>
                        new NoneLinksHtmlFormatter(output, settings).WriteDocument(doc);

        }

        /// <summary>
        /// Backing store for the MarkdownView.Stylesheet property
        /// </summary>
        public static readonly BindableProperty StylesheetProperty =
            BindableProperty.Create<MarkdownView, string>(
                p => p.Stylesheet, "");

        /// <summary>
        /// Gets or sets the stylesheet that will be applied to the document
        /// </summary>
        public string Stylesheet
        {
            get { return (String)GetValue(StylesheetProperty); }
            set
            {
                SetValue(StylesheetProperty, value);
                SetStylesheet();
            }
        }

        /// <summary>
        /// Backing storage for the MarkdownView.Markdown property
        /// </summary>
        public static readonly BindableProperty MarkdownProperty =
            BindableProperty.Create<MarkdownView, string>(
                p => p.Markdown, "");

        /// <summary>
        /// The markdown content
        /// </summary>
        public string Markdown
        {
            get { return (String)GetValue(MarkdownProperty); }
            set
            {
                SetValue(MarkdownProperty, value);
                SetWebViewSourceFromMarkdown();
            }
        }

        private void SetWebViewSourceFromMarkdown()
        {
            string swapCssFunction =
                @"
function _sw(e){ 
    var cssFile = '" + _baseUrl + "'+e+'.css'; " +
  @"document.getElementById('_ss').setAttribute('href',cssFile);
}";
            string head = @"
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=no'>
    <link id='_ss' rel='stylesheet' href='#' >
    <script>" + swapCssFunction + @"</script>
</head>";

            var body = @"
<body>" +
    CommonMarkConverter.Convert(Markdown) +
"</body>";

            Source = new HtmlWebViewSource { Html = "<html>" + head + body + "</html>", BaseUrl = _baseUrl };

            SetStylesheet();
        }

        private void SetStylesheet()
        {
            if (!String.IsNullOrEmpty(Stylesheet))
            {
                Eval("_sw(\"" + Stylesheet + "\")");
            }
        }
    }
}

