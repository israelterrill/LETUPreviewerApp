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

        private const string JSON_DATA_DIR = @"C:\Users\mccan\Documents\Code\LETUPreviewerApp\APIDataServer\";
        private const string SCHEDULES_FILE = JSON_DATA_DIR + "schedule.json";
        private const string MAPDATA_FILE = JSON_DATA_DIR + "mapdata.json";
        private const string QUESTIONS_FILE = JSON_DATA_DIR + "questions.json";
        private const string ACTIVITIES_FILE = JSON_DATA_DIR + "activities.json";

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
                Schedules = JsonConvert.DeserializeObject<Schedule[]>(File.ReadAllText(SCHEDULES_FILE));
                MapData = JsonConvert.DeserializeObject<Map[]>(File.ReadAllText(MAPDATA_FILE));
                Questions = JsonConvert.DeserializeObject<Question[]>(File.ReadAllText(QUESTIONS_FILE));
                Activities = JsonConvert.DeserializeObject<Activity[]>(File.ReadAllText(ACTIVITIES_FILE));
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
