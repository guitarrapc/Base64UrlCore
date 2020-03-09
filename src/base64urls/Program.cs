using Cocona;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Base64UrlCore.Tool
{
    class Program
    {
        static async Task Main(string[] args) =>
            await CoconaLiteApp.RunAsync<Base64Batch>(args);
    }

    public class Base64Batch
    {
        internal ChannelReader<string> Reader { get; }
        private readonly Channel<string> _channel;

        public Base64Batch()
        {
            _channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = true,
            });
            Reader = _channel.Reader;
        }

        [Command(Description = "encode input string to base64url. run this: base64urls encode \"C# is awesome.\"")]
        public async ValueTask Encode([Argument]string input)
        {
            var result = Base64Url.Encode(input);
            await _channel.Writer.WriteAsync(result);
            Console.WriteLine(result);
        }

        [Command(Description = "decode input base64url to string. run this: base64urls decode QyMgaXMgYXdlc29tZQ==")]
        public async ValueTask Decode([Argument]string input)
        {
            var result = Base64Url.Decode(input);
            await _channel.Writer.WriteAsync(result);
            Console.WriteLine(result);
        }

        [Command(Description = "escape base64 to base64url. run this: base64urls escape \"This+is/goingto+escape==\"")]
        public async ValueTask Escape([Argument]string input)
        {
            var result = Base64Url.Escape(input);
            await _channel.Writer.WriteAsync(result);
            Console.WriteLine(result);
        }

        [Command(Description = "unescape base64url to base64. run this: base64urls unescape \"This-is_goingto-escape\"")]
        public async ValueTask Unescape([Argument]string input)
        {
            var result = Base64Url.Unescape(input);
            await _channel.Writer.WriteAsync(result);
            Console.WriteLine(result);
        }
    }
}
