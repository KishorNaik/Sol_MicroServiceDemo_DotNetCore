using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLC.Models.Users
{
    [DataContract]
    public class UserModel
    {
        [DataMember(EmitDefaultValue = false)]
        public String UserIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String LastName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public UserCommunicationModel UserCommunication { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public UserLoginModel UserLogin { get; set; }
    }
}