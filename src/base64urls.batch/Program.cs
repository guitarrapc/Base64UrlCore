using MicroBatchFramework;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base64UrlCore.Tool
{
    class Program
    {
        private static readonly string[] validCommand = new[]
        {
            "help", "list", "-h", "-help", "--help",
            "version", "-v", "-version", "--version",
            "encode",
            "decode",
            "escape",
            "unescape",
        };
        static async Task Main(string[] args)
        {
            if (args == null || !args.Any() || !validCommand.Contains(args[0]))
            {
                args = new[] { "help" }.ToArray();
            }
            // TODO: How to fallback if none of arg is match to command. E.G., `base64urls version`
            await new HostBuilder().RunBatchEngineAsync<Base64Batch>(args);
        }
    }

    public class Base64Batch : BatchBase
    { 
        [Command("encode", "encode input string to base64url")]
        public void Encode([Option(0)]string input) => Console.WriteLine(Base64Url.Encode(input));

        [Command("decode", "decode input base64url to string")]
        public void Decode([Option(0)]string input) => Console.WriteLine(Base64Url.Decode(input));

        [Command("escape", "escape base64 to base64url")]
        public void Escape([Option(0)]string input) => Console.WriteLine(Base64Url.Escape(input));

        [Command("unescape", "unescape base64url to base64")]
        public void Unescape([Option(0)]string input) => Console.WriteLine(Base64Url.Unescape(input));

        /// <summary>
        /// Provide unix style command argument: -version --version -v + version command
        /// </summary>
        [Command(new[] { "version", "-v", "-version", "--version" }, "show version")]
        public void ShowVersion()
        {
            var version = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion
                .ToString();
            Console.WriteLine($"base64urls v{version}");
        }

        /// <summary>
        /// Provide unix style command argument: -help --help -h + override default help / list
        /// </summary>
        [Command(new[] { "help", "list", "-h", "-help", "--help" }, "show help")]
        public void ShowHelp()
        {
            Console.WriteLine("Usage: base64urls [-version] [-help] [decode|encode|escape|unescape] [args]");
            Console.WriteLine("E.g., run this: base64urls decode QyMgaXMgYXdlc29tZQ==");
            Console.WriteLine("E.g., run this: base64urls encode \"C# is awesome.\"");
            Console.WriteLine("E.g., run this: base64urls escape \"This+is/goingto+escape==\"");
            Console.WriteLine("E.g., run this: base64urls unescape \"This-is_goingto-escape\"");
        }
    }
}
