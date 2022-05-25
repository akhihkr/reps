using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Check input forms values if correct
    /// </summary>
    public class CSanitize
    {

        #region static variables

            private static Regex sUserNameAllowedRegEx = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$", RegexOptions.Compiled);
            private static Regex sUserNameIllegalEndingRegEx = new Regex(@"(\.|\-|\._|\-_)$", RegexOptions.Compiled);
            //password with 1 digit 1 caps and size = 8
            //private static Regex sPasswordAllowedRegEx = new Regex(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$", RegexOptions.Compiled);
            //password with 1 digit 1 lower 1 caps and minsize = 4
            private static Regex sPasswordAllowedRegEx = new Regex(@"^(?=(.*\d))(?=.*[a-z])(?=.*[A-Z])(.{4,})$", RegexOptions.Compiled);

        #endregion

        #region variables

           

        #endregion

        /// <summary>
        /// check  username have correct characters
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>valid or not</returns>
        public static bool sanitizedUsername(String userName)
        {           
            
                if (string.IsNullOrEmpty(userName)
                    || !sUserNameAllowedRegEx.IsMatch(userName)
                    || sUserNameIllegalEndingRegEx.IsMatch(userName))
                {
                    return false;
                }
                return true;
            
        }

        /// <summary>
        /// Check password have correct characters
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns>valid or not</returns>
        public static bool sanitizedPassword(String userPassword)
        {
            if (string.IsNullOrEmpty(userPassword)
                    || !sPasswordAllowedRegEx.IsMatch(userPassword))
                {
                    return false;
                }
                return true;
        }
 
    }


}
