using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class CValidator
    {
        [DataMember]
        public string guid { get; set; }

        [DataMember]
        public string output { get; set; }

        [DataMember]
        public string reason { get; set; }

        [DataMember]
        public bool success { get; set; }

        public static CValidator initValidator(string tid, string toutput, string treason, bool tsuccess)
        {
            CValidator initValidator = new CValidator();
            initValidator.guid = tid;
            initValidator.output = toutput;
            initValidator.reason = treason;
            initValidator.success = tsuccess;
            return initValidator;
        }

        public static CValidator initValidator(string v1, object p, string v2, bool v3)
        {
            throw new NotImplementedException();
        }
    }
}
