using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLC.UtilityModel
{
    [DataContract]
    public class MessageModel
    {
        [DataMember(EmitDefaultValue = false)]
        public String Message { get; set; }
    }
}