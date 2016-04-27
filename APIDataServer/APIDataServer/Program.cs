using System;
using System.Net;
using System.Linq;
using DataClasses;
using Newtonsoft.Json;

namespace APIDataServer
{
    class Program
    {
        private const int LISTEN_PORT = 44623;
#if DEBUG
        private const string DATA_DIR = @"..\..\..\Install\Data\";
#else
        private const string DATA_DIR = @"Data\";
#endif

        private static Database Database;

        /// <summary>
        /// Main function. args is an array of strings from command line
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Database = new Database(DATA_DIR, refeshOnFileChange:true);

            var ws = new WebServer(HandleRequest, "http://*:" + LISTEN_PORT + "/api/");
            ws.Run();
            while (true)
            {
                System.Threading.Thread.Sleep(10);
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
                                if (!int.TryParse(value, out skip) || skip < 0)
                                    return JsonConvert.SerializeObject(new { error = "invalid value for key 'skip'" });
                                break;
                            case "take":
                                if (!int.TryParse(value, out take) || take < 1)
                                    return JsonConvert.SerializeObject(new { error = "invalid value for key 'take'" });
                                break;
                        }
                    }
                }

                var dataType = reqParts[1];
                switch (dataType)
                {
                    case "questions":
                        var subQuestions = Database.Questions.Skip(skip);
                        if (take > 0) subQuestions = subQuestions.Take(take);
                        response = JsonConvert.SerializeObject(subQuestions.ToArray());
                        break;
                    case "schedules":
                        var subSchedules = Database.Schedules.Skip(skip);
                        if (take > 0) subSchedules = subSchedules.Take(take);
                        response = JsonConvert.SerializeObject(subSchedules.ToArray());
                        break;
                    case "activities":
                        var subActivities = Database.Activities.Skip(skip);
                        if (take > 0) subActivities = subActivities.Take(take);
                        response = JsonConvert.SerializeObject(subActivities.ToArray());
                        break;
                    case "mapdata":
                        var subMapData = Database.MapData.Skip(skip);
                        if (take > 0) subMapData = subMapData.Take(take);
                        response = JsonConvert.SerializeObject(subMapData.ToArray());
                        break;
                }
            }

            return response;
        }
    }
}
