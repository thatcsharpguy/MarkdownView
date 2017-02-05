using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ViewMarkdown.Forms.Plugin.Uwp
{
    public class MarkdownViewRenderer
    {
        public static void Init()
        {
            DependencyService.Register<WebViewBaseUrl>();
        }
    }
}
