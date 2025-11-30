// See https://aka.ms/new-console-template for more information

using Pdf24WrapperConsole;

Console.WriteLine("План такой:");
Console.WriteLine("Превратить docx в pdf");
Console.WriteLine("Найти в Pdf текстовый пласехолдер");
Console.WriteLine("Вставить картинку на место пласехолдер");
Console.WriteLine("");

await Main(new string[] { "" });

static async Task Main(string[] args)
{
    int exitCode1 = await ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa1.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Console.WriteLine($"Процесс 1 завершился с кодом: {exitCode1}");

    
    var pdfBytes = File.ReadAllBytes("c:\\Users\\a_zub\\Downloads\\anketa1.pdf");

    var textPositions = PdfTextLocator.FindText(pdfBytes);
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    var faximileLocations = textPositions.Select(x => x.ToFacsimileLocator());

    Console.WriteLine("Координаты текста найдены");

    var faximilePngBytes = File.ReadAllBytes("c:\\Users\\a_zub\\Downloads\\carcade-logo.png");

    var newPdf = PdfFacsimileAdder.AddFacsimile(
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        new MemoryStream(pdfBytes),
        new MemoryStream(faximilePngBytes),
        faximileLocations
    );

    Console.WriteLine("Факсимиле добавлен в pdf");

    File.WriteAllBytes("c:\\Users\\a_zub\\Downloads\\anketa2new.pdf", newPdf.ToArray());

    Console.WriteLine("Сохранено в файл");
}