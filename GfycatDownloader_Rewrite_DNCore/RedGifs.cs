using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace GfycatDownloader_Rewrite_DNCore
{
    public class RedGifs
    {
        private static readonly WebClient Client = new WebClient();
        
        public static void DownloadUser(string userId, string type, int minLikes)
        {
            Logger.Log("Downloading user " + userId + "...");
            Directory.CreateDirectory("Users/" + userId);
            string apiUrl = "https://api.redgifs.com/v2/users/" + userId + "/search?order=recent&page=";
            int pageCount = 1;

            for (int i = 1; i <= pageCount; i++)
            {
                JObject responseObject = JObject.Parse(Client.DownloadString(apiUrl + i));
                pageCount = (int)JObject.Parse(Client.DownloadString(apiUrl + 1))["pages"];
                foreach (var gif in responseObject["gifs"])
                {
                    if ((int)gif["likes"] >= minLikes)
                    {
                        if (!File.Exists("Users/" + userId + "/" + gif["id"] + (type == "mp4" ? ".mp4" : ".gif")))
                        {
                            if (type == "mp4")
                            {
                                Client.DownloadFile((string)gif["urls"]["hd"], "Users/" + userId + "/" + gif["id"] + ".mp4");
                            }
                            else
                            {
                                Client.DownloadFile((string)gif["urls"]["gif"], "Users/" + userId + "/" + gif["id"] + ".gif");
                            }
                            Logger.Log("Downloaded " + gif["id"]);
                            Thread.Sleep(Config.Instance.SleepBetweenDownloads);
                        }
                    }
                }
                Thread.Sleep(Config.Instance.SleepBetweenApiRequests);
            }
            Logger.Log("Finished downloading user " + userId);
        }
    }
}