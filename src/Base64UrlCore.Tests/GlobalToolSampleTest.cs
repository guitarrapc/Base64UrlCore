using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Base64UrlCore.Tests
{
    public class GlobalToolSampleTest
    {
        // copy and paste from: base64urls -> Program.cs
        public enum Command
        {
            none, // unsupported command
            decode,
            encode,
            escape,
            unescape,
            binarydecode,
            binaryencode,
        }
        static bool HasHelpFlag(string[] args, string shortName, string longName) => args.Length != 0 && (args.Contains(shortName) || args.Contains(longName) || args.Contains("-" + longName));
        static bool IsValidFlag(string[] args) => args.Length == 2;
        static bool IsCommand(string actual, string expected) => string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase);
        static Command FetchCommand(string actual) => Enum.TryParse<Command>(actual, ignoreCase: true, result: out var command)
                ? command
                : Command.none;

        [Theory]
        [InlineData("-v")]
        [InlineData("-version")]
        [InlineData("--version")]
        [InlineData("-v", "-h")]
        [InlineData("-version", "encode")]
        [InlineData("--version", "fuga")]
        public void VersionArgsTest(params string[] args)
        {
            HasHelpFlag(args, "-v", "-version").IsTrue();
        }

        [Theory]
        [InlineData("--v")]
        [InlineData("-versions")]
        [InlineData("--versions")]
        public void NotVersionArgsTest(params string[] args)
        {
            HasHelpFlag(args, "-v", "-version").IsFalse();
        }

        [Theory]
        [InlineData("-h")]
        [InlineData("-help")]
        [InlineData("--help")]
        [InlineData("-h", "encode")]
        [InlineData("-help", "encode")]
        [InlineData("--help", "fuga")]
        public void HelpArgsTest(params string[] args)
        {
            HasHelpFlag(args, "-h", "-help").IsTrue();
        }

        [Theory]
        [InlineData("--h")]
        public void NotHelpArgsTest(params string[] args)
        {
            HasHelpFlag(args, "-h", "-help").IsFalse();
        }

        [Theory]
        [InlineData("encode", "QyMgaXMgYXdlc29tZQ==")]
        [InlineData("decode", "\"C# is awesome.\"")]
        [InlineData("escape", "\"This+is/goingto+escape==\"")]
        [InlineData("unescape", "\"This-is_goingto-escape\"")]
        [InlineData("binaryencode", "QyMgaXMgYXdlc29tZQ==")]
        [InlineData("binarydecode", "\"C# is awesome.\"")]
        public void ValidArgsTest(params string[] args)
        {
            IsValidFlag(args).IsTrue();
        }

        [Theory]
        [InlineData("encode")]
        [InlineData("decode")]
        [InlineData("escape")]
        [InlineData("unescape")]
        [InlineData("binaryencode")]
        [InlineData("binarydecode")]
        [InlineData("none")]
        [InlineData("foo", "bar", "baz")]
        public void InvalidArgsTest(params string[] args)
        {
            IsValidFlag(args).IsFalse();
        }

        [Theory]
        [InlineData("encode", Command.encode)]
        [InlineData("decode", Command.decode)]
        [InlineData("escape", Command.escape)]
        [InlineData("unescape", Command.unescape)]
        [InlineData("binaryencode", Command.binaryencode)]
        [InlineData("binarydecode", Command.binarydecode)]
        [InlineData("none", Command.none)]
        public void FetchCommandTest(string command, Command expected)
        {
            FetchCommand(command).Is(expected);
        }
    }
}
