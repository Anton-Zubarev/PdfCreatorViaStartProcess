# Ссылки

## PDF24

* https://tools.pdf24.org/ru/creator
* https://tools.pdf24.org/ru/
* https://creator.pdf24.org/manual/11/#1114-apply-a-profile-to-input-files
* https://creator.pdf24.org/manual/11/#11110-convert-to-pdf

## LibreOffice

* https://www.libreoffice.org/download/download-libreoffice/

# Применяю такие аргументы 

``` java
var t4 = ProgramPdf24Wrapper.RunProcessAsync(@"c:\Program Files\PDF24\pdf24-DocTool.exe", "-convertToPDF -noProgress -profile default/good -outputFile c:\\Users\\a_zub\\Downloads\\anketa4.pdf c:\\Users\\a_zub\\Downloads\\анкета.docx");
```

чтобы превратить docx в pdf
