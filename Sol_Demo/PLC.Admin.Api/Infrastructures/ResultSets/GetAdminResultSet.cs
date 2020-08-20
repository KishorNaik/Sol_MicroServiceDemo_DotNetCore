using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Infrastructures.ResultSets
{
    public class GetAdminResultSet
    {
        public String AdminIdentity { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String EmailId { get; set; }

        public String Role { get; set; }

        public String UserName { get; set; }

        public String Salt { get; set; }

        public String Hash { get; set; }
    }
}