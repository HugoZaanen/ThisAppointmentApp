using Google.Apis.Calendar.v3.Data;
using Ical.Net.Interfaces.DataTypes;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;

namespace ThisAppointmentApp
{
    public class Meeting
    {
        private string location;
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public IList<Attendee> Attendees { get; private set; }
        public string Location
        {
            get { return location; }
            private set {
                location = value ?? "NVA";
            }
        }
        public string Name
        {
            get 
            { 
                return _name; 
            }
            private set 
            {
                if (value.ToString() == "VEVENT")
                {
                    _name = Summary == null ? Summary: "Bleekstraat 3";
                }
                else
                {
                    _name = value;
                }
            }
        }
        
        public string _name;
        public string Summary { get; private set; }

        public string StartEnd { get { return Start.ToString("hh:mm") + "-" + End.ToString("hh:mm"); } }
        
        public Meeting(DateTime start, DateTime end, IList<Attendee> attendees, string location, string name,string summary)
        {
            Start = start;
            End = end;
            Attendees = attendees;
            Location = location;
            Summary = summary;
            Name = name;
        }

        public override string ToString()
        {
            return _name + "\n" + Location + "\n" + Start.ToString() + "/" + End.ToString();
        }               
    }
}
