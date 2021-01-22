using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Opten.Condent.App.Web.Controllers
{
	internal class PdfRasterizer
	{
		public int _desiredXDpi;
		public int _desiredYDpi;
		public GhostscriptVersionInfo _lastInstalledVersion;
		public GhostscriptRasterizer _rasterizer;
	

		public PdfRasterizer(int xDpi = 720, int yDpi = 720, string paperSize = "a4")
		{
			_desiredXDpi = xDpi;
			_desiredYDpi = yDpi;
			string platform = IntPtr.Size == 4 ? "gsdll32.dll" : "gsdll64.dll";

			var test = Directory.GetFiles("./");
			var path = Directory.GetCurrentDirectory();
			var dllPath = Path.Combine(path, platform);

			if (File.Exists(dllPath))
			{
				// use DLL in bin folder
				_lastInstalledVersion = new GhostscriptVersionInfo(new System.Version(0, 0, 0), dllPath, string.Empty, GhostscriptLicense.GPL | GhostscriptLicense.AFPL);
			}
			else
			{
				// try to use installed DLL
				_lastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);
			}

			_rasterizer = new GhostscriptRasterizer();
			//_rasterizer.CustomSwitches.Add($"-sPAPERSIZE#{paperSize.ToLower()}");
		}

		public Image Rasterize(string pdfUri, int pageNumber = 1)
		{
			_rasterizer.Open(pdfUri, _lastInstalledVersion, true);

			var img = _rasterizer.GetPage(_desiredXDpi, _desiredYDpi, pageNumber);

			_rasterizer.Close();
			return img;
		}

		public Image Rasterize(Stream pdfStream, int pageNumber = 1)
		{
			_rasterizer.Open(pdfStream, _lastInstalledVersion, true);

			var img = _rasterizer.GetPage(_desiredXDpi, _desiredYDpi, pageNumber);

			_rasterizer.Close();
			return img;
		}

		public List<Image> RasterizeAll(string pdfUri)
		{
			var images = new List<Image>();
			_rasterizer.Open(pdfUri, _lastInstalledVersion, true);
			for (int pageNumber = 1; pageNumber <= _rasterizer.PageCount; pageNumber++)
			{
				images.Add(_rasterizer.GetPage(_desiredXDpi, _desiredYDpi, pageNumber));
			}
			_rasterizer.Close();
			return images;
		}

		public List<Image> RasterizeAll(Stream pdfStream)
		{
			var images = new List<Image>();
			_rasterizer.Open(pdfStream, _lastInstalledVersion, true);
			for (int pageNumber = 1; pageNumber <= _rasterizer.PageCount; pageNumber++)
			{
				images.Add(_rasterizer.GetPage(_desiredXDpi, _desiredYDpi, pageNumber));
			}
			_rasterizer.Close();
			return images;
		}

		private string GetBinPath()
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;

			UriBuilder uri = new UriBuilder(codeBase);

			string path = Uri.UnescapeDataString(uri.Path);

			return Path.GetDirectoryName(path);
		}
	}
}
