using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Threading.Tasks;

namespace ConsoleAISpeech
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sppechConfig = SpeechConfig.FromEndpoint(new Uri("https://southcentralus.api.cognitive.microsoft.com/sts/v1.0/issuetoken"), "f943293eeeeb442b965c469afb3155f9");
            sppechConfig.SpeechRecognitionLanguage = "es-MX";
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var recognizer = new SpeechRecognizer(sppechConfig, audioConfig);
            Console.WriteLine("habla ahora");
            
            var result = await recognizer.RecognizeOnceAsync();
            Console.WriteLine($"el texto es {result}");

        }
    }
}
