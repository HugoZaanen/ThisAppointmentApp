using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ThisAppointmentApp.Models
{
    public sealed class Calendar
    {
        private static Calendar instance = null;
        private IcsClient _client;
        List<AppointmentModel> appointments = new List<AppointmentModel>();
        Dictionary<string, DayOfWeek> stringToDay = new Dictionary<string, DayOfWeek>();
        List<string> datesString = new List<string>();
        public List<ScheduleAppointment> scheduleAppointments = new List<ScheduleAppointment>();

        char[] chars = { ';', '=' };
        string days;
        int interval = 1;
        int count = 100;
        int monthDay = 0;
        int weekNum = 0;
        int dail = 0;
        int NumOfMonthDay = 0;
        string day = null;

        public Calendar()
        {
            stringToDay.Add("MO", DayOfWeek.Monday);
            stringToDay.Add("TU", DayOfWeek.Tuesday);
            stringToDay.Add("WE", DayOfWeek.Wednesday);
            stringToDay.Add("TH", DayOfWeek.Thursday);
            stringToDay.Add("FR", DayOfWeek.Friday);
            _client = new IcsClient("https://calendar.google.com/calendar/ical/hugo%40letstalk.nl/public/basic.ics");

            foreach (var model in _client.GetModels())
            {
                appointments.Add(model);
            }

            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].RequerencePattern != null)
                {
                    foreach (var appointment in CreateRecurringAppointments(appointments[i]))
                    {                        
                        appointments.Add(appointment);
                    }
                }
            }

            scheduleAppointments = _client.ScheduleAppointments;
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

        public List<AppointmentModel> GetTodayAppointments()
        {
            var listAppointmentsModel = (from appointment in appointments where appointment.StartTime.Year == DateTime.Now.Year && appointment.StartTime.Month == DateTime.Now.Month && appointment.StartTime.Day == DateTime.Now.Day select appointment).ToList();                                 
            return listAppointmentsModel;
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

        public List<AppointmentModel> GetAppointments
        {
            get { return appointments; }
        }

        public List<AppointmentModel> CreateRecurringAppointments(AppointmentModel model)
        {
            string pattern = model.RequerencePattern;
            var charPattern = model.RequerencePattern.Split(chars);
            var duration = model.EndTime.Ticks - model.StartTime.Ticks;
            var span = new TimeSpan(duration);
            List<string> dayStrings = new List<string>();
            string s = charPattern[1];
            DateTime date = model.StartTime;

            List<AppointmentModel> RecurrenceAppointments = new List<AppointmentModel>();
            List<DateTime> dates = new List<DateTime>();

            if (pattern.Contains("COUNT"))
            {
                for (int i = 0; i < charPattern.Length - 1; i++)
                {
                    if (charPattern[i] == "COUNT")
                    {
                        count = int.Parse(charPattern[i + 1]);
                    }
                }
            }
            if (pattern.Contains("INTERVAL"))
            {
                for (int i = 0; i < charPattern.Length - 1; i++)
                {
                    if (charPattern[i] == "INTERVAL")
                    {
                        interval = int.Parse(charPattern[i + 1]);
                    }
                }
            }
            if (s == "WEEKLY")
            {
                for (int k = 0; k < charPattern.Length; k++)
                {
                    if (charPattern[k] == "BYDAY")
                    {
                        dayStrings = charPattern[k + 1].Split(',').ToList();
                    }
                }
            }
            if (s == "DAILY")
            {
                for (int i = 1; i < count; i++)
                {
                    RecurrenceAppointments.Add(CreateRecurrentAppointment(model, interval, duration, i));
                }
            }
            else if (s == "WEEKLY")
            {
                DateTime placehDate = model.StartTime;
                for (int i = 1; i < count; i++)
                {
                    int dayNum = (int)placehDate.DayOfWeek;
                    placehDate = placehDate.AddDays(interval * 7);
                   
                    foreach (var str in dayStrings)
                    {
                        var dayNumWeek = (int)stringToDay[str];
                        int detractNum = dayNum - dayNumWeek;
                        if (!(placehDate.Day - detractNum <= 0))
                        {
                            DateTime recurrenceDate = new DateTime(placehDate.Year, placehDate.Month, placehDate.Day - detractNum, placehDate.Hour, placehDate.Minute, placehDate.Second);
                            var da = recurrenceDate.ToString();
                            datesString.Add(da);
                            RecurrenceAppointments.Add(CreateRecurrentAppointment(model, interval, duration, i, recurrenceDate));
                        }
                    }
                }
            }
            else
            {
                DateTime placedHolder = model.StartTime;
                if (pattern.Contains("BYMONTHDAY"))
                {
                    for (int i = 0; i < charPattern.Length; i++)
                    {
                        if (charPattern[i] == "BYMONTHDAY")
                        {
                            monthDay = int.Parse(charPattern[i + 1]);
                        }
                    }

                    for (int i = 0; i < count; i++)
                    {
                        placedHolder = placedHolder.AddMonths(interval);
                        RecurrenceAppointments.Add(CreateRecurrentAppointment(model, interval, duration, i));
                    }
                }
                else
                {
                    for (int i = 0; i < charPattern.Length; i++)
                    {
                        if (charPattern[i] == "BYDAY")
                        {
                            monthDay = int.Parse(charPattern[i + 1].Substring(0, 1));
                            day = charPattern[i + 1].Substring(1, 2);
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        dail = 0;
                        placedHolder = placedHolder.AddMonths(interval);
                        for (int j = 1; j <= DateTime.DaysInMonth(placedHolder.Year, placedHolder.Month); j++)
                        {
                            dail += new DateTime(placedHolder.Year, placedHolder.Month, j).DayOfWeek == stringToDay[day] ? 1 : 0;

                            if (dail == monthDay)
                            {
                                dail = j;
                                break;
                            }
                        }
                        placedHolder = new DateTime(placedHolder.Year, placedHolder.Month, dail, placedHolder.Hour, placedHolder.Minute, placedHolder.Second);
                        var da = new DateTime(placedHolder.Year, placedHolder.Month, dail, placedHolder.Hour, placedHolder.Minute, placedHolder.Second);
                        RecurrenceAppointments.Add(CreateRecurrentAppointment(model, interval, duration, i, placedHolder));
                    }
                }
            }
            count = 100;
            return RecurrenceAppointments;
        }

        public AppointmentModel CreateRecurrentAppointment(AppointmentModel model, int interval, long duration, int i)
        {
            AppointmentModel mod = new AppointmentModel();
            mod.Name = model.Name;
            mod.StartTime = model.StartTime.AddDays(interval * i);
            mod.EndTime = model.EndTime.AddDays(interval * i).AddTicks(duration);
            mod.Attendees = model.Attendees;
            mod.Location = model.Location;
            return mod;
        }

        public AppointmentModel CreateRecurrentAppointment(AppointmentModel model, int interval, long duration, int i, DateTime date)
        {
            AppointmentModel mod = new AppointmentModel();
            mod.Name = model.Name;
            mod.StartTime = date;
            mod.EndTime = date.AddTicks(duration);
            mod.Attendees = model.Attendees;
            mod.Location = model.Location;
            return mod;
        }

        public AppointmentModel returnNowAppointment()
        {                      
            return (from app in appointments where app.StartTime.Year == DateTime.Now.Year && app.StartTime.Month == DateTime.Now.Month && app.StartTime.Day == DateTime.Now.Day && (app.StartTime.Hour >= DateTime.Now.Hour && app.EndTime.Hour <= DateTime.Now.Hour) select app).First();
        } 

        //public List<AppointmentModel> WeekOrganizer(DayOfWeek d)
        //{            
        //    var day = DateTime.Now.Day;

                
        //    return (from app in appointments where app.StartTime.Year == DateTime.Now.Year && app.StartTime.Month == DateTime.Now.Month && app.StartTime.Day ==  );
        //}
    }
}