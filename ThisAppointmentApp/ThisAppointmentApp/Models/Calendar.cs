using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using ThisAppointmentApp.Models;

namespace ThisAppointmentApp.Controls
{
    public sealed class Calendar
    {
        private static Calendar instance = null;
        private IcsClient _client;
        List<AppointmentModel> appointments = new List<AppointmentModel>();
        
        public Calendar()
        {
            _client = new IcsClient("https://calendar.google.com/calendar/ical/hugo%40letstalk.nl/public/basic.ics");
            //appointments.Add(_client.GetModels());           
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
        
        public 
    }
}
