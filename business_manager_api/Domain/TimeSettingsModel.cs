using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace business_manager_api.Domain
{
    public class TimeSettingsModel
    {
        public int Id { get; set; }

        public DayOfWeek Weekday { get; set; }

        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }

        public DateTime CurrentDate { get; set; }

    }
}
