using System;
using System.Text;

namespace Base64Core
{
    public static class Base64Url
    {
        private static Encoding defaultEncoding = new UTF8Encoding(false);

        /// <summary>
        /// Encode from plain text to Base64 url string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encode(string text)
        {
            return Encode(text, defaultEncoding);
        }

        /// <summary>
        /// Encode from plain text to Base64 url string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Encode(string text, Encoding enc)
        {
            var bytes = enc.GetBytes(FromBase64(text));
            return Encode(bytes);
        }

        /// <summary>
        /// Encode from plain text bytes to Base64 url string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Encode from plain text to Base64 url bytes
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] EncodeBytes(string text)
        {
            return EncodeBytes(text, defaultEncoding);
        }

        /// <summary>
        /// Encode from plain text to Base64 url bytes
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static byte[] EncodeBytes(string text, Encoding enc)
        {
            var bytes = enc.GetBytes(FromBase64(text));
            return bytes;
        }

        /// <summary>
        /// Decode from Base64 url string to plain string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decode(string text)
        {
            return Decode(text, defaultEncoding);
        }

        /// <summary>
        /// Decode from Base64 url string to plain string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Decode(string text, Encoding enc)
        {
            var base64 = Convert.FromBase64String(ToBase64(text));
            return Decode(base64, enc);
        }

        /// <summary>
        /// Decode from Base64 url bytes to plain string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Decode(byte[] bytes, Encoding enc)
        {
            return enc.GetString(bytes);
        }

        /// <summary>
        /// Decode from Base64 url string to plain bytes
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] DecodeBytes(string text)
        {
            return DecodeBytes(text, defaultEncoding);
        }

        /// <summary>
        /// Decode from Base64 url string to plain bytes
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static byte[] DecodeBytes(string text, Encoding enc)
        {
            var base64 = Convert.FromBase64String(ToBase64(text));
            return base64;
        }

        /// <summary>
        /// Fix Plain format to Base64 format
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns></returns>
        public static string ToBase64(string base64Url)
        {
            return PadString(base64Url)
                .Replace("-", "+")
                .Replace("_", "/");
        }

        /// <summary>
        /// Fix Base64 format to Plain format
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string FromBase64(string base64)
        {
            return RemovePadding(base64)
                .Replace("+", "-")
                .Replace("/", "_");
        }

        /// <summary>
        /// Padding = to base64 string when it missing.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PadString(string text)
        {
            // shorthand way:
            // base64String.PadRight(base64String.Length + (4 - base64String.Length % 4) % 4, '=');

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

        /// <summary>
        /// Remove = (padding) from Base64 string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemovePadding(string text)
        {
            return text.Replace("=", "");
        }
    }
}
