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
            var appoint = calendar.returnNowAppointment();
            
            Name.Text = appoint.Name;
            Location.Text = appoint.Location;
            StartTime.Text = appoint.StartTime.ToString("hh:mm");
            EndTime.Text = appoint.EndTime.ToString("hh:mm");
            var attendees = appoint.Attendees;

            attendeesList.ItemsSource = appoint.Attenders;
            attendeesList1.ItemsSource = appoint.Attenders;
            attendeesList2.ItemsSource = appoint.Attenders;
            attendeesList3.ItemsSource = appoint.Attenders;           
        }
    }
}