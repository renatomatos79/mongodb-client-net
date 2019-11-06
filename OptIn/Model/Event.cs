using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptIn.Model
{
    public class Event : EntityBase
    {
        public string DeviceID { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Enabled { get; set; }
    }
}
