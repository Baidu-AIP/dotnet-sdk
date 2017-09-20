/*
 * Copyright 2017 Baidu, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Baidu.Aip
{
    public class Consts
    {
        public static HashSet<string> AipScopes = new HashSet<string>
        {
            "brain_all_scope"
        };
    }

    public class Utils
    {
//        public static bool HasProperty(dynamic obj, params string[] propertys)
//        {
//            return propertys.ToList().TrueForAll(v => obj.GetType().GetProperty(v) != null);
//        }

        /// <summary>
        ///     read stream to string. Will close the steam !
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string StreamToString(Stream ss, Encoding enc)
        {
            string ret;
            using (var reader = new StreamReader(ss, enc))
            {
                ret = reader.ReadToEnd();
            }
            ss.Close();
            return ret;
        }

        /// <summary>
        ///     query dictionary to string
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        public static string ParseQueryString(Dictionary<string, string> querys)
        {
            if (querys.Count == 0)
                return "";
            return querys
                .Select(pair => pair.Key + "=" + pair.Value)
                .Aggregate((a, b) => a + "&" + b);
        }

        /// <summary>
        ///     UriEncode
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encodeSlash"></param>
        /// <returns></returns>
        public static string UriEncode(string input, bool encodeSlash = false)
        {
            var builder = new StringBuilder();
            foreach (var b in Encoding.UTF8.GetBytes(input))
                if (b >= 'a' && b <= 'z' || b >= 'A' && b <= 'Z' || b >= '0' && b <= '9' || b == '_' || b == '-' ||
                    b == '~' || b == '.')
                    builder.Append((char) b);
                else if (b == '/')
                    if (encodeSlash)
                        builder.Append("%2F");
                    else
                        builder.Append((char) b);
                else
                    builder.Append('%').Append(b.ToString("X2"));
            return builder.ToString();
        }

//        /// <summary>
//        /// Bitmap resize
//        /// </summary>
//        /// <param name="image"></param>
//        /// <param name="maxLenth"></param>
//        /// <returns></returns>
//        public static Bitmap ImageSizeLimit(Bitmap image, int maxLenth)
//        {
//            if (image.Width <= maxLenth && image.Height <= maxLenth)
//            {
//                return image;
//            }
//            float scale = Math.Min(maxLenth / image.Width, maxLenth / image.Height);
//            Console.WriteLine("Scale !");
//            var scaleWidth = (int)(image.Width * scale);
//            var scaleHeight = (int)(image.Height * scale);
//            var resized = new Bitmap(image, new Size(scaleWidth, scaleHeight));
//            return resized;
//        }

        /// <summary>
        ///     MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Md5(string text)
        {
            var result = Encoding.Default.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            var output = md5.ComputeHash(result);
            return BitConverter.ToString(output).ToUpper();
        }

        /// <summary>
        ///     Stream to bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Timestamp
        /// </summary>
        /// <returns>timestamp in milliseconds</returns>
        public static long UnixTimestamp()
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long) timeSpan.TotalMilliseconds;
        }
    }
}