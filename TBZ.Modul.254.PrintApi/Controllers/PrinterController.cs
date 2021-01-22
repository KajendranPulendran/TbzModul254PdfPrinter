using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace TBZ.Modul._254.PrintApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PrinterController : Controller
	{
		public JsonResult Index()
		{
			List<PrinterModel> printerConfig = new List<PrinterModel>();

			foreach (string printer in PrinterSettings.InstalledPrinters)
			{
				PrinterSettings printerSettings = new PrinterSettings();
				List<string> printerTrays = new List<string>();
				printerSettings.PrinterName = printer;

				for (int i = 0; i < printerSettings.PaperSources.Count; i++)
				{
					printerTrays.Add(printerSettings.PaperSources[i].SourceName);
				}

				printerConfig.Add(new PrinterModel { PrinterName = printer, PrinterTray = printerTrays });
			};

			return Json(new { localprinters = printerConfig });
		}
	}
}
