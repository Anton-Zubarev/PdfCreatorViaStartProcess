using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Drawing;
using System.IO;

public class PdfFacsimileAdder
{

    public class FacsimileLocator
    {
        public double X;
        public double Y;
        public int PageNumber;
    }

    public static MemoryStream AddFacsimile(Stream pdfStream, Stream pngStream, IEnumerable<FacsimileLocator> facsimileLocator, Size? size = null)
    {
        using (PdfDocument document = PdfReader.Open(pdfStream))
        {
            foreach (var loc in facsimileLocator)
            {
                PdfPage page = document.Pages[loc.PageNumber];
                {
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    XImage image = XImage.FromStream(pngStream);

                    // gfx.DrawRectangle(XBrushes.White, xCoord, yCoord, boundsWidth, boundsHeight);

                    if (size.HasValue)
                    {
                        gfx.DrawImage(image, loc.X, loc.Y, size.Value.Width, size.Value.Height);
                    }
                    else
                    {
                        gfx.DrawImage(image, loc.X, loc.Y);
                    }
                }
            }

            var memStream = new MemoryStream();
            document.Save(memStream, true);

            document.Close();

            return memStream;
        }
    }
}