# WootingService

A simple service to mute/unmute microphone globally. Adjusts wooting mode key color to match mute status (red = muted, default = unmuted).

Run `WootingController.exe` to toggle mute.

## Setup
- Open in Visual Studio.
- Configure Microphone id in `WootingService.cs` (find your microphone ID by using `MMDE.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)`)
- Build Solution
- Download and copy `wooting-rgb-sdk.dll` (https://github.com/WootingKb/wooting-rgb-sdk) into build folder (`bin/Debug` or `bin/Release`)
- Install service by running opening `Developer Powershell for Visual Studio 2019` and running `installutil WootingService.exe`
- Set service to auto start in `services.msc` if wanted
- Setup a key to run WootingController.exe to toggle mute (e.g. with AutoHotkey)

