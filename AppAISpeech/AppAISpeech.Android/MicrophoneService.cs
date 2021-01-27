using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Google.Android.Material.Snackbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAISpeech.Droid
{
    public class MicrophoneService : IMicrophoneService
    {
        public const int REQUEST_MIC = 1;
        private string[] permissions = { Manifest.Permission.RecordAudio };
        private TaskCompletionSource<bool> tcsPermissions;
        public Task<bool> GetPermission()
        {
            tcsPermissions = new TaskCompletionSource<bool>();
            if ((int)Build.VERSION.SdkInt<23)
            {
                tcsPermissions.TrySetResult(true);
            }
            else
            {
                var currentActivity = MainActivity.Instance;
                if(ActivityCompat.CheckSelfPermission(currentActivity,Manifest.Permission.RecordAudio) != (int)Android.Content.PM.Permission.Granted)
                {
                    if(ActivityCompat.ShouldShowRequestPermissionRationale(currentActivity,Manifest.Permission.RecordAudio))
                    {
                        Snackbar.Make(currentActivity.FindViewById(Android.Resource.Id.Content), "Se requeren permisos de microfono", Snackbar.LengthIndefinite).SetAction("Ok", v =>
                         {
                             ((Activity)currentActivity).RequestPermissions(permissions, REQUEST_MIC);
                         }).Show();
                    }
                    else
                    {
                        ActivityCompat.RequestPermissions((Activity)currentActivity,permissions, REQUEST_MIC);
                    }

                }
                else
                {
                    tcsPermissions.TrySetResult(true);
                }
            }
            return tcsPermissions.Task;
        }
        public void OnRequestPermissionsResult(bool isGranted)
        {
            tcsPermissions.TrySetResult(isGranted);
        }
    }
}