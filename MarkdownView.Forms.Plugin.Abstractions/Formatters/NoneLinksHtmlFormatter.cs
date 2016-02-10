using System;
using CommonMark;
using CommonMark.Syntax;

namespace ViewMarkdown.Forms.Plugin.Abstractions
{
    /// <summary>
    /// A formatter that will ignore all link tags inside a markdown document
    /// </summary>
	internal class NoneLinksHtmlFormatter: CommonMark.Formatters.HtmlFormatter
	{
		public NoneLinksHtmlFormatter(System.IO.TextWriter target, CommonMarkSettings settings)
			: base(target, settings)
		{
		}

		protected override void WriteInline(Inline inline, bool isOpening, bool isClosing, out bool ignoreChildNodes)
		{
			if (inline.Tag == InlineTag.Link
				&& !this.RenderPlainTextInlines.Peek())
			{
				ignoreChildNodes = false;

				if (isOpening)
				{
                    Write(inline.LiteralContent);
				}
			}
			else
			{
				base.WriteInline(inline, isOpening, isClosing, out ignoreChildNodes);
			}
		}
	}
}

