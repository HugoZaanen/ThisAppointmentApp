using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThisAppointmentApp.Models
{
    public class AppointmentModel
    {
        private string _name;
        private DateTime _startTime { get; set; }
        private DateTime _endTime { get; set; }
        private List<Attendee> _attendees {get;set;}
        private string _location { get; set; }
        private ScheduleAppointment _appointment { get; set; }
       
        public AppointmentModel(ScheduleAppointment scheduleAppointment, List<Attendee> attendees)
        {
            _appointment = scheduleAppointment;
            Name = Name;
            StartTime = StartTime;
            EndTime = EndTime;
            Location = Location;
            _attendees = attendees;
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = _appointment.Subject; }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = _appointment.StartTime; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = _appointment.EndTime; }
        }

        public List<Attendee> Attendees
        {
            get { return _attendees; }           
        }
        
        public string Location
        {
            get { return _location; }
            set { _location = _appointment.Location; }
        }

        public ScheduleAppointment ReturnAppointment()
        {
            return _appointment;
        }
    }
}