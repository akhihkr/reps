using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.ComponentModel;
using System.Globalization;

namespace Common
{
    public class CString
    {
        public static XmlDocument StringToXML(string myXmlString)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(myXmlString); // suppose that myXmlString contains "<Names>...</Names>"

            return xml;

        }

        public static string ConvertToXMLParametersAdd(object classObj, bool bUpdate = false)
        {
            StringBuilder auditparams = new StringBuilder();
            Type myType = classObj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            auditparams.AppendLine("<Parameters>");
            foreach (PropertyInfo prop in props)
            {

                if (!(prop.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))) && prop.GetValue(classObj, null) != null)
                {

                    auditparams.AppendLine("<Param>");
                    auditparams.AppendLine("<Column>" + prop.Name.ToString() + "</Column>");
                    auditparams.AppendLine("<OldValue>" + bUpdate.ToString() + "</OldValue>");
                    auditparams.AppendLine("<NewValue>" + prop.GetValue(classObj, null) + "</NewValue>");
                    auditparams.AppendLine("</Param>");

                }

            }
            auditparams.AppendLine("</Parameters>");
            return auditparams.ToString();
        }

        public string ConvertDataTableToJSON(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }


        public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }


        public static string FirstCharToUpper(string input)
        {

            return input.First().ToString().ToUpper() + String.Join("", input.ToLower().Skip(1));
        }

        //make the first letter capital of any phrase
        //make sure that the string contains space and its split the space
        public static string FirstCharToUpperPhrase(string input)
        {
            //declare variables
            string[] splitPhrasesInput;
            List<object> someStringList = new List<object>();
            //end of declare variables

            //split the string and assign it to variables
            splitPhrasesInput = input.Split(' '); 
            foreach(string word in splitPhrasesInput)
            {
                someStringList.Add(word.First().ToString().ToUpper() + String.Join("", word.ToLower().Skip(1)));
            }
            //end of split the string and assign it to variables
            //CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
            return string.Join(" ", someStringList.ToArray());
        }

        ///// <summary>
        ///// return null or object value
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        public static object ReturnNullOrObject<T>(T value)
        {
            if (typeof(T) == typeof(string) && string.IsNullOrEmpty(value as string))
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(value);
            }
        }
        /// <summary>
        /// return null or object value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static object ReturnNullOrObject<T>(T value)
        //{
        //    if (typeof(T) == typeof(string) && string.IsNullOrEmpty(value as string))
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return (T)(value);
        //    }
        //}

        /// <summary>
        /// return null or object in case object is zero
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ReturnNullOrObjectForZero<T>(T value)
        {
            if (typeof(T) == typeof(string) && string.IsNullOrEmpty(value as string))
            {
                return null;
            }
            else if (typeof(T) == typeof(int) && Convert.ToInt32(value) == 0)
            {
                return null;
            }
            else if (typeof(T) == typeof(decimal) && Convert.ToDecimal(value) == 0)
            {
                return null;
            }
            else
            {
                return (T)(value);
            }
        }

        /// <summary>
        /// return boolean to chk if value is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckNullOrEmpty<T>(T value)
        {

            if (typeof(T) == typeof(string) && string.IsNullOrEmpty(value as string))
            {
                return true;
            }
            else if (typeof(T) == typeof(int) && Convert.ToInt32(value) <= 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// get enum description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
