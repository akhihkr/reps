using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CUniqueReference
    {
        public static string GetUniqueReferenceAjaxRequest(string uniqueReferenceURL)
        {
            if (uniqueReferenceURL != string.Empty)
            {
                // UR = Unique Reference
                string UniqueReference = string.Empty; // We set an empty UR variable
                if (!string.IsNullOrEmpty(uniqueReferenceURL) && uniqueReferenceURL.ToLower().Contains("uref=")) // We check if the URL contains the UR
                {
                    if (uniqueReferenceURL.ToLower().Split(new[] { "uref=" }, StringSplitOptions.None)[1].Length > 24) // If the UR's length is less than 25 then it is not in the appropriate format
                    {
                        string refURL = uniqueReferenceURL.ToLower().Split(new[] { "uref=" }, StringSplitOptions.None)[1].Substring(0, 25); // We get the UR from the URL
                        string checkSumURL = refURL.Substring(23).ToUpper(); // We take only the last 2 digits (checksum) in the reference
                        string referenceWithoutChecksum = refURL.Substring(0, 23); // We take the whole unique ref without the checksum digits

                        string checkSumCheck = CUuid.GetCheckSum(referenceWithoutChecksum.ToUpper()); // We get the checksum of the unique ref
                        if (checkSumCheck == checkSumURL) // We compare the 2 checksums and see if they match
                        {
                            return refURL;
                        }
                        else // Checksums do not match
                        {
                            return Enums.UniqueReference.Invalid.ToString();
                        }

                    }
                    else // Unique ref is less than 25
                    {
                        return Enums.UniqueReference.Invalid.ToString();
                    }
                }
                else // If parameter doesn't contain "uref"
                {
                    return Enums.UniqueReference.Invalid.ToString();
                }
            }
            else // If parameter is empty
            {
                return Enums.UniqueReference.Invalid.ToString();
            }
        }

        public static string GetUniqueReferenceNonAjaxRequest(string uniqueReferenceURL)
        {
            string uniqueReference = string.Empty; // We set an empty UR variable
            if (!string.IsNullOrEmpty(uniqueReferenceURL)) // We check if the URL contains the UR
            {
                if (uniqueReferenceURL.Length > 24)
                {
                    string refURL = uniqueReferenceURL.Substring(0, 25);
                    string checkSumURL = refURL.Substring(23).ToUpper();
                    string referenceWithoutChecksum = refURL.Substring(0, 23);

                    string checkSumCheck = CUuid.GetCheckSum(referenceWithoutChecksum.ToUpper());
                    if (checkSumCheck == checkSumURL)
                    {
                        return refURL;
                    }
                    else // Checksums do not match
                    {
                        return Enums.UniqueReference.Invalid.ToString();
                    }
                }
                else // Unique ref is less than 25
                {
                    return Enums.UniqueReference.Invalid.ToString();
                }
            }
            else // If parameter is empty
            {
                return Enums.UniqueReference.Invalid.ToString();
            }

        }
    }
}