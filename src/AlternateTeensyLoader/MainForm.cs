using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AlternateTeensyLoader
{
    public partial class MainForm : Form
    {
        const string TEENSY_CLI_FILENAME = "teensy_loader_cli.exe";
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Alternate Teensy Loader.");

            // Check if Teensy loader CLI is in the same directory
            CheckIfCliToolExists();

            // Check if Teensy dfu can be found & update interface
            UpdateUSBStatus();

            // Listen for USB changes
            UsbNotification.RegisterUsbDeviceNotification(this.Handle);

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == UsbNotification.WmDevicechange)
            {
                switch ((int)m.WParam)
                {
                    case UsbNotification.DbtDeviceremovecomplete:
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            UpdateUSBStatus();
                        }));
                        break;
                    case UsbNotification.DbtDevicearrival:
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            UpdateUSBStatus();
                        }));
                        break;
                }
            }
        }

        private void CheckIfCliToolExists()
        {
            string cliToolDownloadLink = "https://www.pjrc.com/teensy/teensy_loader_cli_windows.zip";
            if (System.IO.File.Exists(TEENSY_CLI_FILENAME))
            {
                Console.WriteLine("CLI tool {0} OK", TEENSY_CLI_FILENAME);
            }
            else
            {
                Console.WriteLine("Please ensure that Teensy Loader CLI ({0}) \nis in the same folder as AlternateTeensyLoader", TEENSY_CLI_FILENAME);
                Console.WriteLine("Teensy Loader CLI can be obtained at \n{0}", cliToolDownloadLink);
                btnSelectFw.Hide();
                btnUploadFw.Hide();
                txtFwPath.Hide();
                lblFirmwarePath.Text = "Teensy Loader CLI could not be found.";
            }
        }

        private void UpdateUSBStatus()
        {
            var usbDevices = GetUSBDevices();
            bool foundTeensyDfu = false;
            bool foundTeensyRawHid = false;

            foreach (var usbDevice in usbDevices)
            {
                if (usbDevice.DeviceID.Contains("VID_16C0&PID_0478"))
                {
                    foundTeensyDfu = true;
                }
                if (usbDevice.DeviceID.Contains("VID_16C0&PID_0486")) 
                {
                    foundTeensyRawHid = true;
                }
            }

            if (foundTeensyDfu)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Found Teensy in Bootloader mode - Ready to upload firmware.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                btnUploadFw.Enabled = true;
            }
            else
            {
                if (foundTeensyRawHid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Found Teensy in normal mode. \nPress Reset button on Teensy to enter Bootloader for uploading firmware");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    btnUploadFw.Enabled = false;
                }
                else
                {
                    Console.WriteLine("Could not find a Teensy device");
                }
            }
        }

        static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;
        }

        private void btnSelectFw_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a firmware file (.hex)";
            ofd.Filter = "Firmware files|*.hex";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFwPath.Text = ofd.FileName;
                lblFirmwarePath.Text = ofd.FileName;
            }
        }

        private void btnUploadFw_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtFwPath.Text))
            {
                MessageBox.Show(string.Format("Please select a firmware (could not be found at '{0}')", txtFwPath.Text));
                return;
            }
            btnUploadFw.Enabled = false;
            Console.WriteLine("Starting upload");
            RunTeensyLoaderCliAndRedirectOutput(EncodeParameterArgument(txtFwPath.Text));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Upload complete");
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        public static string EncodeParameterArgument(string original)
        {
            if (string.IsNullOrEmpty(original))
                return original;
            string value = System.Text.RegularExpressions.Regex.Replace(original, @"(\\*)" + "\"", @"$1\$0");
            value = System.Text.RegularExpressions.Regex.Replace(value, @"^(.*\s.*?)(\\*)$", "\"$1$2$2\"");
            return value;
        }
        
        private void RunTeensyLoaderCliAndRedirectOutput(string fwPath)
        {
            Process process = new Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = "--mcu=atmega32u4 " + fwPath;
            process.StartInfo.FileName = TEENSY_CLI_FILENAME;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += (sender, args) => Console.WriteLine("{0}", args.Data);
            process.Start();
            process.BeginOutputReadLine();

        }
    }
}


class USBDeviceInfo
{
    public USBDeviceInfo(string deviceID, string pnpDeviceID, string description)
    {
        this.DeviceID = deviceID;
        this.PnpDeviceID = pnpDeviceID;
        this.Description = description;
    }
    public string DeviceID { get; private set; }
    public string PnpDeviceID { get; private set; }
    public string Description { get; private set; }
    
}


internal static class UsbNotification
{
    public const int DbtDevicearrival = 0x8000; // system detected a new device        
    public const int DbtDeviceremovecomplete = 0x8004; // device is gone      
    public const int WmDevicechange = 0x0219; // device change event      
    private const int DbtDevtypDeviceinterface = 5;
    private static readonly Guid GuidDevinterfaceUSBDevice = new Guid("A5DCBF10-6530-11D2-901F-00C04FB951ED"); // USB devices
    private static IntPtr notificationHandle;

    /// <summary>
    /// Registers a window to receive notifications when USB devices are plugged or unplugged.
    /// </summary>
    /// <param name="windowHandle">Handle to the window receiving notifications.</param>
    public static void RegisterUsbDeviceNotification(IntPtr windowHandle)
    {
        DevBroadcastDeviceinterface dbi = new DevBroadcastDeviceinterface
        {
            DeviceType = DbtDevtypDeviceinterface,
            Reserved = 0,
            ClassGuid = GuidDevinterfaceUSBDevice,
            Name = 0
        };

        dbi.Size = Marshal.SizeOf(dbi);
        IntPtr buffer = Marshal.AllocHGlobal(dbi.Size);
        Marshal.StructureToPtr(dbi, buffer, true);

        notificationHandle = RegisterDeviceNotification(windowHandle, buffer, 0);
    }

    /// <summary>
    /// Unregisters the window for USB device notifications
    /// </summary>
    public static void UnregisterUsbDeviceNotification()
    {
        UnregisterDeviceNotification(notificationHandle);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);

    [DllImport("user32.dll")]
    private static extern bool UnregisterDeviceNotification(IntPtr handle);

    [StructLayout(LayoutKind.Sequential)]
    private struct DevBroadcastDeviceinterface
    {
        internal int Size;
        internal int DeviceType;
        internal int Reserved;
        internal Guid ClassGuid;
        internal short Name;
    }
}
