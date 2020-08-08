using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using Wooting;

namespace MuteButtonWooting
{
    class Program
    {
        private static readonly string DEVICE = @"""Focusrite Usb Audio\Device\Microphone\Capture""";
        private static readonly string MICROPHONE = @"{0.0.1.00000000}.{fcfae21b-582c-4a5c-8fc7-7b99cc5e76be}";

        static void Main(string[] args)
        {
            try
            {
                using (var MMDE = new MMDeviceEnumerator())
                {
                    using (var microphone = MMDE.GetDevice(MICROPHONE))
                    {
                        microphone.AudioEndpointVolume.Mute = !microphone.AudioEndpointVolume.Mute;
                        WootingHelper.UpdateButtonToCurrentState(microphone.AudioEndpointVolume.Mute);
                    }
                }

               
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}