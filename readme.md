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

## Тут виндовс службой сделано

И **важно** установить

```LoadUserProfile = true,```

в *Pdf24Wrapper*.cs

Службу запускаю под своей учеткой.

Вообще со службой над PDF24 возникли проблемы.

PDF24 хранит данные и свои профили в реестре виндовс. А виндовс-служба не имеет доступа в эту ветку реестра.

HKEY_CURRENT_USER (HKCU): A service **should not** access HKEY_CURRENT_USER or HKEY_CLASSES_ROOT directly, especially when not impersonating a specific interactive user. HKCU maps to the registry hive of the user account currently associated with the service's process, which is often a non-interactive service account (like LocalService) and not the user currently logged into the desktop. Attempting to use HKCU from a service may lead to unexpected behavior or failures. 
