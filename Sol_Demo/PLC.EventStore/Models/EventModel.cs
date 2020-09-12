using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.EventStore.Models
{
    public class EventModel
    {
        public String TransactionId { get; set; }

        public String EventName { get; set; }

        public String Data { get; set; }

        public String CreatedDate { get; set; }
    }
}