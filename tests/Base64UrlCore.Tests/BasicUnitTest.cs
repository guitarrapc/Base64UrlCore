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
        public void DecodeTest(string input, string exptected)
        {
            var decode = Base64Url.Decode(input);
            decode.Should().Be(exptected);
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
        public void EncodeTest(string input, string expected)
        {
            var encode = Base64Url.Encode(input);
            encode.Should().Be(expected);
        }

        [Theory]
        [InlineData("This+is/goingto+escape==", "This-is_goingto-escape")]
        public void EscapeTest(string input, string expected)
        {
            var encode = Base64Url.Escape(input);
            encode.Should().Be(expected);
        }

        [Theory]
        [InlineData("This-is_goingto-escape", "This+is/goingto+escape==")]
        public void UnescapeTest(string input, string expected)
        {
            var encode = Base64Url.Unescape(input);
            encode.Should().Be(expected);
        }

        [Theory]
        [InlineData("MQ", "MQ==")]
        [InlineData("MTE", "MTE=")]
        [InlineData("aG9nZW1vZ2U", "aG9nZW1vZ2U=")]
        [InlineData("cGl5b3BpeW8", "cGl5b3BpeW8=")]
        public void PaddingTest(string input, string expected)
        {
            var encode = Base64Url.PadString(input);
            encode.Should().Be(expected);
        }

        [Theory]
        [InlineData("MQ", "MQ==")]
        [InlineData("MTE", "MTE=")]
        [InlineData("aG9nZW1vZ2U", "aG9nZW1vZ2U=")]
        [InlineData("cGl5b3BpeW8", "cGl5b3BpeW8=")]
        public void RemovePaddingTest(string input, string expected)
        {
            var encode = Base64Url.RemovePadding(expected);
            encode.Should().Be(input);
        }
    }
}
