using System;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class CArray
    {

        public static int SearchInArray(string needle, string[] haystack)
        {
            int found = 0;

            var UserRole = new JavaScriptSerializer().Deserialize<dynamic>(needle);

            for (int i = 0; i < UserRole.Length; i++)
            {
                if (Array.IndexOf(haystack, UserRole[i]["Code"]) >= 0)
                {
                    found = 1;
                    break;
                }

            }

            return found;

        }

        /// <summary>
        /// Check if the user has permission to do this action
        /// </summary>
        /// <param name="role_name"></param>
        /// <returns></returns>
        public static bool SearchInCookie(string role_name)
        {
            bool found = false;
            HashSet<string> hashRoles = new HashSet<string>();
            var hashRolesAct = new JavaScriptSerializer().Deserialize<dynamic>(HttpContext.Current.Request.Cookies["UserRolesActions"].Value);
            foreach (var role in hashRolesAct as IEnumerable<dynamic>)
            {
                hashRoles.Add(role); // We add the roles to the hashset
            }
            if (hashRoles.Contains(role_name))
            {
                found = true;
            }
            return found;
        }
    }
}
