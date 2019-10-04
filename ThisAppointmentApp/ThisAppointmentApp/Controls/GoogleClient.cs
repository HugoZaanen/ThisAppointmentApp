using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace ThisAppointmentApp
{
    public class GoogleClient
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        private readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        private readonly string ApplicationName = "Google Calendar API .NET Quickstart";
        private readonly string _credentials = "{\"installed\":{\"client_id\":\"156722111953-nl8eqg8ivv2bkg8cunbdcb347m63c47d.apps.googleusercontent.com\",\"project_id\":\"quickstart-1561104338132\",\"auth_uri\":\"https://accounts.google.com/o/oauth2/auth\",\"token_uri\":\"https://oauth2.googleapis.com/token\",\"auth_provider_x509_cert_url\":\"https://www.googleapis.com/oauth2/v1/certs\",\"client_secret\":\"Hn0RSQtCWP9EkyvDAEukuFwV\",\"redirect_uris\":[\"urn:ietf:wg:oauth:2.0:oob\",\"http://localhost\"]}}";
        
        private CalendarService _service;

        public GoogleClient()
        {
            UserCredential credential = null;
            //var path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string credPath = "/data/user/0/com.companyname.ThisAppointmentApp/files/token.json";
            // Je stored nu token.json waar je nit mag storen.
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(_credentials)))
            {
                var secrets = GoogleClientSecrets.Load(stream);
                
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets.Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None, new FileDataStore(credPath, true)).Result;        
            }

            Console.WriteLine("Credential file saved to: " + credPath);

            // Create Google Calendar API service.
            _service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public List<Meeting> GetMeetings()
        {
            EventsResource.ListRequest request = _service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var result = request.Execute();

            List<Meeting> meetings = new List<Meeting>();

            foreach (Event e in result.Items)
            {
                //meetings.Add(new Meeting(e.Start, e.End, e.Attendees, e.Location, e.Summary));
            }

            return meetings;
        }
    }
}
