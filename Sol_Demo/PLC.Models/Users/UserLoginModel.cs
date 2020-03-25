using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLC.Models.Users
{
    [DataContract]
    public class UserLoginModel
    {
        [DataMember(EmitDefaultValue = false)]
        public String UserName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Password { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Salt { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Hash { get; set; }
    }
}