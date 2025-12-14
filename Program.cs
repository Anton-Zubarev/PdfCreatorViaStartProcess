// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Pdf24WrapperConsole;

Console.WriteLine("План такой:");
Console.WriteLine("Превратить docx в pdf");
Console.WriteLine("Найти в Pdf текстовый пласехолдер");
Console.WriteLine("Вставить картинку на место пласехолдер");
Console.WriteLine("");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Pdf24TestService";
});

builder.Services.AddHostedService<Pdf24TestService>();

var host = builder.Build();
host.Run();




/*await Main([""]);

static async Task Main(string[] args)
{
    
}*/

partial class Pdf24TestService : BackgroundService
{
    public Pdf24TestService()
    {
        //InitializeComponent();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            //Process process = Process.Start("notepad");

            var ff = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa1.txt");
            ff.WriteLine("111");
            ff.Close();

            int exitCode1 = ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa1.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx")
                .Result;

            var ff2 = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa2.txt");
            ff2.WriteLine(exitCode1);
            ff2.Close();

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Console.WriteLine($"Процесс 1 завершился с кодом: {exitCode1}");

            if (exitCode1!=0) throw new Exception("Ошибка pdf24-DocTool.exe")"


            var pdfBytes = File.ReadAllBytes("c:\\Users\\a_zub\\Downloads\\anketa1.pdf");
            ff2 = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa2.txt");
            ff2.WriteLine("read");
            ff2.Close();
            var textPositions = PdfTextLocator.FindText(pdfBytes);
            ff2 = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa2.txt");
            ff2.WriteLine("textlocator " + textPositions.Count);
            ff2.Close();
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            var faximileLocations = textPositions.Select(x => x.ToFacsimileLocator());

            Console.WriteLine("Координаты текста найдены");

            var faximilePngBytes = File.ReadAllBytes("c:\\Users\\a_zub\\Downloads\\carcade-logo.png");

            ff2 = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa2.txt");
            ff2.WriteLine("fax read");
            ff2.Close();

            var newPdf = PdfFacsimileAdder.AddFacsimile(
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                new MemoryStream(pdfBytes),
                new MemoryStream(faximilePngBytes),
                faximileLocations
            );

            ff2 = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa2.txt");
            ff2.WriteLine("fax ins");
            ff2.Close();

            Console.WriteLine("Факсимиле добавлен в pdf");

            File.WriteAllBytes("c:\\Users\\a_zub\\Downloads\\anketa2new.pdf", newPdf.ToArray());

            ff2 = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa2.txt");
            ff2.WriteLine("2 file wrote");
            ff2.Close();

            Console.WriteLine("Сохранено в файл");
            
        }
        catch (Exception ex)
        {
            var ff2 = File.CreateText("c:\\Users\\a_zub\\Downloads\\anketa4.txt");
            ff2.WriteLine(ex.Message);
            ff2.WriteLine(ex.StackTrace);

            ff2.Close();
        }
        return Task.CompletedTask;
    }

}