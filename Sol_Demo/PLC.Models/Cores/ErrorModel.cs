using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.Models.Cores
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }

        public String Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
