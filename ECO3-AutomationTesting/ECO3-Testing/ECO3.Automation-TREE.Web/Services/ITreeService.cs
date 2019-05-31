using ECO3.Automation_TREE.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO3.Automation_TREE.Web.Services
{
    interface ITreeService
    {
        ProcessExecution Run(string exePath, string arguments);
    }
}
