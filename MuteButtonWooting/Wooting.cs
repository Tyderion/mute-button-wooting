using System;
using System.Collections.Generic;
using System.Text;
using Wooting;

namespace MuteButtonWooting
{
    class Wooting
    {
        public static bool IsRgbConnected => RGBControl.IsConnected();

        public static void UpdateButtonToCurrentState(bool IsMuted)
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

        private static void TurnModeButtonRed()
        {
            RGBControl.SetKey(WootingKey.Keys.Mode_ScrollLock, 255, 0, 0, true);
        }

        private static void TurnModeButtonNormal()
        {
            RGBControl.ResetKey(WootingKey.Keys.Mode_ScrollLock);
        }
    }
}
