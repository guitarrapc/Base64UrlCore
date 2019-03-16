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
        static async Task Main(string[] args)
        {
            if (args.Any() && !IsValidCommand(args[0]))
            {
                args = new[] { "help" }.ToArray();
            }
            await new HostBuilder().RunBatchEngineAsync<Base64Batch>(args);
        }

        /// <summary>
        /// Ready for fallback to help when f none of arg is match to valid command. E.G., `base64urls unexceptected` should not acceptable.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private static bool IsValidCommand(string command) => new[]
        {
            "help", "list", "-h", "-help", "--help",
            "version", "-v", "-version", "--version",
            "encode",
            "decode",
            "escape",
            "unescape",
        }.Contains(command);
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
        public void Version()
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
        /// <remarks>
        /// Also override default help. no arguments execution will fallback to here.
        /// </remarks>
        [Command(new[] { "help", "list", "-h", "-help", "--help" }, "show help")]
        public void Help()
        {
            Console.WriteLine("Usage: base64urls [-version] [-help] [decode|encode|escape|unescape] [args]");
            Console.WriteLine("E.g., run this: base64urls decode QyMgaXMgYXdlc29tZQ==");
            Console.WriteLine("E.g., run this: base64urls encode \"C# is awesome.\"");
            Console.WriteLine("E.g., run this: base64urls escape \"This+is/goingto+escape==\"");
            Console.WriteLine("E.g., run this: base64urls unescape \"This-is_goingto-escape\"");
        }
    }
}
