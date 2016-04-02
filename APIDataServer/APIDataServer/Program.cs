using System;
using System.Net;
using System.IO;
using System.Linq;
using DataClasses;
using Newtonsoft.Json;

namespace APIDataServer
{
    class Program
    {
        private const int LISTEN_PORT = 44623;
        private static Schedule[] Schedules = null;
        private static Map[] MapData = null;
        private static Question[] Questions = null;
        private static Activity[] Activities = null;

        static void Main(string[] args)
        {
            ImportData();

            WebServer ws = new WebServer(HandleRequest, "http://localhost:" + LISTEN_PORT + "/api/");
            ws.Run();
            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        private static void ImportData()
        {
            try
            {
                Schedules = JsonConvert.DeserializeObject<Schedule[]>(File.ReadAllText(@"..\..\..\schedule.json"));
                MapData = JsonConvert.DeserializeObject<Map[]>(File.ReadAllText(@"..\..\..\mapdata.json"));
                Questions = JsonConvert.DeserializeObject<Question[]>(File.ReadAllText(@"..\..\..\questions.json"));
                Activities = JsonConvert.DeserializeObject<Activity[]>(File.ReadAllText(@"..\..\..\activities.json"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        
        private static string HandleRequest(HttpListenerRequest request)
        {
            var response = "[{\"error\": \"Not supported\"}]";
            var requestStr = request.RawUrl.ToLower();
            
#if DEBUG
            Console.WriteLine(requestStr);
#endif

            var reqParts = requestStr.Split('/').Skip(1).ToArray();

            if (reqParts.Length>1 && reqParts[0].Equals("api"))
            {
                var dataType = reqParts[1];
                switch (dataType)
                {
                    case "questions":
                        response = JsonConvert.SerializeObject(Questions);
                        break;
                    case "schedules":
                        response = JsonConvert.SerializeObject(Schedules);
                        break;
                    case "activities":
                        response = JsonConvert.SerializeObject(Activities);
                        break;
                    case "mapdata":
                        response = JsonConvert.SerializeObject(MapData);
                        break;
                }
            }

            return response;
        }
    }
}
