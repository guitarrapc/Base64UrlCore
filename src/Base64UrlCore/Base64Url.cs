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
        public static string Encode(string utf8string)
        {
            return Encode(utf8string, defaultEncoding);
        }

        /// <summary>
        /// Encode from plain utf8string to Base64 url string
        /// </summary>
        /// <param name="utf8string"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Encode(string utf8string, Encoding enc)
        {
            var bytes = enc.GetBytes(Encode(utf8string));
            return Encode(bytes);
        }

        /// <summary>
        /// Encode from bytes to Base64 url string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

         /// <summary>
        /// Decode from Base64 url string to plain string
        /// </summary>
        /// <param name="base64string"></param>
        /// <returns></returns>
        public static string Decode(string base64string)
        {
            return Decode(base64string, defaultEncoding);
        }

        /// <summary>
        /// Decode from Base64 url string to plain string
        /// </summary>
        /// <param name="base64string"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Decode(string base64string, Encoding enc)
        {
            var base64 = Convert.FromBase64String(Unescape(base64string));
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
        /// Fix Plain format to Base64 format
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns></returns>
        public static string Unescape(string base64Url)
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
        public static string Escape(string base64)
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
