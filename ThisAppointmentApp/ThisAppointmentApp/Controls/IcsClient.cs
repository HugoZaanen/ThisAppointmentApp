using Ical.Net;
using Ical.Net.DataTypes;
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
using ThisAppointmentApp.Models;

namespace ThisAppointmentApp
{
    public class IcsClient
    {
        private Ical.Net.Calendar calendar;
        
        public IcsClient(string url)
        {
            using (MemoryStream stream = new MemoryStream(Resource1.HugoCalender))
            {
                calendar = Ical.Net.Calendar.Load(stream);
            }
            
            foreach (var calEvent in calendar.Events) 
            {               
                if ((calEvent.Start.Year == DateTime.Now.Year && calEvent.RecurrenceRules.Count > 0) || calEvent.Start.Year == DateTime.Now.Year)
                {
                    IList<Ical.Net.DataTypes.Attendee> attendees = calEvent.Attendees;
                    DateTime dateStart = new DateTime(calEvent.Start.Ticks);
                    DateTime dateEnd;
                                       
                    if (calEvent.End != null)
                    {
                        dateEnd = new DateTime(calEvent.End.Ticks);
                    }
                    else
                    {
                        dateEnd = new DateTime(calEvent.Start.Ticks);
                    }

                    ScheduleAppointment appointment;

                    foreach (var a in attendees)
                    {
                        Attendee attender = new Attendee();
                        attender.Name = a.Value.UserInfo;
                        attender.Email = a.CommonName;
                        attending.Add(attender);
                    }

                    if (calEvent.RecurrenceRules != null && calEvent.RecurrenceRules.Count != 0)
                    {
                        var RecRule = new List<RecurrencePattern>(calEvent.RecurrenceRules.AsEnumerable());
                        var pattern = RecRule.FirstOrDefault();
                        var pat  = pattern.ByDay;
                        var pat1 = pattern.ByMonthDay;
                        var pat2 = pattern.Calendar;                       
                        var pat4 = pattern.Frequency;
                        var pat5 = pattern.Interval;

                        string stringPattern = pattern?.ToString();
                        var ex = pattern?.Frequency;

                        RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
                        recurrenceProperties.RecurrenceType = (RecurrenceType)calEvent.RecurrenceRules[0].Frequency;
                       
                        int[] dayInts = new int[pattern.ByDay.Count];

                        for (int i = 0; i < pattern.ByDay.Count; i++)
                        {
                            recurrenceProperties.WeekDays = (WeekDays)pattern.ByDay[i].DayOfWeek;
                        }
                       
                        appointment = new ScheduleAppointment() { StartTime = dateStart, EndTime = dateEnd, Subject = calEvent.Summary, IsRecursive = true, RecurrenceRule = stringPattern, Location = calEvent.Location};
                        AppointmentModels.Add(new AppointmentModel(new ScheduleAppointment() { StartTime = dateStart, EndTime = dateEnd, Subject = calEvent.Summary, IsRecursive = true, RecurrenceRule = stringPattern, Location = calEvent.Location },attending));
                    }
                    else
                    {
                        appointment = new ScheduleAppointment() { StartTime = dateStart, EndTime = dateEnd, Subject = calEvent.Summary, IsRecursive = true, Location = calEvent.Location };
                        AppointmentModels.Add(new AppointmentModel(new ScheduleAppointment() { StartTime = dateStart, EndTime = dateEnd, Subject = calEvent.Summary, IsRecursive = true, Location = calEvent.Location },attending));
                    }

                    ScheduleAppointments.Add(appointment);
                }
            }
        }

        public List<Meeting> Meetings { get; } = new List<Meeting>();
        public List<ScheduleAppointment> ScheduleAppointments { get; } = new List<ScheduleAppointment>();
        public List<AppointmentModel> AppointmentModels = new List<AppointmentModel>();
        
        public List<AppointmentModel> GetModels()
        {
            return AppointmentModels;
        }

        public List<Attendee> attending = new List<Attendee>();
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