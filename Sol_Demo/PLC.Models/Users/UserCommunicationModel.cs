using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLC.Models.Users
{
    [DataContract]
    public class UserCommunicationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public String MobileNo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String EmailId { get; set; }
    }
}