using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using Wooting;

namespace MuteButtonWooting
{
    class MuteButtonHandler
    {
        private static readonly string GET_MUTE_COMMAND = "/GetMute";
        private static readonly string TOGGLE_COMMAND= "/Switch";
        public bool IsRgbConnected => RGBControl.IsConnected();

        private string Device { get;  }

        public MuteButtonHandler(string device)
        {
            Device = device;
        }

        public void ToggleMute()
        {
            if (IsRgbConnected) {
                ToggleMicrophoneMute();
                UpdateButtonToCurrentState();
            }
        }

        public void UpdateButtonToCurrentState()
        {
            if (IsRgbConnected)
            {
                if (IsMuted)
                {
                    TurnModeButtonRed();
                }
                else
                {
                    TurnModeButtonNormal();
                }
            }

        }

        private void TurnModeButtonRed()
        {
            RGBControl.SetKey(WootingKey.Keys.Mode_ScrollLock, 255, 0, 0, true);
        }

        private void TurnModeButtonNormal()
        {
            RGBControl.ResetKey(WootingKey.Keys.Mode_ScrollLock);
        }

        private bool ToggleMicrophoneMute()
        {
            return ExecuteSoundVolumeView(TOGGLE_COMMAND) == 1;
        }

        private bool IsMuted => ExecuteSoundVolumeView(GET_MUTE_COMMAND) == 1;

        private int? ExecuteSoundVolumeView(string command)
        {
            using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
            {
                pProcess.StartInfo.FileName = @"SoundVolumeView.exe";
                pProcess.StartInfo.Arguments = $"{command} {Device}"; //argument
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                pProcess.StartInfo.CreateNoWindow = true; //not diplay a windows
                pProcess.Start();
                string output = pProcess.StandardOutput.ReadToEnd(); //The output result
                pProcess.WaitForExit();
                return pProcess.ExitCode;
            }
        }
    }
}
