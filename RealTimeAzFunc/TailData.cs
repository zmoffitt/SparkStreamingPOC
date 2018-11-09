using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAzFunc
{
    public class TailData
    {
        public string TailNumber { get; set; }
        public string FlightLegSeqNumber { get; set; }
        public DateTimeOffset SchLocalDepDateTime { get; set; }
    }

    public class SFL
    {
        public string FlightLegSeqNumber { get; set; }
        public DateTimeOffset SchLocalDepDateTime { get; set; }
    }
}
