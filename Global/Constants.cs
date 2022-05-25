using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Global
{
    public class Constants
    {

        public const string MainLayoutPath = "~/Views/Shared/LayoutMain.cshtml";

        public const string all = "all";

 
        /// <summary>
        /// set constant values for background color
        /// </summary>
        /// <returns></returns>
        #region set constant values for background color for charts 
        public string BackgroundChartsColor()
        {
            var backgroundChartColor =
                             "'rgba(103,58,183,0.4)'," +
                             "'rgba(139,195,74,0.4)'," +
                             "'rgba(233,30,99,0.4)'," +
                             "'rgba(75, 192, 192, 0.2)'," +
                             "'rgba(75, 192, 192, 0.2)'," +
                             "'rgba(233,30,99,0.4)'," +
                             "'rgba(103,58,183,0.4)'," +
                             "'rgba(255, 99, 132, 0.2)'," +
                             "'rgba(128,0,0, 0.2)'," +
                             "'rgba(192,192,192, 0.2)'," +
                             "'rgba(255,0,255, 0.2)'," +
                             "'rgba(54, 162, 235, 0.2)'," +
                             "'rgba(255, 206, 86, 0.2)'," +
                             "'rgba(220,220,220,0.5)'," +
                             "'rgba(0, 255, 0, 0.3)'," +
                             "'rgba(0, 0, 255, 0.3)'," +
                             "'rgba(255, 0, 0, 0.8)'," +
                             "'rgba(128,128,128, 0.8)'," +
                             "'rgba(233,150,122, 0.8)'," +
                             "'rgba(147,112,219, 0.8)'," +
                             "'rgba(153,50,204, 0.8)'," +
                             "'rgba(0,0,128, 0.8)'," +
                             "'rgba(0,128,128, 0.8)'," +
                             "'rgba(102,45,145,1)'";
            return backgroundChartColor;
        }
        #endregion end of set constant values for background color for charts 

        /// <summary>
        /// set constant values for background color for bar charts 
        /// </summary>
        /// <returns></returns>
        #region set constant values for background color for bar charts 
        public string BarBackgroundColor()
        {
            var barBackgroundChartColor =
                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)'," +

                             "'rgba(33, 150, 243, 0.4)',";
            return barBackgroundChartColor;
        }
        #endregion end of set constant values for background color for bar charts 

        /// <summary>
        /// set border color for bar charts
        /// </summary>
        /// <returns></returns>
        #region set border color for bar charts
        public string BarBorderColor()
        {
            var barBorderColor =
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +              
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)'," +
                             "'rgba(33, 150, 243, 1)',";
            return barBorderColor;
        }
        #endregion end of set border color for bar charts
    }
}
