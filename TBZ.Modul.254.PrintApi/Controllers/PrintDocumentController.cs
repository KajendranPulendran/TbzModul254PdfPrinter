using Microsoft.AspNetCore.Mvc;
using Opten.Condent.App.Web.Controllers;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace TBZ.Modul._254.PrintApi.Controllers
{
	public class PrintDocumentController : Controller
	{
		public bool isLandscape;
		public bool isColor;
		public Image convertedBmp;

		[HttpPost]
		[Produces("application/json")]
		[Route("printout")]
		public JsonResult Index([FromBody] PrintDocumentModel printDocumentModel)
		{
			isLandscape = printDocumentModel.IsLandscape;
			isColor = printDocumentModel.IsColor;
			LoadPdfFromBase(printDocumentModel.PrinterDocument, printDocumentModel.PrinterName, printDocumentModel.PrinterTray, printDocumentModel.PaperSize);

			return Json(new { printDocumentModel.PrinterName });
		}
		public void LoadPdfFromBase(byte[] base64BinaryStr, string printerName, string printerTray, string paperSize)
		{
			string tempDirectory = Environment.GetEnvironmentVariable("LocalAppData") + "/001/";

			Stream streamer = new MemoryStream();
			streamer.Write(base64BinaryStr);

			PdfRasterizer pdfRasterizer = new PdfRasterizer(920, 920, paperSize);
			var tiffs = pdfRasterizer.Rasterize(streamer);
			convertedBmp = tiffs;

			if (!Directory.Exists(tempDirectory))
			{
				Directory.CreateDirectory(tempDirectory);
			}

			convertedBmp.Save(tempDirectory + "test1.tiff");

			PrintPdf("printJobName", printerName, GetPrinterTrayIdByTrayName(printerName, printerTray), paperSize);
		}

		public static int GetPrinterTrayIdByTrayName(string printerName, string trayName)
		{
			int returnValue = 0;
			PrintDocument printDoc = new PrintDocument();

			printDoc.PrinterSettings.PrinterName = printerName;

			for (int i = 0; i < printDoc.PrinterSettings.PaperSources.Count; i++)
			{
				if (printDoc.PrinterSettings.PaperSources[i].SourceName == trayName)
				{
					returnValue = i;
				}
			}

			return returnValue;
		}

		public void PrintPdf(string printJobName, string printQueueName, int paperTray, string paperSizeToPrint)
		{

			Margins margin = new Margins();
			margin.Top = 0;
			margin.Left = 0;
			margin.Right = 0;
			margin.Bottom = 0;

			PrinterResolution printerResolution = new PrinterResolution();
			printerResolution.X = 720;
			printerResolution.Y = 720;

			PrintDocument document = new PrintDocument();
			PaperSize paperSize = document.PrinterSettings.PaperSizes.Cast<PaperSize>().FirstOrDefault(e => e.PaperName.StartsWith(paperSizeToPrint));
			document.DocumentName = printJobName;
			document.PrinterSettings.PrintFileName = printJobName;
			document.PrinterSettings.PrinterName = printQueueName;
			document.DefaultPageSettings.PaperSource = document.PrinterSettings.PaperSources[paperTray];
			document.DefaultPageSettings.PaperSize = paperSize;
			document.OriginAtMargins = false;
			document.PrinterSettings.Copies = 1;
			document.DefaultPageSettings.Color = isColor;
			document.DefaultPageSettings.PrinterResolution = printerResolution;
			document.DefaultPageSettings.Landscape = isLandscape;

			document.PrintPage += new PrintPageEventHandler(document_PrintPageToA4ForA4);

			document.Print();
		}

		private void document_PrintPageToA4ForA4(object sender, PrintPageEventArgs args)
		{
			RectangleF srcRect = new RectangleF();

			srcRect.Width = args.PageSettings.PrintableArea.Width;
			srcRect.Height = args.PageSettings.PrintableArea.Height;
			srcRect.Y = 0;
			srcRect.X = 0;

			args.Graphics.InterpolationMode = InterpolationMode.High;
			args.Graphics.CompositingQuality = CompositingQuality.HighQuality;
			args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			args.Graphics.DrawImage(convertedBmp, srcRect);

			args.HasMorePages = false;
		}

	}
}
