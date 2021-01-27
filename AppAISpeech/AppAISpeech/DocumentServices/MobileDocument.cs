using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AppAISpeech.DocumentServices
{
    public class MobileDocument
    {
        private string AppFolder =>
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public MobileDocument()
        {

        }
        public void Create(string DataToSaveInWord)
        {

            using(var stream=new MemoryStream())
            {
                using(var wordDocument=WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document,true))
                {
                    wordDocument.AddMainDocumentPart();
                    var document = new Document();
                    var body = new Body();
                    var palabra = new Paragraph();
                    var propiedadesPalabra = new ParagraphProperties();
                    var PalabraEstilo = new ParagraphStyleId() { Val = "Normal" };
                    var Justification = new Justification() { Val = JustificationValues.Left };
                    propiedadesPalabra.Append(PalabraEstilo);
                    propiedadesPalabra.Append(Justification);
                    var run = new Run();
                    var text = new Text(DataToSaveInWord);
                    run.Append(text);
                    palabra.Append(propiedadesPalabra);
                    palabra.Append(run);
                    body.Append(palabra);
                    document.Append(body);
                    wordDocument.MainDocumentPart.Document = document;
                    wordDocument.Close();

                    //wordDocument.SaveAs(fileparth);
                    //var fileparth = Android.OS.Environment.ExternalStorageDirectory.Path
                    //Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, $"{DateTime.Now.ToString("yyyy-MM-dd")}Prueba.docx");

                    //File.WriteAllBytes(fileparth, stream.ToArray());
                    DependencyService.Get<IFileOperations>().Save($"{DateTime.Now.ToString("yyyy-MM-dd:hh:mm:ss")}Prueba.docx", stream);
                    
                }
                
            }
            
            
        }
    }
}
