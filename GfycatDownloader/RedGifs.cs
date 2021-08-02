using System;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace RedGifsDownloader
{
    public static class Api
    {
        private static readonly WebClient WebClient = new WebClient();

        internal static void DownloadUser(string userId, bool downloadMp4, int minLikes)
        {
            int count = 0;
            Directory.CreateDirectory(userId);
            var user = JObject.Parse(
                WebClient.DownloadString($"{ApiEndpoints.BaseUrl}{ApiEndpoints.UsersEndpoint}{userId}{ApiEndpoints.UserGfysEndpoint}?count=100"));
            //var user = JsonConvert.DeserializeObject<ApiResponse>(WebClient.DownloadString($"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100"));
            if (user != null)
            {
                foreach (var gif in user["gfycats"])
                    if ((int) gif["likes"] >= minLikes)
                    {
                        if (downloadMp4)
                        {
                            if (!File.Exists(userId + "/mp4/" + (string) gif["gfyName"] + ".mp4"))
                            {
                                Directory.CreateDirectory(userId + "/mp4");
                                Console.WriteLine($"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                                try
                                {
                                    WebClient.DownloadFile((string) gif["mp4Url"] ?? (string) gif["mobileUrl"],
                                        userId + "/mp4/" + (string) gif["gfyName"] + ".mp4");
                                }
                                catch
                                {
                                    WebClient.DownloadFile((string) gif["mobileUrl"],
                                        userId + "/mp4/" + (string) gif["gfyName"] + ".mp4");
                                }
                            }
                            else count++;
                        }
                        else
                        {
                            if (!File.Exists(userId + "/gif/" + (string) gif["gfyName"] + ".gif"))
                            {
                                Directory.CreateDirectory(userId + "/gif");
                                Console.WriteLine($"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                                if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                    gif["content_urls"]["largeGif"]["url"] != null)
                                    WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                        userId + "/gif/" + (string) gif["gfyName"] + ".gif");
                                else if (gif["gifUrl"] != null)
                                    WebClient.DownloadFile((string) gif["gifUrl"],
                                        userId + "/gif/" + (string) gif["gfyName"] + ".gif");
                            }
                            else count++;
                        }

                        Thread.Sleep(100);
                    }

                while ((string) user["cursor"] != null && count < 100)
                {
                    user = JObject.Parse(WebClient.DownloadString(
                        $"{ApiEndpoints.BaseUrl}{ApiEndpoints.UsersEndpoint}{userId}{ApiEndpoints.UserGfysEndpoint}?count=100&cursor={(string) user["cursor"]}"));
                    foreach (var gif in user["gfycats"])
                        if ((int) gif["likes"] >= minLikes)
                        {
                            if (downloadMp4)
                            {
                                if (!File.Exists(userId + "/mp4/" + (string) gif["gfyName"] + ".mp4"))
                                {
                                    Console.WriteLine($"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                                    try
                                    {
                                        WebClient.DownloadFile((string) gif["mp4Url"] ?? (string) gif["mobileUrl"],
                                            userId + "/mp4/" + (string) gif["gfyName"] + ".mp4");
                                    }
                                    catch
                                    {
                                        WebClient.DownloadFile((string) gif["mobileUrl"],
                                            userId + "/mp4/" + (string) gif["gfyName"] + ".mp4");
                                    }
                                }
                            }
                            else
                            {
                                if (!File.Exists(userId + "/gif/" + (string) gif["gfyName"] + ".gif"))
                                {
                                    Console.WriteLine($"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                                    if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                        gif["content_urls"]["largeGif"]["url"] != null)
                                        WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                            userId + "/gif/" + (string) gif["gfyName"] + ".gif");
                                    else if (gif["gifUrl"] != null)
                                        WebClient.DownloadFile((string) gif["gifUrl"],
                                            userId + "/gif/" + (string) gif["gfyName"] + ".gif");
                                }
                            }
                            
                            Thread.Sleep(100);
                        }
                }
            }
            else
            {
                Console.WriteLine("Unable to fetch users gifs...");
            }
        }

        internal static void DownloadBySearch(string searchTerm, bool downloadMp4, int minLikes)
        {
            int count = 0;
            Directory.CreateDirectory("search/" + searchTerm);
            var search =
                JObject.Parse(WebClient.DownloadString(
                    $"{ApiEndpoints.BaseUrl}{ApiEndpoints.SearchEndpoint}?search_text={searchTerm}&count=150&order=trending"));
            if (search != null)
            {
                foreach (var gif in search["gfycats"])
                    if ((int) gif["likes"] >= minLikes)
                    {
                        if (downloadMp4)
                        {
                            if (!File.Exists("search/" + searchTerm + "/" + (string) gif["gfyName"] + ".mp4"))
                            {
                                Console.WriteLine(
                                    $"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                                try
                                {
                                    WebClient.DownloadFile((string) gif["mp4Url"] ?? (string) gif["mobileUrl"],
                                        "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".mp4");
                                }
                                catch
                                {
                                    WebClient.DownloadFile((string) gif["mobileUrl"],
                                        "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".mp4");
                                }
                            }
                            else count++;
                        }
                        else
                        {
                            if (!File.Exists("search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif"))
                            {
                                Console.WriteLine($"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                                if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                    gif["content_urls"]["largeGif"]["url"] != null)
                                    WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                        "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                                else if (gif["gifUrl"] != null)
                                    WebClient.DownloadFile((string) gif["gifUrl"],
                                        "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                            }
                            else count++;
                        }
                        
                        Thread.Sleep(100);
                    }

                while ((string) search["cursor"] != null && count < 100)
                {
                    search = JObject.Parse(WebClient.DownloadString(
                        $"{ApiEndpoints.BaseUrl}{ApiEndpoints.SearchEndpoint}?search_text={searchTerm}&count=150&order=trending&cursor={(string) search["cursor"]}"));
                    foreach (var gif in search["gfycats"])
                        if ((int) gif["likes"] >= minLikes)
                        {
                            Console.WriteLine(
                                $"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                            if (downloadMp4)
                            {
                                if (!File.Exists("search/" + searchTerm + "/" + (string) gif["gfyName"] + ".mp4"))
                                {
                                    try
                                    {
                                        WebClient.DownloadFile((string) gif["mp4Url"] ?? (string) gif["mobileUrl"],
                                            "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".mp4");
                                    }
                                    catch
                                    {
                                        WebClient.DownloadFile((string) gif["mobileUrl"],
                                            "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".mp4");
                                    }
                                }
                                else count++;
                            }
                            else
                            {
                                if (!File.Exists("search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif"))
                                {
                                    if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                        gif["content_urls"]["largeGif"]["url"] != null)
                                        WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                            "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                                    else if (gif["gifUrl"] != null)
                                        WebClient.DownloadFile((string) gif["gifUrl"],
                                            "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                                }
                                else count++;
                            }
                            
                            Thread.Sleep(100);
                        }
                }
            }
            else
            {
                Console.WriteLine("Unable to fetch gifs for search term...");
            }
        }
    }
}