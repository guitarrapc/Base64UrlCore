using System;
using System.Linq;
using System.Reflection;

namespace Base64UrlCore.Tool
{
    class Program
    {
        enum Command
        {
            none, // unsupported command
            decode,
            encode,
            escape,
            unescape,
        }

        static void Main(string[] args)
        {
            // version -> help -> invalid -> command
            if (HasFlag(args, "v", "version"))
            {
                ShowVersion();
                return;
            }
            if (HasFlag(args, "h", "help"))
            {
                ShowHelp();
                return;
            }
            if (!IsValidArguments(args))
            {
                ShowHelp();
                return;
            }

            var command = FetchCommand(args[0]);
            var input = args[1];
            try
            {
                switch (command)
                {
                    case Command.none:
                        ShowHelp();
                        return;
                    case Command.decode:
                        Console.WriteLine(Base64Url.Decode(input));
                        return;
                    case Command.encode:
                        Console.WriteLine(Base64Url.Encode(input));
                        return;
                    case Command.escape:
                        Console.WriteLine(Base64Url.Escape(input));
                        return;
                    case Command.unescape:
                        Console.WriteLine(Base64Url.Unescape(input));
                        return;
                    default:
                        throw new NotImplementedException($"{command} is not impremented.");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"failed decode input. {e.Message}");
            }
        }

        static bool HasFlag(string[] args, string shortName, string longName) => args.Length != 0 && (args.Contains("-" + shortName) || args.Contains("-" + longName) || args.Contains("--" + longName));
        static bool IsValidArguments(string[] args) => args.Length == 2;
        static Command FetchCommand(string actual) => Enum.TryParse<Command>(actual, ignoreCase: true, result: out var command)
                ? command
                : Command.none;

        /// <summary>
        /// -v -version --version
        /// </summary>
        static void ShowVersion()
        {
            var version = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion
                .ToString();
            Console.WriteLine($"base64urls v{version}");
        }

        /// <summary>
        /// -h -help --help
        /// </summary>
        static void ShowHelp()
        {
            Console.WriteLine("Usage: base64urls [-version] [-help] [decode|encode|escape|unescape] [args]");
            Console.WriteLine("E.g., run this: base64urls decode QyMgaXMgYXdlc29tZQ==");
            Console.WriteLine("E.g., run this: base64urls encode \"C# is awesome.\"");
            Console.WriteLine("E.g., run this: base64urls escape \"This+is/goingto+escape==\"");
            Console.WriteLine("E.g., run this: base64urls unescape \"This-is_goingto-escape\"");
        }
    }
}