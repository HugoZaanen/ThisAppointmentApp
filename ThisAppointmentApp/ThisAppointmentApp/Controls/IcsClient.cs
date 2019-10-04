using Ical.Net;
using Ical.Net.Interfaces;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ThisAppointmentApp
{
    public class IcsClient
    {
        private ICalendar calendar;

        public IcsClient(string url)
        {
            using (MemoryStream stream = new MemoryStream(Resource1.Meetings_letstalk_nl_9e0i37n5ka7p3f3rnc4ucr52tg_group_calendar_google_com))
            {
                calendar = Ical.Net.Calendar.LoadFromStream(stream).First();
            }
            foreach(var calEvent in calendar.Events)
            {
                if (calEvent.Start.Year == DateTime.Now.Year && calEvent.Start.Month == DateTime.Now.Month)
                {
                    IList<Ical.Net.Interfaces.DataTypes.IAttendee> attendees = calEvent.Attendees;
                    List<Attendee> attending = new List<Attendee>();
                                      
                    DateTime dateStart = new DateTime();
                    DateTime dateEnd = new DateTime();
                
                try
                {
                   dateStart = DateTime.Parse(calEvent.Start.ToString().Remove(calEvent.Start.ToString().IndexOf('M') + 1));
                   dateEnd = DateTime.Parse(calEvent.End.ToString().Remove(calEvent.End.ToString().IndexOf('M') + 1));
                }
                catch(Exception ex)
                {
                    dateStart = calEvent.End == null ? DateTime.Parse(calEvent.Start.ToString().Remove(calEvent.Start.ToString().IndexOf('M') + 1)) : DateTime.Parse(calEvent.Start.ToString());
                    dateEnd = calEvent.End == null ? dateStart.AddHours(1) : DateTime.Parse(calEvent.End.ToString());
                }   
                
                foreach (var a in attendees)
                {
                    Attendee attender = new Attendee();
                    attender.Name = a.Value.UserInfo;
                    attender.Email = a.CommonName;
                    attending.Add(attender);
                }
                    Meetings.Add(new Meeting(dateStart, dateEnd, attending, calEvent.Location, calEvent.Name, calEvent.Summary));
                    ScheduleAppointments.Add(new ScheduleAppointment() { StartTime = dateStart, EndTime = dateEnd, Subject = calEvent.Summary });
                }
            }                     
        }

        public List<Meeting> Meetings { get; } = new List<Meeting>();
        public List<ScheduleAppointment> ScheduleAppointments { get; } = new List<ScheduleAppointment>();

        public Meeting GetCurrentMeeting()
        {
            Meeting meeting;

            foreach(var meet in Meetings)
            {
                if(meet.Start.Date >= DateTime.Now)
                {
                    return meeting = meet;
                }
            }

            return null;
        }

        public int Whitespacing(string s)
        {
            int count = 0;
            int index = 0;
            foreach (char c in s)
            {
                count++;
                if (c == ' ')
                {
                    index = count;
                }
            }

            return index;
        }
    }
}
