using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdArgsLogger
{
    internal static class HelpFileCreator
    {
        public static string CreateAndOpenHelpFile(string exeFile, string jsonFileThatNotFound)
        {
            var text= $"--- ERROR ---\nConfiguration file '{jsonFileThatNotFound}' for cmd logger was not found."+
                @"

--- INFO ---
"
        }
    }
}
