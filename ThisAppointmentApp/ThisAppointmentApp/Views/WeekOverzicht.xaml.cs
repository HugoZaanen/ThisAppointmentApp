using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThisAppointmentApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeekOverzicht : ContentPage
    {
        public WeekOverzicht()
        {
            InitializeComponent();
            MainPageViewModel model = new MainPageViewModel();           

            ScheduleAppointmentCollection appointmentCollection = new ScheduleAppointmentCollection();
            List<ScheduleAppointment> appointments = model.ScheduleAppointments;

            foreach(ScheduleAppointment ap in appointments)
            {
                appointmentCollection.Add(ap);
            }

            schedule.DataSource = appointmentCollection;
        }
    }
}