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

        private const string DATA_FORMAT = "csv";
        private const string DATA_DIR = @"..\..\..\Data\";
        private const string SCHEDULES_FILE = DATA_DIR + "schedule." + DATA_FORMAT;
        private const string MAPDATA_FILE = DATA_DIR + "mapdata." + DATA_FORMAT;
        private const string QUESTIONS_FILE = DATA_DIR + "questions." + DATA_FORMAT;
        private const string ACTIVITIES_FILE = DATA_DIR + "activities." + DATA_FORMAT;

        private static Schedule[] Schedules = null;
        private static Map[] MapData = null;
        private static Question[] Questions = null;
        private static Activity[] Activities = null;

        /// <summary>
        /// Main function. args is an array of strings from command line
        /// </summary>
        /// <param name="args"></param>
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

        /// <summary>
        /// Imports existing data file(s) contents for processing and serving requests
        /// </summary>
        private static void ImportData()
        {
            if (Directory.Exists(DATA_DIR))
            {
                try
                {
                    switch (DATA_FORMAT)
                    {
                        case "json":
                            Schedules = JsonConvert.DeserializeObject<Schedule[]>(File.ReadAllText(SCHEDULES_FILE));
                            MapData = JsonConvert.DeserializeObject<Map[]>(File.ReadAllText(MAPDATA_FILE));
                            Questions = JsonConvert.DeserializeObject<Question[]>(File.ReadAllText(QUESTIONS_FILE));
                            Activities = JsonConvert.DeserializeObject<Activity[]>(File.ReadAllText(ACTIVITIES_FILE));
                            break;
                        case "csv":
                            Schedules = Schedule.FromCsvMulti(DATA_DIR);
                            MapData = Map.FromCsvMulti(MAPDATA_FILE);
                            Questions = Question.FromCsvMulti(QUESTIONS_FILE);
                            Activities = Activity.FromCsvMulti(ACTIVITIES_FILE);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Web Server Request Handler
        /// </summary>
        /// <param name="request">HTTP request string</param>
        /// <returns>JSON string with requested data</returns>
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
                        var subSchedules = Schedules.Skip(skip);
                        if (take > 0) subSchedules = subSchedules.Take(take);
                        response = JsonConvert.SerializeObject(subSchedules.ToArray());
                        break;
                    case "activities":
                        var subActivities = Activities.Skip(skip);
                        if (take > 0) subActivities = subActivities.Take(take);
                        response = JsonConvert.SerializeObject(subActivities.ToArray());
                        break;
                    case "mapdata":
                        var subMapData = MapData.Skip(skip);
                        if (take > 0) subMapData = subMapData.Take(take);
                        response = JsonConvert.SerializeObject(subMapData.ToArray());
                        break;
                }
            }

            return response;
        }
    }
}
