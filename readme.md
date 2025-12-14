# Docx to PDF и добавить факсимиле на бесплатных библиотеках

## Ссылки

### PDF24

* https://tools.pdf24.org/ru/creator
* https://tools.pdf24.org/ru/
* https://creator.pdf24.org/manual/11/#1114-apply-a-profile-to-input-files
* https://creator.pdf24.org/manual/11/#11110-convert-to-pdf

### LibreOffice

* https://www.libreoffice.org/download/download-libreoffice/

***Это независимые приложения, можно нагрузить любое***

## Применяю такие аргументы 

``` java
var t4 = ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa4.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");
```

чтобы превратить docx в pdf, через PDF24

# Вставить факсимиле на пласехолдер

## Ссылки

* dotnet add package **PdfPig**
* dotnet add package **PDFsharp**

Первая умеет искать координыты теста в pdf, вторая умеет вставить картинку.

```` java
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
````

В ветке **feature/windows-service** сделал виндовс службой