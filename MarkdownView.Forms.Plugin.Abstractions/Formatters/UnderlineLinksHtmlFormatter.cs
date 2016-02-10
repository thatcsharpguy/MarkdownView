using System;
using CommonMark.Syntax;
using CommonMark;

namespace ViewMarkdown.Forms.Plugin.Abstractions
{
    /// <summary>
    /// A formatter that will underline all link tags inside a markdown document
    /// </summary>
	internal class UnderlineLinksHtmlFormatter: CommonMark.Formatters.HtmlFormatter
	{
		public UnderlineLinksHtmlFormatter(System.IO.TextWriter target, CommonMarkSettings settings)
			: base(target, settings)
		{
		}

		protected override void WriteInline(Inline inline, bool isOpening, bool isClosing, out bool ignoreChildNodes)
		{
			if (inline.Tag == InlineTag.Link
				&& !this.RenderPlainTextInlines.Peek())
			{
				ignoreChildNodes = false;

				// start and end of each node may be visited separately
				if (isOpening)
				{
					this.Write("<u>");
					this.Write(inline.LiteralContent);
				}

				if (isClosing)
				{
					this.Write("</u>");
				}
			}
			else
			{
				base.WriteInline(inline, isOpening, isClosing, out ignoreChildNodes);
			}
		}
	}
}

