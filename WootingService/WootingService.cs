using NAudio.CoreAudioApi;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using WootingCommon;

namespace WootingService
{
    public partial class WootingService : ServiceBase
    {
        
        private static readonly string MICROPHONE_ID = @"{0.0.1.00000000}.{fcfae21b-582c-4a5c-8fc7-7b99cc5e76be}";
        private int eventId = 1;

        private bool IsConnected = false;
        private MMDevice Microphone;
        public WootingService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("WootingService"))
            {
                System.Diagnostics.EventLog.CreateEventSource("WootingService", "WootingLog");
            }
            eventLog1.Source = "WootingService";
            eventLog1.Log = "WootingLog";
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
            try
            {
                var com = (WootingCommands)command;
                if (com == WootingCommands.UpdateButtonState)
                {
                    try
                    {
                        if (IsConnected)
                        {
                            WootingHelper.UpdateButtonToCurrentState(IsMute());
                        }
                    } catch (Exception ex)
                    {
                        eventLog1.WriteEntry("Update button failed: " + ex.Message, EventLogEntryType.Error, eventId++);
                    }
                } else if (com == WootingCommands.Toggle)
                {
                    try
                    {
                        ToggleMicrophone();
                        if (IsConnected)
                        {
                            WootingHelper.UpdateButtonToCurrentState(IsMute());
                        }
                    } catch (Exception ex)
                    {
                        eventLog1.WriteEntry("Unable to Update Button " + ex.Message, EventLogEntryType.Error, eventId++);
                    }
                }

            } catch (Exception ex)
            {
                eventLog1.WriteEntry("Error: "+ ex.Message, EventLogEntryType.Error, eventId++);
            }
        }

        protected override void OnStart(string[] args)

        {

            TryConnect();
            // Set up a timer that triggers every minute.
            Timer timer = new Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            //TryConnect();
        }

        private MMDevice GetMicrophone()
        {
            try
            {
                using (var MMDE = new MMDeviceEnumerator())
                {
                    return MMDE.GetDevice(MICROPHONE_ID);
                }
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("Could not find microphone " + ex.Message, EventLogEntryType.Error, eventId++);
            }
            return null;
        }

        private void ToggleMicrophone()
        {
            Microphone.AudioEndpointVolume.Mute = !Microphone.AudioEndpointVolume.Mute;
        }

        private bool IsMute()
        {
            return Microphone.AudioEndpointVolume.Mute;
        }

        private void TryConnect()
        {
            try
            {
                IsConnected = WootingHelper.IsRgbConnected;
                
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("Not connected to keyboard: " + ex.Message, EventLogEntryType.Error, eventId++);

            }
            try
            {
                Microphone = GetMicrophone();
                eventLog1.WriteEntry("Got Microphone " + Microphone.FriendlyName, EventLogEntryType.Information, eventId++);
            } catch (Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            //eventLog1.WriteEntry("In OnStop.");
            if (Microphone != null)
            {
                Microphone.Dispose();
            }
            WootingHelper.Close();
        }

        protected override void OnContinue()
        {
            //eventLog1.WriteEntry("In OnContinue.");
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }


    }
}
