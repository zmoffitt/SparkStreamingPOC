using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAzFunc
{
    public class FlightData
    {
        public string partition_key { get; set; }
        public Flight Flight { get; set; }
        public string id { get; set; }
    }

    public class Flight
    {
        public List<ScheduledFlightLeg> ScheduledFlightLeg { get; set; }
    }

    public class ScheduledFlightLeg
    {
        public DateTimeOffset SchLocalDepDateTime { get; set; }

        public List<CurrentFlightLeg> CurrentFlightLeg { get; set; }

    }
    public class CurrentFlightLeg
    {
        public string TailNumber { get; set; }
    }
}
