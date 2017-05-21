using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Forcast.Report
{
	public static class ReportExtentions
	{
		public static PdfPTable AddCellToTable(this PdfPTable table, string text, Font font, int padding = 5, int colspan =1, int rowspan = 1)
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

		public static PdfPTable AddPairToTable(this PdfPTable table, string key, string value, Font font)
		{
			table.AddCellToTable(key, font)
				.AddCellToTable(value, font);
			return table;
		}

		public static PdfPTable AddPairToTable(this PdfPTable table, string key, double value, Font font)
		{
			table.AddCellToTable(key, font)
				.AddCellToTable(value, font);
			return table;
		}

		public static PdfPTable AddCellToTable(this PdfPTable table, double value, Font font, int padding = 5, int colspan = 1, int rowspan = 1)
		{
			return AddCellToTable(table, value.ToPrettyDouble(), font, padding, colspan, rowspan);
		}

		public static string ToPrettyDouble(this double value)
		{
			return value > 1 ? ((int) value).ToString() : $"{value:F2}";
		}

		public static string ToRusString(this MatterSaveType type)
		{
			if (type == MatterSaveType.Cx1)
				return "Изотермический";
			if (type == MatterSaveType.Cx2)
				return "Под давлением";
			return "Обынчый";
		}
	}
}