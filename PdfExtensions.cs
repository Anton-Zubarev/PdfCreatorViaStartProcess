namespace Pdf24WrapperConsole
{
    public static class PdfExtensions
    {
        public static PdfFacsimileAdder.FacsimileLocator ToFacsimileLocator(this PdfTextLocator.TextPosition tp)
        {
            return new PdfFacsimileAdder.FacsimileLocator
            {
                PageNumber = tp.PageNumber,
                X = tp.X,
                Y = tp.YInvert
            };
        }
    }
}
