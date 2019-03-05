using FluentAssertions;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Base64UrlCore.Tests
{
    public class BasicUnitTest
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
        public void DecodeTest(string base64, string raw)
        {
            var decode = Base64Url.Decode(base64);
            decode.Should().Be(raw);
        }

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
        public void EncodeTest(string base64, string raw)
        {
            var encode = Base64Url.Encode(raw);
            encode.Should().Be(base64);
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
        public void DecodePaddingMissAutoFixTest(string base64, string raw)
        {
            var decode = Base64Url.Decode(base64);
            decode.Should().Be(raw);
        }

        [Theory]
        [InlineData("This+is/goingto+escape==", "This-is_goingto-escape")]
        public void EscapeTest(string from, string to)
        {
            var encode = Base64Url.Escape(from);
            encode.Should().Be(to);
        }

        [Theory]
        [InlineData("This-is_goingto-escape", "This+is/goingto+escape==")]
        public void UnescapeTest(string from, string to)
        {
            var encode = Base64Url.Unescape(from);
            encode.Should().Be(to);
        }

        [Theory]
        [InlineData("MQ", "MQ==")]
        [InlineData("MTE", "MTE=")]
        [InlineData("aG9nZW1vZ2U", "aG9nZW1vZ2U=")]
        [InlineData("cGl5b3BpeW8", "cGl5b3BpeW8=")]
        public void PaddingTest(string from, string to)
        {
            var encode = Base64Url.PadString(from);
            encode.Should().Be(to);
        }

        [Theory]
        [InlineData("MQ", "MQ==")]
        [InlineData("MTE", "MTE=")]
        [InlineData("aG9nZW1vZ2U", "aG9nZW1vZ2U=")]
        [InlineData("cGl5b3BpeW8", "cGl5b3BpeW8=")]
        public void RemovePaddingTest(string to, string from)
        {
            var encode = Base64Url.RemovePadding(from);
            encode.Should().Be(to);
        }
    }
}
