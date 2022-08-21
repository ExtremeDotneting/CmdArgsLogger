using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdArgsLogger
{
    internal class RunOptions
    {
        public string ExeToRun { get; set; }

        public bool IncludeEnvVariablesToLog { get; set; }
    }
}
