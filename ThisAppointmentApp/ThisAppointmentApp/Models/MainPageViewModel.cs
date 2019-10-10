using Google.Apis.Calendar.v3.Data;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ThisAppointmentApp
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public IcsClient _client;
         
        public MainPageViewModel()
        {
            _client = new IcsClient("https://calendar.google.com/calendar/ical/hugo%40letstalk.nl/public/basic.ics");
            _meetings = _client.Meetings;
            _scheduleAppointments = _client.ScheduleAppointments;
            #region
            //Meetings = new List<Meeting>
            //{
            //    new Meeting(
            //        new Google.Apis.Calendar.v3.Data.EventDateTime() {DateTime = DateTime.Now.AddHours(-2) },
            //        new Google.Apis.Calendar.v3.Data.EventDateTime() {DateTime = DateTime.Now.AddHours(-1) },
            //        new List<EventAttendee>(),
            //        "TestLocation",
            //        "TestMeeting1"
            //    ),
            //     new Meeting(
            //        new Google.Apis.Calendar.v3.Data.EventDateTime() {DateTime = DateTime.Now.AddHours(-1) },
            //        new Google.Apis.Calendar.v3.Data.EventDateTime() {DateTime = DateTime.Now.AddHours(1) },
            //        new List<EventAttendee>(),
            //        "TestLocation",
            //        "TestMeeting2"
            //    ),
            //      new Meeting(
            //        new Google.Apis.Calendar.v3.Data.EventDateTime() {DateTime = DateTime.Now.AddHours(3) },
            //        new Google.Apis.Calendar.v3.Data.EventDateTime() {DateTime = DateTime.Now.AddHours(4) },
            //        new List<EventAttendee>(),
            //        "TestLocation",
            //        "TestMeeting3"
            //    )
            //};
            #endregion
        }

        private List<Meeting> _meetings;
        public List<Meeting> Meetings
        {
            get
            {
                return _meetings;
            }
            private set
            {
                _meetings = value;
                OnPropertyChanged(nameof(Meetings));
            }
        }

        public List<ScheduleAppointment> ScheduleAppointments
        {
            get
            {
                return _scheduleAppointments;
            }
            set
            {
                _scheduleAppointments = value;
                OnPropertyChanged(nameof(ScheduleAppointments));
            }
        }

        private List<ScheduleAppointment> _scheduleAppointments;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
