using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AppAISpeech.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileOperations))]
namespace AppAISpeech.Droid
{
    
    public class FileOperations : IFileOperations
    {
        public string Save(string fileName, MemoryStream data)
        {
            //https://gist.github.com/lopspower/76421751b21594c69eb2
            string path =Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
               // Android.OS.Environment.ExternalStorageDirectory.Path; 
            //Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath;
            string fullPath = Path.Combine(path, fileName);
            if(!File.Exists(fullPath))
            File.WriteAllBytes(fullPath, data.ToArray());
            return fullPath;


        }
    }
}