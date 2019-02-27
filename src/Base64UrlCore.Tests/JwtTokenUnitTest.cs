using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Base64UrlCore.Tests
{
    public class JwtTokenUnitTest
    {
        [Theory]
        [InlineData("eyJraWQiOiIxZTlnZGs3IiwiYWxnIjoiUlMyNTYifQ", "{\"kid\":\"1e9gdk7\",\"alg\":\"RS256\"}")]
        [InlineData("eyJhbGciOiAiSFMyNTYiLCAidHlwIjogIkpXVCJ9", "{\"alg\": \"HS256\", \"typ\": \"JWT\"}")]
        [InlineData("eyJhbGciOiJub25lIn0=", "{\"alg\":\"none\"}")]
        public void JWTHeaderDecode(string base64, string raw)
        {
            var decode = Base64Url.Decode(base64);
            decode.Is(raw);
        }

        /// <summary>
        /// raw must be LF.(not CRLF)
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="raw"></param>
        [Theory]
        [InlineData("ewogImlzcyI6ICJodHRwOi8vc2VydmVyLmV4YW1wbGUuY29tIiwKICJzdWIiOiAiMjQ4Mjg5NzYxMDAxIiwKICJhdWQiOiAiczZCaGRSa3F0MyIsCiAibm9uY2UiOiAibi0wUzZfV3pBMk1qIiwKICJleHAiOiAxMzExMjgxOTcwLAogImlhdCI6IDEzMTEyODA5NzAsCiAibmFtZSI6ICJKYW5lIERvZSIsCiAiZ2l2ZW5fbmFtZSI6ICJKYW5lIiwKICJmYW1pbHlfbmFtZSI6ICJEb2UiLAogImdlbmRlciI6ICJmZW1hbGUiLAogImJpcnRoZGF0ZSI6ICIwMDAwLTEwLTMxIiwKICJlbWFpbCI6ICJqYW5lZG9lQGV4YW1wbGUuY29tIiwKICJwaWN0dXJlIjogImh0dHA6Ly9leGFtcGxlLmNvbS9qYW5lZG9lL21lLmpwZyIKfQ"
            , @"{
 ""iss"": ""http://server.example.com"",
 ""sub"": ""248289761001"",
 ""aud"": ""s6BhdRkqt3"",
 ""nonce"": ""n-0S6_WzA2Mj"",
 ""exp"": 1311281970,
 ""iat"": 1311280970,
 ""name"": ""Jane Doe"",
 ""given_name"": ""Jane"",
 ""family_name"": ""Doe"",
 ""gender"": ""female"",
 ""birthdate"": ""0000-10-31"",
 ""email"": ""janedoe@example.com"",
 ""picture"": ""http://example.com/janedoe/me.jpg""
}")]
        [InlineData("eyJpc3MiOiJqb2UiLCJleHAiOjEzMDA4MTkzODAsImh0dHA6Ly9leGFtcGxlLmNvbS9pc19yb290Ijp0cnVlfQ", "{\"iss\":\"joe\",\"exp\":1300819380,\"http://example.com/is_root\":true}")]
        public void JWTPayloadDecode(string base64, string raw)
        {
            var decode = Base64Url.Decode(base64);
            decode.Is(raw.Replace("\r", ""));
        }

        [Theory]
        [InlineData("rHQjEmBqn9Jre0OLykYNnspA10Qql2rvx4FsD00jwlB0Sym4NzpgvPKsDjn_wMkHxcp6CilPcoKrWHcipR2iAjzLvDNAReF97zoJqq880ZD1bwY82JDauCXELVR9O6_B0w3K-E7yM2macAAgNCUwtik6SjoSUZRcf-O5lygIyLENx882p6MtmwaL1hd6qn5RZOQ0TLrOYu0532g9Exxcm-ChymrB4xLykpDj3lUivJt63eEGGN6DH5K6o33TcxkIjNrCD4XB1CKKumZvCedgHHF3IAK4dVEDSUoGlH9z4pP_eWYNXvqQOjGs-rDaQzUHl6cQQWNiDpWOl_lxXjQEvQ", 437, 239, 191, 189)]
        [InlineData("hRYa5zEdzQzI22S4ijgeS0wrdDxVgE5xE0gzMvFHqZU", 54, 239, 239, 189)]
        public void JwtSignatureDecode(string base64, int length, byte first, byte fiftyOneth, byte last)
        {
            var decode = Base64Url.Decode(base64);
            var bytes = new UTF8Encoding(false).GetBytes(decode);
            bytes.Length.Is(length);
            bytes.First().Is(first);
            bytes.Skip(51).First().Is(fiftyOneth);
            bytes.Last().Is(last);
        }

        [Theory]
        [InlineData("eyJraWQiOiIxZTlnZGs3IiwiYWxnIjoiUlMyNTYifQ==", "{\"kid\":\"1e9gdk7\",\"alg\":\"RS256\"}")]
        [InlineData("eyJhbGciOiAiSFMyNTYiLCAidHlwIjogIkpXVCJ9", "{\"alg\": \"HS256\", \"typ\": \"JWT\"}")]
        [InlineData("eyJhbGciOiJub25lIn0=", "{\"alg\":\"none\"}")]
        public void JwtHeaderEncode(string base64, string raw)
        {
            var encode = Base64Url.Encode(raw);
            encode.Is(base64);
        }
    }
}
