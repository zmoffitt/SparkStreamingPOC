using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAzFunc
{
    public class StreamingTimeTurn
    {
        public double TailNumber { get; set; }
        public DateTimeOffset LastUpdate { get; set; }
        public DateTimeOffset SchLocalDepDateTime { get; set; }
        public double PlannedTimeTurn { get; set; }
    }
}
