using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class ViewDetails
    {
        public static string GenerateControllerViewLink(string actionName, string controllerName)
        {
            actionName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(actionName.ToLower());
            return controllerName + "/" + actionName;
        }
    }
}
