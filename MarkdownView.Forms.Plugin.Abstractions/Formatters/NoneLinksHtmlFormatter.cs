using System;
using CommonMark;
using CommonMark.Syntax;

namespace ViewMarkdown.Forms.Plugin.Abstractions
{
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
					this.Write(inline.LiteralContent);
				}
			}
			else
			{
				base.WriteInline(inline, isOpening, isClosing, out ignoreChildNodes);
			}
		}
	}
}

