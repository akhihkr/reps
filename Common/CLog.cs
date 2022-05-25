using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Globalization;

namespace Common
{
    /// <summary>
    /// General logger class
    /// </summary>
    public class CLog
    {


        private static ILog logger;

        /// <summary>
        /// Nedd to config to be able to use
        /// </summary>
        public static void RConfig()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Write exception to file
        /// </summary>
        /// <param name="ex">Exception occured</param>
        /// <param name="ty">Type from which class </param>
        public static void WriteLogErr(Exception ex, Type ty)
        {
            RConfig();
            logger = LogManager.GetLogger(ty);

            logger.Error(string.Format(CultureInfo.InvariantCulture, "Entering Method {0}", ex));


        }

        /// <summary>
        ///  Write string error info to file
        /// </summary>
        /// <param name="info">string message </param>
        /// <param name="ty">Type from which class </param>
        public static void WriteLogInfo(string info, Type ty)
        {
            RConfig();
            logger = LogManager.GetLogger(ty);

            logger.Info(string.Format(CultureInfo.InvariantCulture, "Entering Method {0}", info));

        }


    }
}
