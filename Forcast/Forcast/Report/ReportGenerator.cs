using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Forcast.Report
{
	public class ReportGenerator
	{
		private const string Caption1 = "Зоны поражения и количество пораженных на потенциально поражаемом объекте";
		private readonly BaseFont baseFont = BaseFont.CreateFont(@"D:\AhovRep\ahov\Forcast\Forcast\Report\TimesNewRomanRegular.ttf", "Cp1251", false);

		public ReportGenerator()
		{
			
		}

		public byte[] GenerateReport(ReportModel model)
		{
			var doc = new Document(PageSize.A4.Rotate());
			byte[] result;
			using (var memoryStream = new MemoryStream())
			{
				var font = new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK);
				PdfWriter.GetInstance(doc, memoryStream);
				doc.Open();
				var c = new Chunk(Caption1, font);
				var par = new Paragraph {c};
				par.Alignment = Element.ALIGN_CENTER;
				doc.Add(par);
				PrintCaptionRight("Таблица 1", doc);
				GenerateTable1(doc, model);
				doc.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		private void PrintCaptionRight(string text, Document doc)
		{
			var font = new Font(baseFont, 14, Font.ITALIC, BaseColor.BLACK);
			var c = new Chunk(text, font);
			var par = new Paragraph { c };
			par.Alignment = Element.ALIGN_RIGHT;
			doc.Add(par);
		}

		private void GenerateTable1(Document doc, ReportModel model)
		{
			var table = new PdfPTable(8);
			var font = new Font(baseFont, 12,
				Font.NORMAL);
			table
				.AddCellToTable("Потери и типы поражения", font, 5, 1, 3)
				.AddCellToTable("Зоны поражения", font, 5, 4, 1)
				.AddCellToTable("Количество пораженных", font, 5, 3, 1)
				.AddCellToTable("глубина, м", font, 4, 2, 1)
				.AddCellToTable("площадь, кв.м", font, 5, 2, 1)
				.AddCellToTable("взрослые", font, 5, 1, 2)
				.AddCellToTable("дети", font, 5, 1, 2)
				.AddCellToTable("всего", font, 5, 1, 2)
				.AddCellToTable("Для взрослых", font, 5, 1, 1)
				.AddCellToTable("Для детей", font, 5, 1, 1)
				.AddCellToTable("Для взрослых", font, 5, 1, 1)
				.AddCellToTable("Для детей", font, 5, 1, 1);
			var title = new[]
			{
				"без возвратные потери", "поражения тяжелой степени", "поражения средней тяжести", "поражения легкой тяжести",
				"Пороговые воздействия"
			};
			for (int i=0; i<title.Length; i++)
			{
				table.AddCell(new Phrase(title[i], font));
				table.AddCellToTable(model.Gau[i, 0].ToPrettyDouble(), font, 5, 1, 1)
					.AddCellToTable(model.Gau[i, 1].ToPrettyDouble(), font, 5, 1, 1)
					.AddCellToTable(model.Sau[i, 0].ToPrettyDouble(), font, 5, 1, 1)
					.AddCellToTable(model.Sau[i, 1].ToPrettyDouble(), font, 5, 1, 1)
					.AddCellToTable(model.Nfs[i, 0].ToPrettyDouble(), font, 5, 1, 1)
					.AddCellToTable(model.Nfs[i, 1].ToPrettyDouble(), font, 5, 1, 1)
					.AddCellToTable(model.NfAll[i, 0].ToPrettyDouble(), font, 5, 1, 1);
			}
			doc.Add(table);
		}
	}
}