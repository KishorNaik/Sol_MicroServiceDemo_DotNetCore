using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.EventStore.Models
{
    public class EventModel
    {
        public String TransactionId { get; set; }

        public String EventName { get; set; }

        public String OldData { get; set; }

        public String NewData { get; set; }

        public String CreatedDate { get; set; }
    }
}