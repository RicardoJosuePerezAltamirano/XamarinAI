using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppAISpeech
{
    public interface IFileOperations
    {
        void Save(string fileName,MemoryStream data);
    }
}
