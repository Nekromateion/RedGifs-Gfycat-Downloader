using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RedGifsDownloader
{
    public static class RedGifs
    {
        private static readonly WebClient WebClient = new WebClient();

        internal static void DownloadUser(string userId, bool downloadMp4, int minLikes)
        {
            Directory.CreateDirectory(userId);
            var user = JObject.Parse(
                WebClient.DownloadString($"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100"));
            //var user = JsonConvert.DeserializeObject<ApiResponse>(WebClient.DownloadString($"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100"));
            if (user != null)
            {
                foreach (var gif in user["gfycats"])
                    if ((int) gif["likes"] >= minLikes)
                    {
                        Console.WriteLine(
                            $"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                        if (downloadMp4)
                        {
                            try
                            {
                                WebClient.DownloadFile((string) gif["mp4Url"] ?? (string) gif["mobileUrl"],
                                    userId + "/" + (string) gif["gfyName"] + ".mp4");
                            }
                            catch
                            {
                                WebClient.DownloadFile((string) gif["mobileUrl"],
                                    userId + "/" + (string) gif["gfyName"] + ".mp4");
                            }
                        }
                        else
                        {
                            if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                gif["content_urls"]["largeGif"]["url"] != null)
                                WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                    userId + "/" + (string) gif["gfyName"] + ".gif");
                            else if (gif["gifUrl"] != null)
                                WebClient.DownloadFile((string) gif["gifUrl"],
                                    userId + "/" + (string) gif["gfyName"] + ".gif");
                        }
                    }

                while ((string) user["cursor"] != null)
                {
                    user = JObject.Parse(WebClient.DownloadString(
                        $"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100&cursor={(string) user["cursor"]}"));
                    foreach (var gif in user["gfycats"])
                        if ((int) gif["likes"] >= minLikes)
                        {
                            Console.WriteLine(
                                $"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                            if (downloadMp4)
                            {
                                try
                                {
                                    WebClient.DownloadFile((string) gif["mp4Url"] ?? (string) gif["mobileUrl"],
                                        userId + "/" + (string) gif["gfyName"] + ".mp4");
                                }
                                catch
                                {
                                    WebClient.DownloadFile((string) gif["mobileUrl"],
                                        userId + "/" + (string) gif["gfyName"] + ".mp4");
                                }
                            }
                            else
                            {
                                if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                    gif["content_urls"]["largeGif"]["url"] != null)
                                    WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                        userId + "/" + (string) gif["gfyName"] + ".gif");
                                else if (gif["gifUrl"] != null)
                                    WebClient.DownloadFile((string) gif["gifUrl"],
                                        userId + "/" + (string) gif["gfyName"] + ".gif");
                            }
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
            Directory.CreateDirectory("search/" + searchTerm);
            var search =
                JObject.Parse(WebClient.DownloadString(
                    $"{Api.BaseUrl}{Api.SearchEndpoint}?search_text={searchTerm}&count=150&order=trending"));
            if (search != null)
            {
                foreach (var gif in search["gfycats"])
                    if ((int) gif["likes"] >= minLikes)
                    {
                        Console.WriteLine(
                            $"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                        if (downloadMp4)
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
                        else
                        {
                            if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                gif["content_urls"]["largeGif"]["url"] != null)
                                WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                    "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                            else if (gif["gifUrl"] != null)
                                WebClient.DownloadFile((string) gif["gifUrl"],
                                    "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                        }
                    }

                while ((string) search["cursor"] != null)
                {
                    search = JObject.Parse(WebClient.DownloadString(
                        $"{Api.BaseUrl}{Api.SearchEndpoint}?search_text={searchTerm}&count=150&order=trending&cursor={(string) search["cursor"]}"));
                    foreach (var gif in search["gfycats"])
                        if ((int) gif["likes"] >= minLikes)
                        {
                            Console.WriteLine(
                                $"Downloading {(string) gif["gfyName"]}\t({(string) gif["views"]} views & {(string) gif["likes"]} likes)");
                            if (downloadMp4)
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
                            else
                            {
                                if (gif["content_urls"] != null && gif["content_urls"]["largeGif"] != null &&
                                    gif["content_urls"]["largeGif"]["url"] != null)
                                    WebClient.DownloadFile((string) gif["content_urls"]["largeGif"]["url"],
                                        "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                                else if (gif["gifUrl"] != null)
                                    WebClient.DownloadFile((string) gif["gifUrl"],
                                        "search/" + searchTerm + "/" + (string) gif["gfyName"] + ".gif");
                            }
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