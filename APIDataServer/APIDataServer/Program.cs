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

        private const string JSON_DATA_DIR = @"..\..\..\Data\";
        private const string DATA_FORMAT = "json";
        private const string SCHEDULES_FILE = JSON_DATA_DIR + "schedule." + DATA_FORMAT;
        private const string MAPDATA_FILE = JSON_DATA_DIR + "mapdata." + DATA_FORMAT;
        private const string QUESTIONS_FILE = JSON_DATA_DIR + "questions." + DATA_FORMAT;
        private const string ACTIVITIES_FILE = JSON_DATA_DIR + "activities." + DATA_FORMAT;

        static void Main(string[] args)
        {
            ImportData();

            WebServer ws = new WebServer(HandleRequest, "http://*:" + LISTEN_PORT + "/api/");
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
            //var response = "[{\"error\": \"Not supported\"}]";
            var response = JsonConvert.SerializeObject(new { error = "Not supported" });
            var requestStr = request.RawUrl.ToLower();

#if DEBUG
            Console.WriteLine(requestStr);
#endif

            var reqParts = requestStr.Split('/').Skip(1).ToArray();

            if (reqParts.Length > 1 && reqParts[0].Equals("api"))
            {
                var skip = 0;
                var take = -1;
                if (reqParts.Length >= 3)
                {
                    foreach (var kvp in reqParts[2].Split('&'))
                    {
                        if (!kvp.Contains("=")) continue;
                        var kvpParts = kvp.Split('=');
                        var key = kvpParts[0];
                        var value = kvpParts[1];
                        switch (key)
                        {
                            case "skip":
                                if (!Int32.TryParse(value, out skip) || skip < 0)
                                    return JsonConvert.SerializeObject(new { error = "invalid value for key 'skip'" });
                                break;
                            case "take":
                                if (!Int32.TryParse(value, out take) || take < 1)
                                    return JsonConvert.SerializeObject(new { error = "invalid value for key 'take'" });
                                break;
                        }
                    }
                }

                var dataType = reqParts[1];
                switch (dataType)
                {
                    case "questions":
                        var subQuestions = Questions.Skip(skip);
                        if (take > 0) subQuestions = subQuestions.Take(take);
                        response = JsonConvert.SerializeObject(subQuestions.ToArray());
                        break;
                    case "schedules":
                        var subSchedules= Schedules.Skip(skip);
                        if (take > 0) subSchedules = subSchedules.Take(take);
                        response = JsonConvert.SerializeObject(subSchedules.ToArray());
                        break;
                    case "activities":
                        var subActivities = Activities.Skip(skip);
                        if (take > 0) subActivities = subActivities.Take(take);
                        response = JsonConvert.SerializeObject(subActivities.ToArray());
                        break;
                    case "mapdata":
                        var subMapData= MapData.Skip(skip);
                        if (take > 0) subMapData = subMapData.Take(take);
                        response = JsonConvert.SerializeObject(subMapData.ToArray());
                        break;
                }
            }

            return response;
        }
    }
}
