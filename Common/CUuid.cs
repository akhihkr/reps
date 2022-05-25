using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CUuid
    {


        public static string GeneratedealId()
        {

            Random _random = new Random();
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            //return string.Format("d" + DateTime.Now.ToString("yyMMdd") + "{0:x}", i - DateTime.Now.Ticks).ToUpper();
            string generatedID = string.Format("d" + DateTime.Now.ToString("yyMMdd") + "{0:x}", i - DateTime.Now.Ticks).ToUpper();
            if (generatedID.Length != 23)
            {
                if (generatedID.Length < 23)
                {
                    while (generatedID.Length < 23)
                    {
                        int num = _random.Next(0, 26);
                        char letter = (char)('a' + num);
                        generatedID += letter.ToString().ToUpper();
                    }
                }
                else
                {
                    generatedID = generatedID.Substring(0, 23);
                }
            }

            string checkSumString = GetCheckSum(generatedID);

            return generatedID + checkSumString; // Add the checksum to the Ref

            //return string.Format("d" + DateTime.Now.ToString("yyMMdd") + "{0:x}", Guid.NewGuid().ToString("N"));
            //return string.Format("d" + DateTime.Now.ToString("yyMMdd") + "{0:x}", Guid.NewGuid().GetHashCode().ToString("x"));
        }

        public static string GetCheckSum(string refURL)
        {
            #region check sum calculation

            byte[] byteToCalculate = Encoding.ASCII.GetBytes(refURL);
            int checksum = 0;
            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }
            checksum &= 0xff;
            return checksum.ToString("X2");

            #endregion check sum calculation
        }

        private string GetUniqueKey()
        {
            int maxSize = 8;
            //int minSize = 5;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                // result.Append(chars[__b % (chars.Length - )); 
            }
            return result.ToString();
        }


    }
}
