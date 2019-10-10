using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Interfaces;
using Ical.Net.Interfaces.DataTypes;
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
            using (MemoryStream stream = new MemoryStream(Resource1.Hugo_TestCalendar_letstalk_nl_5viiu8st1brvbgdcamkrdnutrs_group_calendar_google_com))
            {
                calendar = Ical.Net.Calendar.LoadFromStream(stream).First();
            }
            foreach(var calEvent in calendar.Events)
            {               
                IList<Ical.Net.Interfaces.DataTypes.IAttendee> attendees = calEvent.Attendees;
                List<Attendee> attending = new List<Attendee>();
                                
                DateTime dateStart = new DateTime(calEvent.Start.Ticks);
                DateTime dateEnd = new DateTime(calEvent.End.Ticks);
                                   
                foreach (var a in attendees)
                {
                    Attendee attender = new Attendee();                   
                    attender.Name = a.Value.UserInfo;
                    attender.Email = a.CommonName;
                    attending.Add(attender);
                }

                var ev = calEvent.RecurrenceDates;
                var ve = new List<IRecurrencePattern>(calEvent.RecurrenceRules.AsEnumerable());
                var pattern = ve.FirstOrDefault();
                var ex = pattern?.Frequency;
                var ex1 = pattern?.ByDay[0].DayOfWeek;
                //var ex2 = pattern.ByDay.Select(s => s.);
                var ka = calEvent.RecurrenceId;

                if(calEvent.RecurrenceRules.Count == 1)
                {
                    RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
                    recurrenceProperties.RecurrenceType = (RecurrenceType)pattern.Frequency;
                    //recurrenceProperties.WeekDays =; 
                }

                //RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
                //recurrenceProperties.RecurrenceType = ;
                //recurrenceProperties.RecurrenceType = (RecurrenceType)pattern.Frequency;
                //var Recurrency = calEvent.Properties;

                ScheduleAppointment appointment = new ScheduleAppointment() { StartTime = dateStart, EndTime = dateEnd, Subject = calEvent.Summary, IsRecursive = true, };
                
                Meetings.Add(new Meeting(dateStart, dateEnd, attending, calEvent.Location, calEvent.Name, calEvent.Summary));
                ScheduleAppointments.Add(appointment);
                
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
    }
}