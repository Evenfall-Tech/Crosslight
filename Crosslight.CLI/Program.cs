using CommandLine;
using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crosslight.CLI
{
    class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
            [Option('j', "json", Required = false, HelpText = "Load build pipeline from options.")]
            public string Json { get; set; }
        }
        public class JsonOptions
        {
            public IEnumerable<string> InputFiles { get; set; }
            public string InputLanguageAssembly { get; set; }
            public string OutputLanguageAssembly { get; set; }
        }
        static void Main(string[] args)
        {
            throw new NotImplementedException();

            IConfiguration configuration = null;
            ILogger logger = null;
            try
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("settings.json")
                    .Build();

                logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
                Logger.Instance = logger;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to initialize configuration and logging.");
                Console.Error.WriteLine($"Fatal error encountered: {e}");
                return;
            }

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       IFileSystemItem source = null;
                       if (o.Json != null && File.Exists(o.Json))
                       {
                           JsonOptions jsonOptions = JsonConvert.DeserializeObject<JsonOptions>(File.ReadAllText(o.Json));
                           if (jsonOptions != null && jsonOptions.InputFiles != null && jsonOptions.InputFiles.Count() > 0)
                           {
                               source = FileSystem.FromFiles(jsonOptions.InputFiles);
                           }
                       }
                       if (source == null)
                       {
                           Logger.Instance.Fatal("No sources found to process.");
                           return;
                       }
                   })
                   .WithNotParsed(o =>
                   {
                       foreach (var error in o)
                       {
                           Console.WriteLine(error.ToString());
                       }
                   });
        }
    }
}
