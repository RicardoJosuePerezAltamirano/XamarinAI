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
        SpeechRecognizer recognizer;
        string textoFinal = "";
        public bool IsEnabledRecord { get; set; }
        public MainPage()
        {
            InitializeComponent();
            DocumentService = new MobileDocument();
            sppechConfig = SpeechConfig.FromEndpoint(new Uri("https://southcentralus.api.cognitive.microsoft.com/sts/v1.0/issuetoken"), "");
            sppechConfig.SpeechRecognitionLanguage = "es-MX";
            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            recognizer = new SpeechRecognizer(sppechConfig, audioConfig);
            recognizer.Recognizing += Recognizer_Recognizing;
            recognizer.Recognized += Recognizer_Recognized;
            recognizer.Canceled += Recognizer_Canceled;
            IsEnabledRecord = true;
            BindingContext = this;



        }

        private void Recognizer_Canceled(object sender, SpeechRecognitionCanceledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Recognizer_Recognized(object sender, SpeechRecognitionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async delegate
            {
                if(e.Result.Reason== ResultReason.RecognizedSpeech)
                {
                    textoFinal += e.Result.Text;
                    
                }
                

            });
        }

        private void Recognizer_Recognizing(object sender, SpeechRecognitionEventArgs e)
        {
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(FileName.Text))
            {
                await DisplayAlert("Aviso", "Recuerde que debe escribir el nombre del archivo para diferenciarlo","Ok");
            }
            else
            {
                var permision = await DependencyService.Get<IMicrophoneService>().GetPermission();
                if (permision)
                {
                    //sppechConfig = SpeechConfig.FromEndpoint(new Uri("https://southcentralus.api.cognitive.microsoft.com/sts/v1.0/issuetoken"), "");
                    //sppechConfig.SpeechRecognitionLanguage = "es-MX";
                    //var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
                    //using (var recognizer = new SpeechRecognizer(sppechConfig, audioConfig))
                    //{
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Grabar.IsEnabled = false;
                        TextoResult.Text = "Habla ahora";
                    });

                    //var resultTest = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);
                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                    //Device.BeginInvokeOnMainThread(async  delegate
                    //{
                    //    TextoResult.Text = $"{resultTest}";
                    //    DocumentService.Create(resultTest.Text);
                    //    await DisplayAlert("Aviso", "Documento guardado", "Ok");

                    //});


                    //TextoResult.Text = "Habla ahora";
                    //var resultTest = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);
                    //TextoResult.Text = $"{resultTest}";
                    // }

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


        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (Grabar.IsEnabled == false)
            {
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                var path = DocumentService.Create(textoFinal, FileName.Text);

                Device.BeginInvokeOnMainThread(async () =>
                {


                    TextoResult.Text = $"Reconocimiento terminado";
                    await DisplayAlert("Aviso", "Documento guardado", "Ok");
                    if (!string.IsNullOrEmpty(path))
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            File = new ShareFile(path),
                            Title = "Documento Terminado"
                        });
                    }
                    Grabar.IsEnabled = true;
                });
            }
            
            
        }
    }
}
