using PLC.Admin.Cryptography.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Models
{
    [DataContract]
    public class AdminModel
    {
        [DataMember(EmitDefaultValue = false)]
        public String AdminIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String LastName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String EmailId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Role { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AdminLoginModel AdminLogin { get; set; }
    }
}