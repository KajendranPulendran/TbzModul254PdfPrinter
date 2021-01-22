using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBZ.Modul._254.PrintApi.Models
{
	public class PdfModel
	{
		public string PrinterName { get; set; }
		public string PrinterTray { get; set; }
		public string PaperSize { get; set; }
		public bool IsLandscape { get; set; }
		public bool IsColor { get; set; }
		public string Sex { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Street { get; set; }
		public string StreetNr { get; set; }
		public string ZipCode { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string Photo { get; set; }
		public string PhoneNumber { get; set; }
		public string BirthdayDate { get; set; }
		public string Occupation { get; set; }
		public string Description { get; set; }
	}
}
