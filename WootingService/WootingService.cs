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
            log = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("WootingLog"))
            {
                System.Diagnostics.EventLog.CreateEventSource("WootingLog", "WootingLog");
            }
            log.Source = "WootingLog";
            log.Log = "WootingLog";
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
                        UpdateKeyColor();
                    } catch (Exception ex)
                    {
                        log.WriteEntry("Update button failed: " + ex.Message, EventLogEntryType.Error, eventId++);
                    }
                } else if (com == WootingCommands.Toggle)
                {
                    try
                    {
                        ToggleMicrophone();
                        UpdateKeyColor();
                    } catch (Exception ex)
                    {
                        log.WriteEntry("Unable to Update Button " + ex.Message, EventLogEntryType.Error, eventId++);
                    }
                }

            } catch (Exception ex)
            {
                log.WriteEntry("Error: "+ ex.Message, EventLogEntryType.Error, eventId++);
            }
        }

        protected override void OnStart(string[] args)

        {

            TryConnect();
            UpdateKeyColor();
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
                log.WriteEntry("Could not find microphone " + ex.Message, EventLogEntryType.Error, eventId++);
            }
            return null;
        }

        private void UpdateKeyColor()
        {
            try
            {
                if (IsConnected)
                {
                    WootingHelper.UpdateButtonToCurrentState(IsMute());
                }
            } catch (Exception ex)
            {
                log.WriteEntry("Unable to update button state " + ex.Message, EventLogEntryType.Error, eventId++);
            }
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
                log.WriteEntry("Connected to Keyboard", EventLogEntryType.Information, eventId++);

            }
            catch (Exception ex)
            {
                log.WriteEntry("Not connected to keyboard: " + ex.Message, EventLogEntryType.Error, eventId++);

            }
            try
            {
                Microphone = GetMicrophone();
                log.WriteEntry("Got Microphone " + Microphone.FriendlyName, EventLogEntryType.Information, eventId++);
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

        private void eventLog1_EntryWritten_1(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
