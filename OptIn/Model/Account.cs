using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptIn.Model
{
    public class Account : EntityBase
    {
        public Account()
        {
            this.Events = new List<Event>();
        }
        public int HouseHoldId { get; set; }
        public List<Event> Events { get; set; }
        public Event LastEvent { get; set; }
    }
}
