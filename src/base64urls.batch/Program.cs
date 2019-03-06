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
            // TODO: How to fallback if none of arg is match to command. E.G., `base64urls version`
            await new HostBuilder().RunBatchEngineAsync<Base64Batch>(ArgsInterceptor(args));
        }

        /// <summary>
        /// override MicroBatchFramework's default command.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static string[] ArgsInterceptor(string[] args)
        {
            return !args.Any()
                // MEMO: override default error
                ? new[] { "-help" }
                : args
                    // MEMO: override default help message
                    .Select(x => string.Equals("help", x, StringComparison.OrdinalIgnoreCase) ? "-help" : x)
                    // MEMO: override default list message
                    .Select(x => string.Equals("list", x, StringComparison.OrdinalIgnoreCase) ? "-help" : x)
                    .ToArray();
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
        /// Provide unix style command argument: -version --version -v
        /// </summary>
        [Command("-v", "show version")]
        public void Version1() => ShowVersion();
        [Command("-version", "show version")]
        public void Version2() => ShowVersion();
        [Command("--version", "show version")]
        public void Version3() => ShowVersion();

        /// <summary>
        /// Provide unix style command argument: -help --help -h
        /// </summary>
        [Command("-h", "show help")]
        public void Help1() => ShowHelp();
        [Command("-help", "show help")]
        public void Help2() => ShowHelp();
        [Command("--help", "show help")]
        public void Help3() => ShowHelp();

        private void ShowVersion()
        {
            var version = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion
                .ToString();
            Console.WriteLine($"base64urls v{version}");
        }

        private void ShowHelp()
        {
            Console.WriteLine("Usage: base64urls [-version] [-help] [decode|encode|escape|unescape] [args]");
            Console.WriteLine("E.g., run this: base64urls decode QyMgaXMgYXdlc29tZQ==");
            Console.WriteLine("E.g., run this: base64urls encode \"C# is awesome.\"");
            Console.WriteLine("E.g., run this: base64urls escape \"This+is/goingto+escape==\"");
            Console.WriteLine("E.g., run this: base64urls unescape \"This-is_goingto-escape\"");
        }
    }
}
