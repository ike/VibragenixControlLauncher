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

        static int ProcessInput(string[] s)
        {
            string[] uriParams = s[0].Split(':');
            string SessionString = uriParams[1];

            // If SessionString is null, return 0
            if (SessionString == "" || SessionString.Length < 1)
                return 0;

            // If SessionString is an integer, return the integer as milliseconds
            int SessionSeconds;
            if (int.TryParse(SessionString, out SessionSeconds))
            {
                Console.WriteLine("Session Minutes: " + SessionSeconds);
                return SessionSeconds * 1000;
            }

            // If SessionString is not an integer, return 0
            else
                return 0;
        }

        static void Main(string[] args)
        {
            // Get timeout length from args, if any
            var SessionLength = ProcessInput(args);

            Console.WriteLine("VIBRAGENIX CONTROL LAUNCHER");
            Console.WriteLine("Launching..." + SessionLength);
            
            // Hide our console window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            ProcessStartInfo launchInfo = new ProcessStartInfo();
            launchInfo.FileName = @"C:\Program Files\Vibragenix\Vibragenix Control Launcher\VGNX.exe";

            Process launch = new Process();
            launch.StartInfo = launchInfo;
            launch.EnableRaisingEvents = true;

            try
            {
                launch.Start();

                if (launch.WaitForExit(SessionLength))
                    ;// User closed Vibragenix Control
                else
                {
                    launch.Kill();
                    // launch vibragenix
                }
                    
            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}
