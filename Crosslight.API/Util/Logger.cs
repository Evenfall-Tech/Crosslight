using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Util
{
    public class Logger
    {
        // TODO: add default if null.
        public static ILogger Instance { get; set; }
    }
}
