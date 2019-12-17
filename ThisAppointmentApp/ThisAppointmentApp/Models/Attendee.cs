using System;
using System.Collections.Generic;
using System.Text;

namespace ThisAppointmentApp
{
    public class Attendee
    {
        private string _name;
        public String Name { 
            get 
            {
                return _name;
            }
            set 
            {
                if (value.Length < 8)
                {
                    _name = value;
                }
                else
                {
                    _name = value.Substring(0,8);
                }
            }
        }
        public String Email { get; set; }
    }
}
