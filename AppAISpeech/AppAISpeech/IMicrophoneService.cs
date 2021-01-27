using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAISpeech
{
    public interface IMicrophoneService
    {
        Task<bool> GetPermission();
        
        void OnRequestPermissionsResult(bool isGranted);
    }
}
