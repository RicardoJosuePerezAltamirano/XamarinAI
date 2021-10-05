using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;

namespace AppAISpeech.Droid
{
    [Activity(Label = "AppAISpeech", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        private IMicrophoneService microphoneService;
        private const int RECORD_AUDIO = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Xamarin.Forms.DependencyService.Register<IMicrophoneService, MicrophoneService>();
            microphoneService = Xamarin.Forms.DependencyService.Get<IMicrophoneService>();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch(requestCode)
            {
                case RECORD_AUDIO:
                {
                    if(grantResults[0]==Permission.Granted)
                    {
                        microphoneService.OnRequestPermissionsResult(true);
                    }
                        else
                        {
                            microphoneService.OnRequestPermissionsResult(false);
                        }
                }
                
                    break;
                
            }
            ChechStoragePermissions();

        }
        private void ChechStoragePermissions()
        {
            if((int)Build.VERSION.SdkInt<23)
            {
                return;
            }
            else
            {
                if(PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage,PackageName) != Permission.Granted
                    && 
                    PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage,PackageName) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                    RequestPermissions(permissions, 1);
                }
            }
        }
    }
}