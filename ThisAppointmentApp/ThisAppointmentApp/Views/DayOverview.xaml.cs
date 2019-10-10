using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThisAppointmentApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayOverview : ContentPage
    {
        public DayOverview()
        {
            InitializeComponent();
            MainPageViewModel model = new MainPageViewModel();
            List<Meeting> meetings = model.Meetings;

            for (int i = 0; i < meetings.Count; i++)
            {
                if (meetings[i].Start.Day != DateTime.Now.Day)
                {
                    meetings.Remove(meetings[i]);
                }
            }

            list.ItemsSource = meetings;
        }
    }
}