using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CFile
    {

        /// <summary>
        /// convert file to byte array
        /// </summary>
        /// <param name="FilePath"> file location</param>
        /// <returns></returns>
        public static byte[] FileToByteArray(string FilePath)
        {
            return System.IO.File.ReadAllBytes(FilePath);

        }


        /// <summary>
        /// Convert Base 64 String to byte array
        /// </summary>
        /// <param name="Base64String"></param>
        /// <returns></returns>
        public static byte[] FromBase64StringToByteArray(string Base64String)
        {
            return Convert.FromBase64String(Base64String);

        }

        /// <summary>
        /// Convert byte array  to Base 64 String
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static string ConvertByteArrayToBase64String(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }

    }
}
