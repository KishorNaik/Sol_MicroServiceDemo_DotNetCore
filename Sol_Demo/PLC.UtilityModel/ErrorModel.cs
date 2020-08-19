using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLC.UtilityModel
{
    [DataContract]
    public class ErrorModel
    {
        [DataMember(EmitDefaultValue = false)]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}