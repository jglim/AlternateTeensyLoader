# Alternate Teensy Loader

If you have a **Teensy 2.0** that shows up on reset with a *Vendor ID* of `16C0` & a *Product ID* of `0478`, that also refuses to be detected by the original Teensy Loader, Alternate Teensy Loader (ATL) may still be able to flash your firmware.

## Background

A buddy of mine assembled a mechanical keyboard kit, where the included Teensy 2.0 refused to work with Teensy Loader even though it appeared with valid PID/VIDs. Strangely, the CLI version of Teensy Loader worked just fine. ATL acts as a wrapper around the `teensy_loader_cli.exe` binary for ease of use.

## Usage

* Download and unzip both files (*AlternateTeensyLoader.exe* & *teensy_loader_cli.exe*) to a folder
* Run *AlternateTeensyLoader.exe*
* Select your firmware file (.hex)
* Plug in your Teensy 2.0
* Press & release the reset button once
* Press the Upload Firmware file
 
## License
* Alternative Teensy Loader - MIT
* [teensy_loader_cli](https://github.com/PaulStoffregen/teensy_loader_cli) (PaulStoffregen) - GPL 3