using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using TBZ.Modul._254.PrintApi.Models;

namespace TBZ.Modul._254.PrintApi.Controllers
{
	public class PdfGeneratorController : Controller
	{
		[HttpPost]
		[Produces("application/json")]
		[Route("pdf")]
		public string Index([FromBody] PdfModel pdfModel)
		{

			PdfDocument pdf = new PdfDocument();
			PdfPage pdfPage = pdf.AddPage();

			XFont fontTitle = new XFont("Arial", 0.7055555555556, XFontStyle.Bold);
			XFont fontText = new XFont("Arial", 0.388056, XFontStyle.Regular);

			pdfPage.Size = PdfSharpCore.PageSize.A4;
			pdfPage.TrimMargins.Top = 2.5;
			pdfPage.TrimMargins.Left = 2.5;
			pdfPage.TrimMargins.Right = 2.5;
			pdfPage.TrimMargins.Bottom = 2;

			XGraphics graph = XGraphics.FromPdfPage(pdfPage, XGraphicsUnit.Centimeter);
			XTextFormatter textFormatter = new XTextFormatter(graph);
			double pdfWidth = pdfPage.Width.Centimeter - 5;
			double pdfHeight = pdfPage.Height.Centimeter - 4.5;

			XRect rect = new XRect(0, 0, pdfWidth, pdfHeight);

			XImage xImage = XImage.FromStream(() => new MemoryStream(Convert.FromBase64String(pdfModel.Photo)));

			double ySpace = 0;
			graph.DrawImage(xImage, new XRect((pdfWidth - (pdfWidth * 0.2f)), 0, (pdfWidth * 0.23f), (pdfHeight * 0.2f)));


			textFormatter.DrawString("Registration Bestätigung", fontTitle, XBrushes.Black, new XRect(0, 0, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 1.5;
			textFormatter.DrawString(pdfModel.Sex, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 0.5;
			textFormatter.DrawString(pdfModel.Firstname + " " + pdfModel.Lastname, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 0.5;
			textFormatter.DrawString(pdfModel.Street + " " + pdfModel.StreetNr, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 0.5;
			textFormatter.DrawString(pdfModel.ZipCode + " " + pdfModel.City, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 0.5;
			textFormatter.DrawString(pdfModel.Country, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 1;
			textFormatter.DrawString("Telefonnummer: " + pdfModel.PhoneNumber, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 0.5;
			textFormatter.DrawString("Geburtsdatum: " + pdfModel.BirthdayDate, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 0.5;
			textFormatter.DrawString("Beruf: " + pdfModel.Occupation, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);
			ySpace += 1;
			textFormatter.DrawString("Bemerkung: \n" + pdfModel.Description, fontText, XBrushes.Black, new XRect(0, ySpace, pdfWidth, pdfHeight), XStringFormats.TopLeft);


			//pdf.Save(@".\Temp\test.pdf");
			MemoryStream stream = new MemoryStream();

			pdf.Save(stream, false);
			byte[] bytes = stream.ToArray();

			PrintDocumentController printDocument = new PrintDocumentController();

			PrintDocumentModel printDocumentModel = new PrintDocumentModel();
			printDocumentModel.PrinterName = pdfModel.PrinterName;
			printDocumentModel.PrinterTray = pdfModel.PrinterTray;
			printDocumentModel.PaperSize = pdfModel.PaperSize;
			printDocumentModel.IsLandscape = pdfModel.IsLandscape;
			printDocumentModel.IsColor = pdfModel.IsColor;
			printDocumentModel.PrinterDocument = bytes;

			printDocument.Index(printDocumentModel);
			pdf.Clone();
			return "Done";
		}
	}
}
