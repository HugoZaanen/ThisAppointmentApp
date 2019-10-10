using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThisAppointmentApp.Controls
{
    public sealed class Calendar
    {
        private static Calendar instance = null;
        private IcsClient _client;
        List<Meeting> _meetings;
        List<ScheduleAppointment> _scheduleAppointments;
        
        public Calendar()
        {
            _client = new IcsClient("https://calendar.google.com/calendar/ical/hugo%40letstalk.nl/public/basic.ics");
            _meetings = _client.Meetings;
            _scheduleAppointments = _client.ScheduleAppointments;
        }

        public static Calendar Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Calendar();
                }
                return instance;
            }
        }

        public void LoadICal(string file)
        {

        }

        AppointmentView _appointmentView;
        public AppointmentView CurrentAppointment
        {
            get
            {
                return _appointmentView;
            }
            set
            {
                _appointmentView = new AppointmentView();
            }
        }



        public List<Meeting> ReturnMeetings()
        {
            return _meetings;
        }
    }
}
