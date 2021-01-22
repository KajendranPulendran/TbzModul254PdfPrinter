using System.Collections.Generic;

namespace TBZ.Modul._254.PrintApi
{
	public class PrintDocumentModel
	{
		public string PrinterName { get; set; }
		public string PrinterTray { get; set; }
		public byte[] PrinterDocument { get; set; }
		public string PaperSize { get; set; }
		public bool IsLandscape { get; set; }
		public bool IsColor { get; set; }
	}
}
