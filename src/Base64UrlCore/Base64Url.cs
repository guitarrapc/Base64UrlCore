using System;
using System.Text;

namespace Base64UrlCore
{
    public static class Base64Url
    {
        private static Encoding defaultEncoding = new UTF8Encoding(false);

        /// <summary>
        /// Encode from plain utf8string to Base64 url string
        /// </summary>
        /// <param name="utf8string"></param>
        /// <returns></returns>
        public static string Encode(string utf8string) => Encode(utf8string, defaultEncoding);

        /// <summary>
        /// Encode from plain utf8string to Base64 url string
        /// </summary>
        /// <param name="utf8string"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Encode(string utf8string, Encoding enc) 
            => Encode(enc.GetBytes(Escape(utf8string)));

        /// <summary>
        /// Encode from bytes to Base64 url string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Encode(byte[] bytes) => Convert.ToBase64String(bytes);

        /// <summary>
        /// Decode from Base64 url string to plain string
        /// </summary>
        /// <param name="base64string"></param>
        /// <returns></returns>
        public static string Decode(string base64string) => Decode(base64string, defaultEncoding);

        /// <summary>
        /// Decode from Base64 url string to plain string
        /// </summary>
        /// <param name="base64string"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Decode(string base64string, Encoding enc) 
            => Decode(Convert.FromBase64String(Unescape(base64string)), enc);

        /// <summary>
        /// Decode from Base64 url bytes to plain string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Decode(byte[] bytes) => defaultEncoding.GetString(bytes);

        /// <summary>
        /// Decode from Base64 url bytes to plain string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Decode(byte[] bytes, Encoding enc) => enc.GetString(bytes);

        /// <summary>
        /// Fix Plain format to Base64 format
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns></returns>
        public static string Unescape(string base64Url) 
            => PadString(base64Url)
                .Replace("-", "+")
                .Replace("_", "/");

        /// <summary>
        /// Fix Base64 format to Plain format
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string Escape(string base64) 
            => RemovePadding(base64)
                .Replace("+", "-")
                .Replace("/", "_");

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
            var diff = text.Length % segment;

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
        public static string RemovePadding(string text) => text.Replace("=", "");
    }
}
