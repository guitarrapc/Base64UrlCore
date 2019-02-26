using System;
using System.Text;

namespace Base64Url
{
    public static class Base64Url
    {
        private static Encoding defaultEncoding = new UTF8Encoding(false);

        public static string Encode(string text)
        {
            return Encode(text, defaultEncoding);
        }

        public static string Encode(string text, Encoding enc)
        {
            var bytes = enc.GetBytes(FromBase64(text));
            return Convert.ToBase64String(bytes);
        }

        public static string Decode(string text)
        {
            return Decode(text, defaultEncoding);
        }

        public static string Decode(string text, Encoding enc)
        {
            var base64 = Convert.FromBase64String(ToBase64(text));
            return enc.GetString(base64);
        }

        public static string ToBase64(string base64Url)
        {
            return PadString(base64Url)
                .Replace("-", "+")
                .Replace("_", "/");
        }

        public static string FromBase64(string base64)
        {
            return base64.Replace("=", "")
                .Replace("+", "-")
                .Replace("/", "_");
        }

        public static string PadString(string text)
        {
            var segment = 4;
            var length = text.Length;
            var diff = length % segment;

            if (diff == 0) return text;

            var padLength = segment - diff;
            while (padLength-- != 0)
            {
                text += "=";
            }

            return text;
        }
    }
}
