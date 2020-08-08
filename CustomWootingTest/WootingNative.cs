using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CustomWootingTest
{
    public class WootingNative
    {
        [DllImport("wooting-rgb-sdk.dll",
             EntryPoint = "wooting_rgb_kbd_connected",
             CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsConnected();
    }
}
