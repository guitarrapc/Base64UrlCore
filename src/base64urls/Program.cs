using MicroBatchFramework;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Base64UrlCore.Tool
{
    class Program
    {
        static async Task Main(string[] args) =>
            await new HostBuilder().RunBatchEngineAsync<Base64Batch>(args);
    }

    public class Base64Batch : BatchBase
    {
        [Command("encode", "encode input string to base64url")]
        public void Encode([Option(0)]string input) => Context.Logger.LogInformation(Base64Url.Encode(input));

        [Command("decode", "decode input base64url to string")]
        public void Decode([Option(0)]string input) => Context.Logger.LogInformation(Base64Url.Decode(input));

        [Command("escape", "escape base64 to base64url")]
        public void Escape([Option(0)]string input) => Context.Logger.LogInformation(Base64Url.Escape(input));

        [Command("unescape", "unescape base64url to base64")]
        public void Unescape([Option(0)]string input) => Context.Logger.LogInformation(Base64Url.Unescape(input));

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
            Context.Logger.LogInformation($"base64urls v{version}");
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
            Context.Logger.LogInformation("Usage: base64urls [-version] [-help] [decode|encode|escape|unescape] [args]");
            Context.Logger.LogInformation("E.g., run this: base64urls decode QyMgaXMgYXdlc29tZQ==");
            Context.Logger.LogInformation("E.g., run this: base64urls encode \"C# is awesome.\"");
            Context.Logger.LogInformation("E.g., run this: base64urls escape \"This+is/goingto+escape==\"");
            Context.Logger.LogInformation("E.g., run this: base64urls unescape \"This-is_goingto-escape\"");
        }
    }
}
