using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.prowidesoftware.swift.model.mt.mt1xx;
using com.prowidesoftware.swift.model.field;
using java.util;

namespace REPS.UI.Models
{
    public class SwiftModel
    {
        /// <summary>
        /// Get data for the SWIFT file
        /// </summary>
        /// <returns></returns>
        public static string CreateSwiftFile()
        {
            try
            {
                MT103 m = new MT103();

                m.setSender("FOOSEDR0AXXX");
                m.setReceiver("FOORECV0XXXX");

                m.addField(new Field21R("CUSTOMER_REFERENCE"));
                m.addField(new Field28D("MESSAGE_INDEX"));
                m.addField(new Field50A("INSTRUCTING_PARTY"));
                m.addField(new Field52A("ORDERING_CUSTOMER"));
                m.addField(new Field51A("SENDING_INSTITUTION"));
                m.addField(new Field30("EXECUTION_DATE"));
                m.addField(new Field21("TRANSACTION_REFERENCE"));
                m.addField(new Field32B("3000RAND"));
                Field59A f59A = new Field59A()
                    .setAccount("12345678901234567890");
                m.addField(f59A);
                m.addField(new Field71A("OUR"));

                return m.message();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}