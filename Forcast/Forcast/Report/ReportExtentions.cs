using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Forcast.Report
{
	public static class ReportExtentions
	{
		public static PdfPTable AddCellToTable(this PdfPTable table, string text, Font font, int padding, int colspan, int rowspan)
		{
			var cell = new PdfPCell(new Phrase(text, font))
			{
				Padding = padding,
				Colspan = colspan,
				Rowspan = rowspan,
				HorizontalAlignment = Element.ALIGN_CENTER
			};
			table.AddCell(cell);
			return table;
		}

		public static string ToPrettyDouble(this double value)
		{
			return value > 1 ? ((int) value).ToString() : $"{value:F2}";
		}
	}
}