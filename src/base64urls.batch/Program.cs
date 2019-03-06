using MicroBatchFramework;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Base64UrlCore.Tool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // TODO: override default error
            // TODO: override default help message
            // TODO: Is any simple way to provide unix style help: -version --version -v / -help --help -h
            await new HostBuilder().RunBatchEngineAsync<Base64Batch>(args);
        }
    }

    public class Base64Batch : BatchBase
    {
        [Command("encode")]
        public void Encode([Option(0)]string input) => Console.WriteLine(Base64Url.Encode(input));

        [Command("decode")]
        public void Decode([Option(0)]string input) => Console.WriteLine(Base64Url.Decode(input));

        [Command("escape")]
        public void Escape([Option(0)]string input) => Console.WriteLine(Base64Url.Escape(input));

        [Command("unescape")]
        public void Unescape([Option(0)]string input) => Console.WriteLine(Base64Url.Unescape(input));

        /// <summary>
        /// -v -version --version
        /// </summary>
        [Command("-v")]
        public void Version1() => ShowVersion();
        [Command("-version")]
        public void Version2() => ShowVersion();
        [Command("--version")]
        public void Version3() => ShowVersion();

        /// <summary>
        /// -h -help --help
        /// </summary>
        [Command("-h")]
        public void Help1() => ShowHelp();
        [Command("-help")]
        public void Help2() => ShowHelp();
        [Command("--help")]
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
            Console.WriteLine("Usage: dotnet base64urls [-version] [-help] [decode|encode|escape|unescape] [args]");
            Console.WriteLine("E.g., run this: base64urls decode QyMgaXMgYXdlc29tZQ==");
            Console.WriteLine("E.g., run this: base64urls encode \"C# is awesome.\"");
            Console.WriteLine("E.g., run this: base64urls escape \"This+is/goingto+escape==\"");
            Console.WriteLine("E.g., run this: base64urls unescape \"This-is_goingto-escape\"");
        }
    }
}
