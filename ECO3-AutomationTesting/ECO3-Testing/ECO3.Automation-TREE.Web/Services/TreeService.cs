using ECO3.Automation_TREE.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace ECO3.Automation_TREE.Web.Services
{
    public class TreeService : ITreeService
    {
        // Based on https://stackoverflow.com/questions/285760/how-to-spawn-a-process-and-capture-its-stdout-in-net
        public ProcessExecution Run(string exePath, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process p = new Process();

            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;

            startInfo.UseShellExecute = false;
            startInfo.Arguments = arguments;
            startInfo.FileName = exePath;

            p.StartInfo = startInfo;
            p.Start();

            p.WaitForExit();

            StringBuilder output = new StringBuilder();
            output.Append(p.StandardOutput.ReadToEnd());
            output.Append(p.StandardError.ReadToEnd());

            var result = new ProcessExecution()
            {
                ExitCode = p.ExitCode,
                Log = output.ToString()
            };

            return result;
        }
    }
}