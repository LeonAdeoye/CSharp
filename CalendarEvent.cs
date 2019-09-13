using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class CalendarEvent
    {
        public string Title
        {
            get;
            set;
        }

        public DateTimeOffset StartTime
        {
            get;
            set;
        }

        public TimeSpan Duration
        {
            get;
            set;
        }

        public static bool TimesOverlap(DateTimeOffset startTime1, TimeSpan duration1, DateTimeOffset startTime2, TimeSpan duration2)
        {
            DateTimeOffset end1 = startTime1 + duration1;
            DateTimeOffset end2 = startTime2 + duration2;

            return (startTime1 < startTime2) ? (end1 > startTime2) : (startTime1 < end2);
        }
    }
}
