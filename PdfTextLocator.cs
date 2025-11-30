using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;

public class PdfTextLocator
{
    public class TextPosition
    {
        public string Text { get; set; }
        public int PageNumber { get; set; }
        public PdfRectangle Bounds { get; set; }
        public double X => Bounds.Left;
        public double Y => Bounds.Bottom;
        public double YInvert { get; set; }
    }

    public static List<TextPosition> FindText(byte[] pdfBytes, string searchText = "[FACSIMILE_HERE]")
    {
        var foundPositions = new List<TextPosition>();

        using (PdfDocument document = PdfDocument.Open(pdfBytes))
        {
            FindText(searchText, foundPositions, document);
        }

        return foundPositions;
    }

    public static List<TextPosition> FindText(MemoryStream stream, string searchText = "[FACSIMILE_HERE]")
    {
        var foundPositions = new List<TextPosition>();

        using (PdfDocument document = PdfDocument.Open(stream))
        {
            FindText(searchText, foundPositions, document);
        }

        return foundPositions;
    }

    private static void FindText(string searchText, List<TextPosition> foundPositions, PdfDocument document)
    {
        // Номера страниц в PdfPig начинаются с 1
        for (int i = 1; i <= document.NumberOfPages; i++)
        {
            Page page = document.GetPage(i);

            IEnumerable<Word> words = page.GetWords();

            foreach (Word word in words)
            {
                if (word.Text.Equals(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    foundPositions.Add(new TextPosition
                    {
                        Text = word.Text,
                        PageNumber = i - 1,
                        Bounds = word.BoundingBox,
                        YInvert = (double)(page.Height - word.BoundingBox.Bottom - word.BoundingBox.Height)
                    });
                }
            }
        }
    }
}