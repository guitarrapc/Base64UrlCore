using Base64UrlCore.Tool;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Base64UrlCoreTool.Tests
{
    public class GlobalToolTest
    {
        [Theory]
        [InlineData("MQ==", "1")]
        [InlineData("MTE=", "11")]
        [InlineData("MTEx", "111")]
        [InlineData("Zm9v", "foo")]
        [InlineData("YmFy", "bar")]
        [InlineData("Zm9vYmFyYmF6", "foobarbaz")]
        [InlineData("aG9nZW1vZ2U=", "hogemoge")]
        [InlineData("cGl5b3BpeW8=", "piyopiyo")]
        [InlineData("QyMgaXMgYXdlc29tZS4=", "C# is awesome.")]
        [InlineData("Tm9kZS5qcyBpcyBhd2Vzb21lLg==", "Node.js is awesome.")]
        public async Task DecodeTest(string input, string expected)
        {
            var base64 = new Base64Batch();
            await base64.Decode(input);
            var read = await base64.Reader.ReadAsync();
            read.Should().Be(expected);
        }

        [Theory]
        [InlineData("1", "MQ==")]
        [InlineData("11", "MTE=")]
        [InlineData("111", "MTEx")]
        [InlineData("foo", "Zm9v")]
        [InlineData("bar", "YmFy")]
        [InlineData("foobarbaz", "Zm9vYmFyYmF6")]
        [InlineData("hogemoge", "aG9nZW1vZ2U=")]
        [InlineData("piyopiyo", "cGl5b3BpeW8=")]
        [InlineData("C# is awesome.", "QyMgaXMgYXdlc29tZS4=")]
        [InlineData("Node.js is awesome.", "Tm9kZS5qcyBpcyBhd2Vzb21lLg==")]
        public async Task EncodeTest(string input, string expected)
        {
            var base64 = new Base64Batch();
            await base64.Encode(input);
            var read = await base64.Reader.ReadAsync();
            read.Should().Be(expected);
        }

        [Theory]
        [InlineData("aG9nZW1vZ2U", "hogemoge")]
        [InlineData("cGl5b3BpeW8", "piyopiyo")]
        [InlineData("eyJraWQiOiIxZTlnZGs3IiwiYWxnIjoiUlMyNTYifQ", "{\"kid\":\"1e9gdk7\",\"alg\":\"RS256\"}")]
        [InlineData("eyJraWQiOiIxZTlnZGs3IiwiYWxnIjoiUlMyNTYifQ=", "{\"kid\":\"1e9gdk7\",\"alg\":\"RS256\"}")]
        [InlineData("eyJhbGciOiJub25lIn0", "{\"alg\":\"none\"}")]
        [InlineData("QyMgaXMgYXdlc29tZS4", "C# is awesome.")]
        [InlineData("Tm9kZS5qcyBpcyBhd2Vzb21lLg", "Node.js is awesome.")]
        [InlineData("Tm9kZS5qcyBpcyBhd2Vzb21lLg=", "Node.js is awesome.")]
        public async Task DecodePaddingMissAutoFixTest(string input, string expected)
        {
            var base64 = new Base64Batch();
            await base64.Decode(input);
            var read = await base64.Reader.ReadAsync();
            read.Should().Be(expected);
        }

        [Theory]
        [InlineData("This+is/goingto+escape==", "This-is_goingto-escape")]
        public async Task EscapeTest(string input, string expected)
        {
            var base64 = new Base64Batch();
            await base64.Escape(input);
            var read = await base64.Reader.ReadAsync();
            read.Should().Be(expected);
        }

        [Theory]
        [InlineData("This-is_goingto-escape", "This+is/goingto+escape==")]
        public async Task UnescapeTest(string input, string expected)
        {
            var base64 = new Base64Batch();
            await base64.Unescape(input);
            var read = await base64.Reader.ReadAsync();
            read.Should().Be(expected);
        }
    }
}
