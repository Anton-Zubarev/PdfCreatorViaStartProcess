using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
