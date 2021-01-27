using AppAISpeech.DocumentServices;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppAISpeech
{
    public partial class MainPage : ContentPage
    {
        SpeechConfig sppechConfig;
        MobileDocument DocumentService;
        public MainPage()
        {
            InitializeComponent();
            DocumentService = new MobileDocument();
          
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var permision=await DependencyService.Get<IMicrophoneService>().GetPermission();
            if(permision)
            {
                sppechConfig = SpeechConfig.FromEndpoint(new Uri("https://southcentralus.api.cognitive.microsoft.com/sts/v1.0/issuetoken"), "f943293eeeeb442b965c469afb3155f9");
                sppechConfig.SpeechRecognitionLanguage = "es-MX";
                var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
                using (var recognizer = new SpeechRecognizer(sppechConfig, audioConfig))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TextoResult.Text = "Habla ahora";
                    });

                    var resultTest = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);
                    Device.BeginInvokeOnMainThread(async  delegate
                    {
                        TextoResult.Text = $"{resultTest}";
                        DocumentService.Create(resultTest.Text);
                        await DisplayAlert("Aviso", "Documento guardado", "Ok");

                    });


                    //TextoResult.Text = "Habla ahora";
                    //var resultTest = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);
                    //TextoResult.Text = $"{resultTest}";
                }

            }
            else
            {
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    TextoResult.Text = $"no tiene permisos de microfono";
                });
            }
            
                
        }
    }
}
