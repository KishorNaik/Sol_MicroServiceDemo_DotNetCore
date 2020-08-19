using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Models
{
    [DataContract]
    public class AdminLoginModel
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