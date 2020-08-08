using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wooting;

namespace WootingService
{
    static class WootingHelper
    {
        public static bool IsRgbConnected => RGBControl.IsConnected();

        public static void UpdateButtonToCurrentState(bool IsMuted)
        {
            //if (IsRgbConnected)
            //{
                if (IsMuted)
                {
                    TurnModeButtonRed();
                }
                else
                {
                    TurnModeButtonNormal();
                }
            //}

        }

        public static void Close()
        {
            TurnModeButtonNormal();
            RGBControl.Close();
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
