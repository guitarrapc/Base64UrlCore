using FluentAssertions;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Base64UrlCore.Tests
{
    public class AdditionalUnitTest
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
        public void DecodeThenEncodeShouldMatchEncodeTest(string input, string raw)
        {
            var encode = Base64Url.Encode(Base64Url.Decode(input));
            encode.Should().Be(input);
            var decode = Base64Url.Decode(Base64Url.Encode(raw));
            decode.Should().Be(raw);
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
        public void DecodePaddingMissAutoFixTest(string input, string expected)
        {
            var decode = Base64Url.Decode(input);
            decode.Should().Be(expected);
        }
    }
}
