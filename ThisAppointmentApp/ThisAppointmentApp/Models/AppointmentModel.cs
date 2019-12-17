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
        private string _requerencePattern { get; set; }
        private string _equipment { get; set; }
        private int count = 0;
        int index = 0;
        
        public AppointmentModel(ScheduleAppointment scheduleAppointment, List<Attendee> attendees,string requerencePattern)
        {
            _appointment = scheduleAppointment;
            Name = Name;
            StartTime = StartTime;
            EndTime = EndTime;
            Location = Location;
            RequerencePattern = requerencePattern;           
            _attendees = attendees;
        }
        
        public AppointmentModel()
        {}

        public string Name
        {
            get { return _name; }
            set 
            {
                _name = _appointment == null ? value : _appointment.Subject;
            }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set 
            { 
                _startTime = _appointment == null ? value: _appointment.StartTime; 
            }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set 
            { 
                _endTime = _appointment == null ? value : _appointment.EndTime; 
            }
        }

        public List<Attendee> Attendees
        {
            get
            {
                return _attendees;
            }
            set 
            {
                if(this.Name == "daily interval 2")
                {

                }
                _attendees = value; 
            }
        }
        
        public string Location
        {
            get { return _location; }
            set 
            { 
                _location = _appointment == null ? value : _appointment.Location; 
            }
        }

        public ScheduleAppointment ReturnAppointment()
        {
            return _appointment;
        }

        public string RequerencePattern
        {
            get { return _requerencePattern; }
            set { _requerencePattern = value; }
        }

        public List<Attendee> Attenders
        {
            get 
            {               
                count = count == 20 ? 0 : count;
                List<Attendee> list = new List<Attendee>();

                if (count < _attendees.Count)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            list.Add(_attendees[i + count]);
                        }
                        catch (Exception)
                        {
                            count += 5;
                            return list;
                        }
                    }
                }
                count += 5;
                return list;
            }
        }       
    }
}