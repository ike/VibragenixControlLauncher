using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VibragenixControl
{
    class Launcher
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static string ProcessInput(string s)
        {
            // TODO Verify and validate the input 
            // string.
            return s;
        }

        static void Main(string[] args)
        {
            // Hide our console window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            // Get timeout length from args, if any
            int SessionMinutes, SessionLength;
            if (Int32.TryParse(args[0], out SessionMinutes))
                SessionLength = SessionMinutes * 60000;
            else
                SessionLength = 5000;

            ProcessStartInfo launchInfo = new ProcessStartInfo();
            launchInfo.FileName = "notepad.exe";

            Process launch = new Process();
            launch.StartInfo = launchInfo;
            launch.EnableRaisingEvents = true;

            try
            {
                launch.Start();

                if (launch.WaitForExit(SessionLength))
                    ;// User closed Vibragenix Control
                else
                    launch.Kill();
            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}
