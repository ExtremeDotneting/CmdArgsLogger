using System;
using System.Diagnostics;

namespace CmdArgsLogger
{
    class Program
    {
        private static string _currentExeFullPath;
        private static string _argsString;
        private static Process _currentProcess;

        public static void Main(string[] args)
        {
            _currentProcess = Process.GetCurrentProcess();
            _currentExeFullPath = _currentProcess.MainModule.FileName;
            _argsString = string.Join(' ', args);

            Process.Start(new ProcessStartInfo()
            {
                FileName=
            });
           
            Console.WriteLine(argsString);
            Console.ReadLine();
        }

        RunOptions TryGetRunOptions()
        {
            var jsonPath = _currentExeFullPath.Remove(_currentExeFullPath.Length - ".exe".Length) + "cmd_args_logger.json";
            if (File.Exists(jsonPath))
            {

            }
            var jsonFile = Process.GetCurrentProcess();
        }
    }
}
