using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class Notifications
    {

        public static string GetToastrMessage(string messageType, string message)
        {
            //return "toastr." + messageType +@"('" + message +@"')";
            return "displayMessage" + @"('" + message + @"'" + "," + @"'" + messageType + @"')";
        }

    }
}
