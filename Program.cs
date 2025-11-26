// See https://aka.ms/new-console-template for more information

await Main(new string[] { "" });

Console.WriteLine("Hello, World!");

static async Task Main(string[] args)
{
    var t1 = ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa1.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");
    var t2 = ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa2.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");
    var t3 = ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa3.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");
    var t4 = ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa4.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");

    await Task.WhenAll(new List<Task> { t1, t2, t3, t4 });

    //int exitCode1 = await ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa1.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");
    //int exitCode2 = await ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa2.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");


    Console.WriteLine($"Процесс 1 завершился с кодом: {t1.Result}");
    Console.WriteLine($"Процесс 2 завершился с кодом: {t2.Result}");
}