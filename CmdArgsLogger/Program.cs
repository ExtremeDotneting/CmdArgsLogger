using System;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CmdArgsLogger
{
    class Program
    {
        private static string _currentExeFullPath;
        private static string _argsString;
        private static string _exeToRun;
        private static bool _includeEnvVariablesToLog;
        private static Process _currentProcess;

        public static void Main(string[] args)
        {
            try
            {
                _currentProcess = Process.GetCurrentProcess();
                _currentExeFullPath = _currentProcess.MainModule.FileName;
                _argsString = string.Join(' ', args);
                var opt = TryGetRunOptions();
                if (opt != null)
                {
                    _exeToRun = opt.ExeToRun;
                    _includeEnvVariablesToLog = opt.IncludeEnvVariablesToLog;
                }
                WriteRunInfoToLog();
                if (opt == null)
                {
                    throw new Exception(
                      $"CmdArgsLogger configuration file for '{_currentExeFullPath}' was not found.\n" +
                      $"Read instructions here 'https://github.com/ExtremeDotneting/CmdArgsLogger'"
                      );
                }
                var process = StartAnotherProcess();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                WriteToLog(ex.ToString());
            }
        }

        static void WriteRunInfoToLog()
        {
            var info = new Dictionary<string, object>();
            info["CalledExe"] = _currentExeFullPath;
            info["ProxyfiedToExe"] = _exeToRun;
            info["Args"] = _argsString;
            info["CurrentDirectory"] = Environment.CurrentDirectory;
            if (_includeEnvVariablesToLog)
                info["EnvironmentVariables"] = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
            var text = JsonConvert.SerializeObject(info, Formatting.Indented);
            WriteToLog(text);

        }

        static Process StartAnotherProcess()
        {
            var psi = new ProcessStartInfo()
            {
                FileName = _exeToRun,
                //RedirectStandardInput = true,
                //RedirectStandardOutput = true,
                //RedirectStandardError = true,
                Arguments = _argsString,
                UseShellExecute = false

            };
            var envVariables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
            psi.EnvironmentVariables.Clear();
            foreach (DictionaryEntry ent in envVariables)
                psi.EnvironmentVariables.Add((string)ent.Key, (string)ent.Value);

            var process = Process.Start(psi);
            return process;
        }

        static RunOptions TryGetRunOptions()
        {
            var jsonPath = _currentExeFullPath.Remove(_currentExeFullPath.Length - ".exe".Length) + ".cmd_args_logger.json";
            if (!File.Exists(jsonPath))
            {

            }
            var jsonText = File.ReadAllText(jsonPath);
            var opt = JsonConvert.DeserializeObject<RunOptions>(jsonText);
            return opt;
        }

        static void WriteToLog(string str)
        {
            var logsDir = "C:\\CmdArgsLoader";
            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }
            var currentLoggerName = $"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.log";
            var filePath = Path.Combine(logsDir, currentLoggerName);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }
            var fullStr = $"---------------------------\nDateTime: {DateTime.Now}\n" + str + "\n---------------------------";
            File.AppendAllText(filePath, fullStr);
        }
    }
}
