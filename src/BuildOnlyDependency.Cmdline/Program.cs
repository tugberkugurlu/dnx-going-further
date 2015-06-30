using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Runtime.Common.CommandLine;

namespace BuildOnlyDependency.Cmdline
{
    public class Program
    {
        public void Main(string[] args)
        {
            var cmdlineApp = new CommandLineApplication();
            cmdlineApp.HelpOption("--help");
            cmdlineApp.Command("build", c => 
            {
                c.Option("--foo", "Foo bar", CommandOptionType.NoValue);
            });

            cmdlineApp.Execute(args);
        }
    }
}
