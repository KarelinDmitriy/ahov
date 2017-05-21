using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forcast.V2;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Forcast.Report
{
	public class ReportGenerator
	{
		private const string Caption1 = "Зоны поражения и количество пораженных на потенциально поражаемом объекте";
		private readonly BaseFont baseFont;

		public ReportGenerator()
			:this(@"D:\AhovRep\ahov\Forcast\Forcast\Report\TimesNewRomanRegular.ttf")
		{
		}

		public ReportGenerator(string baseFontPath)
		{
			baseFont = BaseFont.CreateFont(baseFontPath, "Cp1251", false);
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
				PrintStorageData(doc, model.InputData.StorageData);
				PrintBarrelsData(doc, model.InputData.Barrels);
				PrintActiveData(doc, model.InputData.ActiveData);
				doc.NewPage();
				var c = new Chunk(Caption1, font);
				var par = new Paragraph {c};
				par.Alignment = Element.ALIGN_CENTER;
				doc.Add(par);
				PrintCaption(doc, "Таблица 1", Element.ALIGN_RIGHT);
				GenerateTable1(doc, model);
				doc.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		private void PrintStorageData(Document doc, StorageData storageData)
		{
			PrintCaption(doc, "Информация об объекте\n", Element.ALIGN_LEFT);
			var table = new PdfPTable(4);
			var font = new Font(baseFont, 12,
				Font.NORMAL);
			table
				.AddCellToTable("Параметр", font)
				.AddCellToTable("Значение", font)
				.AddCellToTable("Параметр", font)
				.AddCellToTable("Значение", font);
			table

				.AddPairToTable("Растояние до леса, м", storageData.Gdl, font)
				.AddPairToTable("Глубина леса по направлению ветра, м", storageData.Gl, font)
				.AddPairToTable("Коэфициент физической нагрузки, ед.", storageData.Kf, font)
				.AddPairToTable("Численность персонала, чел.", storageData.No, font)
				.AddPairToTable("Радиус объекта, м", storageData.Ro, font)
				.AddPairToTable("Радиус санитарной зоны, м", storageData.Rz, font)
				.AddPairToTable("Длинна объекта, м", storageData.Цx_p, font)
				.AddPairToTable("Ширина объекта, м", storageData.Цу_p, font);

			doc.Add(table);
			PrintCaption(doc, "Информация о городе\n", Element.ALIGN_LEFT);
			var table2 = new PdfPTable(4);
			table2
				.AddCellToTable("Параметр", font)
				.AddCellToTable("Значение", font)
				.AddCellToTable("Параметр", font)
				.AddCellToTable("Значение", font);
			table2
				.AddPairToTable("Длинна города, м", storageData.Цх, font)
				.AddPairToTable("Ширина города, м", storageData.Цу, font)
				.AddPairToTable("Численность населения, чел.", storageData.N, font)
				.AddPairToTable("Суммарная высота возвышенностей, м", storageData.W, font)
				.AddPairToTable("Число взрослого населения, чел.", storageData.a[0] * storageData.N, font)
				.AddPairToTable("Число детей, чел.", storageData.a[1] * storageData.N, font);

			doc.Add(table2);
		}

		private void PrintBarrelsData(Document doc, List<BarrelV2> barrels)
		{
			PrintCaption(doc, "Информация о веществах", Element.ALIGN_LEFT);
			var table = new PdfPTable(5);
			var font = new Font(baseFont, 12,
				Font.NORMAL);
			table
				.AddCellToTable("Название", font)
				.AddCellToTable("Количество, т", font)
				.AddCellToTable("Тип вылева", font)
				.AddCellToTable("Высота поддона, м", font)
				.AddCellToTable("Тип хранения", font);
			foreach (var barrelV2 in barrels)
			{
				table
					.AddCellToTable(barrelV2.Matter.Data.Name, font)
					.AddCellToTable(barrelV2.Q, font)
					.AddCellToTable(barrelV2.Draining == Draining.Vp1 ? "В поддон" : "На ровную поверхность", font)
					.AddCellToTable(barrelV2.Draining == Draining.Vp1 ? barrelV2.H.ToPrettyDouble() : "-", font)
					.AddCellToTable(barrelV2.SaveType.ToRusString(), font);
			}
			doc.Add(table);
		}

		private void PrintActiveData(Document doc, ActiveData activeData)
		{
			PrintCaption(doc, "Оперативная информация", Element.ALIGN_LEFT);
			var table = new PdfPTable(4);
			var font = new Font(baseFont, 12,
				Font.NORMAL);
			table
				.AddCellToTable("Параметр", font)
				.AddCellToTable("Значение", font)
				.AddCellToTable("Параметр", font)
				.AddCellToTable("Значение", font);
			table
				.AddPairToTable("Время после аварии, сек", activeData.T, font)
				.AddPairToTable("Скорость ветра, м/с", activeData.U, font)
				.AddPairToTable("Температура воздуха, °C", activeData.Tcw, font)
				.AddPairToTable("Вермя оповещения персонала, сек", activeData.To, font)
				.AddPairToTable("Вермя оповещения населения, сек", activeData.Tn[0], font)
				.AddPairToTable("", "", font);
			doc.Add(table);
		}

		private void PrintCaption(Document doc, string text, int aligment)
		{
			var font = new Font(baseFont, 14, Font.ITALIC, BaseColor.BLACK);
			var c = new Chunk(text, font);
			var par = new Paragraph { c };
			par.Alignment = aligment;
			par.Leading = 15f;
			doc.Add(par);
			doc.Add(new Paragraph(new Chunk(" ", font)));
		}

		private void GenerateTable1(Document doc, ReportModel model)
		{
			var table = new PdfPTable(8);
			var font = new Font(baseFont, 12,
				Font.NORMAL);
			table
				.AddCellToTable("Потери и типы поражения", font, 5, 1, 3)
				.AddCellToTable("Зоны поражения", font, 5, 4)
				.AddCellToTable("Количество пораженных", font, 5, 3)
				.AddCellToTable("глубина, м", font, 4, 2)
				.AddCellToTable("площадь, кв.м", font, 5, 2)
				.AddCellToTable("взрослые", font, 5, 1, 2)
				.AddCellToTable("дети", font, 5, 1, 2)
				.AddCellToTable("всего", font, 5, 1, 2)
				.AddCellToTable("Для взрослых", font)
				.AddCellToTable("Для детей", font)
				.AddCellToTable("Для взрослых", font)
				.AddCellToTable("Для детей", font);
			var title = new[]
			{
				"без возвратные потери", "поражения тяжелой степени", "поражения средней тяжести", "поражения легкой тяжести",
				"Пороговые воздействия"
			};
			for (int i=0; i<title.Length; i++)
			{
				table.AddCell(new Phrase(title[i], font));
				table.AddCellToTable(model.Gau[i, 0].ToPrettyDouble(), font)
					.AddCellToTable(model.Gau[i, 1].ToPrettyDouble(), font)
					.AddCellToTable(model.Sau[i, 0].ToPrettyDouble(), font)
					.AddCellToTable(model.Sau[i, 1].ToPrettyDouble(), font)
					.AddCellToTable(model.Nfs[i, 0].ToPrettyDouble(), font)
					.AddCellToTable(model.Nfs[i, 1].ToPrettyDouble(), font)
					.AddCellToTable(model.NfAll[i, 0].ToPrettyDouble(), font);
			}
			doc.Add(table);
		}
	}
}