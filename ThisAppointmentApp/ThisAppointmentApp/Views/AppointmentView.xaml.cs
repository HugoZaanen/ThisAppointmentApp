using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThisAppointmentApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentView : ContentPage
    {
        public AppointmentView()
        {
            InitializeComponent();            
            ThisAppointmentApp.Models.Calendar calendar = new ThisAppointmentApp.Models.Calendar();
           
            //Name.Text = meeting[0].Name;
            //Location.Text = meeting[0].Location;
            //StartTime.Text = meeting[0].Start.ToString("hh:mm");
            //EndTime.Text = meeting[0].End.ToString("hh:mm");
            //var attendees = meeting[0].Attendees;

            List<string> attendeesNames = new List<string>();

            //foreach (var attendee in attendees)
            //{
            //    attendeesNames.Add(attendee.Name);
            //}
            //attendeesList.ItemsSource = attendeesNames;
        }
    }
}