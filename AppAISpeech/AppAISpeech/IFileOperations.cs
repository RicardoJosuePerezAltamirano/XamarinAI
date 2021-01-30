using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppAISpeech
{
    public interface IFileOperations
    {
        string Save(string fileName,MemoryStream data);
    }
}
