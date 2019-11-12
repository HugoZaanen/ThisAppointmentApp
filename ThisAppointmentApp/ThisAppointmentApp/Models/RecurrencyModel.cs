using System;
using System.Collections.Generic;
using System.Text;

namespace ThisAppointmentApp.Models
{
    class RecurrencyModel
    {
        private int _interval { get; set; }
        private List<DayOfWeek> Weekdays { get; set; }
    }
}
