////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Author          : HKR
/// Purpose         : Dynamic sql for Entity Framework. When Entity resulset is unknow you can use the following class
/// Date Created    : 02Aug 2016
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CDynamicSqlRow
    {
        #region chart data from store procedure
        /// <summary>
        /// Get the unknown Column and value from Expando Object function at Runtime
        /// </summary>
        /// <param name="sql">QUery to be passed</param>
        /// <param name="context">Database context in use</param>
        /// <returns></returns>
        public static IEnumerable<dynamic> GetDynamicSql(string sql, object context)
        {

            var oc = (context as IObjectContextAdapter).ObjectContext;
            IDbConnection conn = (oc.Connection as EntityConnection).StoreConnection;
            conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                using (var reader = (DbDataReader)cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return SqlDataReaderToExpando(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Represents an object whose members can be dynamically added and removed at run time as we do not know resultset
        /// </summary>
        /// <param name="reader">Sql reader pointer to data</param>
        /// <returns></returns>
        public static dynamic SqlDataReaderToExpando(IDataReader reader)
        {
            var expandoObject = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;
            for (var i = 0; i < reader.FieldCount; i++)
            {
                expandoObject.Add(reader.GetName(i), reader[i]);
            }

            return expandoObject;
        }
        #endregion end chart data from store procedure

        /// <summary>
        /// get unkwnon colunm and value
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        #region table data from store procedure
        public static IEnumerable<dynamic> CollectionFromSql(object dbContext, string Sql)
        {
            var oc = (dbContext as IObjectContextAdapter).ObjectContext;
            IDbConnection conn = (oc.Connection as EntityConnection).StoreConnection;
            conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = Sql;
                var retObject = new List<dynamic>();
                using (var dataReader = (DbDataReader)cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;

                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private static dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
            return dataRow;
        }
        #endregion end table data from store procedure

        /// <summary>
        /// get values in list form
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="valueSpecChars"></param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> DecodeSerialize(string queryString)
        {
            //variables
            List<dynamic> list = new List<dynamic>();
            List<dynamic> matchValueCollection = new List<dynamic>();
            Dictionary<string, dynamic> formObjects = new Dictionary<string, dynamic>();
            var parameterForm = "";
            var parameterValue = "";
            var matchValue = "";
            string[] parameterString;
            string[] matchString;
            //end of variables

            var splitResultAmpersit = queryString.Split('&');

            int i = 0;
            while (i < splitResultAmpersit.Count())
            {
                list.Add(splitResultAmpersit[i]);
                i++;
            }

            ///add list to formobject
            foreach (var paramCheck in list)
            {
                parameterString = paramCheck.Split('=');
                parameterValue = parameterString[1];
                parameterForm = parameterString[0];
                if (parameterValue.Contains("+"))//check if (value contains + sign for date time), replace with space 
                {
                    parameterValue = parameterValue.Replace("+", " ");
                }
                //check if keys is duplicated in list
                var match = list.Where(item => item.Contains(parameterForm));

                if (match.Count() > 1)//if duplicates then add values to list 
                {
                    foreach (var matchValues in match)
                    {
                        //do something with the matched items
                        matchString = matchValues.Split('=');
                        matchValue = matchString[1];
                        matchValueCollection.Add(matchValue);
                    }
                    formObjects.Add(parameterForm, string.Join(",", matchValueCollection.ToArray()));
                    break;
                }
                else
                {
                    formObjects.Add(parameterForm, parameterValue);
                }
            }
            ///end check parameters
            return formObjects;
        }


        /// <summary>
        /// get values in list form
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="valueSpecChars"></param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> DecodeSerializes(string queryString)
        {
            //variables
            List<dynamic> list = new List<dynamic>();
            List<dynamic> matchValueCollection = new List<dynamic>();
            Dictionary<string, dynamic> formObjects = new Dictionary<string, dynamic>();
            var parameterForm = "";
            var parameterValue = "";
            string[] parameterString;
            //end of variables

            var splitResultAmpersit = queryString.Split('&');

            int i = 0;
            while (i < splitResultAmpersit.Count())
            {
                list.Add(splitResultAmpersit[i]);
                i++;
            }

            ///add list to formobject
            foreach (var paramCheck in list)
            {
                parameterString = paramCheck.Split('=');
                parameterValue = parameterString[1];
                parameterForm = parameterString[0];
                if (parameterValue.Contains("+"))//check if (value contains + sign for date time), replace with space 
                {
                    parameterValue = parameterValue.Replace("+", " ");
                }

                formObjects.Add(parameterForm, parameterValue);
            }
            ///end check parameters
            return formObjects;
        }

        /// <summary>
        /// get values from Dictionary()
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <returns></returns>
        public static dynamic DictionaryValues(Dictionary<string, dynamic> FormObjects)
        {
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (var kvp in FormObjects)
            {
                eoColl.Add(kvp);
            }
            dynamic eoDynamic = eo;
            return eoDynamic;
        }
    }
}
