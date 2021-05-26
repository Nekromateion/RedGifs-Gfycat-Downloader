using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using RedGifsDownloader.JsonObjects;

namespace RedGifsDownloader
{
    public static class RedGifs
    {
        private static readonly WebClient WebClient = new WebClient();

        internal static void DownloadUser(string userId, bool downloadMp4, int minLikes)
        {
            Directory.CreateDirectory(userId);
            var user = JsonConvert.DeserializeObject<ApiResponse>(
                WebClient.DownloadString($"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100"));
            if (user != null)
            {
                foreach (var gif in user.gfycats)
                    if (gif.likes >= minLikes)
                    {
                        Console.WriteLine($"Downloading {gif.gfyName}\t({gif.views} views & {gif.likes} likes)");
                        if (downloadMp4)
                        {
                            try
                            {
                                WebClient.DownloadFile(gif.mp4Url ?? gif.mobileUrl,
                                    userId + "/" + gif.gfyName + ".mp4");
                            }
                            catch
                            {
                                WebClient.DownloadFile(gif.mobileUrl, userId + "/" + gif.gfyName + ".mp4");
                            }
                        }
                        else
                        {
                            if (gif.content_urls != null && gif.content_urls.largeGif != null &&
                                gif.content_urls.largeGif.url != null)
                                WebClient.DownloadFile(gif.content_urls.largeGif.url,
                                    userId + "/" + gif.gfyName + ".gif");
                            else if (gif.gifUrl != null)
                                WebClient.DownloadFile(gif.gifUrl, userId + "/" + gif.gfyName + ".gif");
                        }
                    }

                while (user.cursor != null)
                {
                    user = JsonConvert.DeserializeObject<ApiResponse>(WebClient.DownloadString(
                        $"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100&cursor={user.cursor}"));
                    foreach (var gif in user.gfycats)
                        if (gif.likes >= minLikes)
                        {
                            Console.WriteLine($"Downloading {gif.gfyName}\t({gif.views} views & {gif.likes} likes)");
                            if (downloadMp4)
                            {
                                try
                                {
                                    WebClient.DownloadFile(gif.mp4Url ?? gif.mobileUrl,
                                        userId + "/" + gif.gfyName + ".mp4");
                                }
                                catch
                                {
                                    WebClient.DownloadFile(gif.mobileUrl, userId + "/" + gif.gfyName + ".mp4");
                                }
                            }
                            else
                            {
                                if (gif.content_urls != null && gif.content_urls.largeGif != null &&
                                    gif.content_urls.largeGif.url != null)
                                    WebClient.DownloadFile(gif.content_urls.largeGif.url,
                                        userId + "/" + gif.gfyName + ".gif");
                                else if (gif.gifUrl != null)
                                    WebClient.DownloadFile(gif.gifUrl, userId + "/" + gif.gfyName + ".gif");
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
            var search = JsonConvert.DeserializeObject<ApiResponse>(
                WebClient.DownloadString(
                    $"{Api.BaseUrl}{Api.SearchEndpoint}?search_text={searchTerm}&count=150&order=trending"));
            if (search != null)
            {
                foreach (var gif in search.gfycats)
                    if (gif.likes >= minLikes)
                    {
                        Console.WriteLine($"Downloading {gif.gfyName}\t({gif.views} views & {gif.likes} likes)");
                        if (downloadMp4)
                        {
                            try
                            {
                                WebClient.DownloadFile(gif.mp4Url ?? gif.mobileUrl,
                                    "search/" + searchTerm + "/" + gif.gfyName + ".mp4");
                            }
                            catch
                            {
                                WebClient.DownloadFile(gif.mobileUrl,
                                    "search/" + searchTerm + "/" + gif.gfyName + ".mp4");
                            }
                        }
                        else
                        {
                            if (gif.content_urls != null && gif.content_urls.largeGif != null &&
                                gif.content_urls.largeGif.url != null)
                                WebClient.DownloadFile(gif.content_urls.largeGif.url,
                                    "search/" + searchTerm + "/" + gif.gfyName + ".gif");
                            else if (gif.gifUrl != null)
                                WebClient.DownloadFile(gif.gifUrl, "search/" + searchTerm + "/" + gif.gfyName + ".gif");
                        }
                    }

                while (search.cursor != null)
                {
                    search = JsonConvert.DeserializeObject<ApiResponse>(WebClient.DownloadString(
                        $"{Api.BaseUrl}{Api.SearchEndpoint}?search_text={searchTerm}&count=150&order=trending&cursor={search.cursor}"));
                    foreach (var gif in search.gfycats)
                        if (gif.likes >= minLikes)
                        {
                            Console.WriteLine($"Downloading {gif.gfyName}\t({gif.views} views & {gif.likes} likes)");
                            if (downloadMp4)
                            {
                                try
                                {
                                    WebClient.DownloadFile(gif.mp4Url ?? gif.mobileUrl,
                                        "search/" + searchTerm + "/" + gif.gfyName + ".mp4");
                                }
                                catch
                                {
                                    WebClient.DownloadFile(gif.mobileUrl,
                                        "search/" + searchTerm + "/" + gif.gfyName + ".mp4");
                                }
                            }
                            else
                            {
                                if (gif.content_urls != null && gif.content_urls.largeGif != null &&
                                    gif.content_urls.largeGif.url != null)
                                    WebClient.DownloadFile(gif.content_urls.largeGif.url,
                                        "search/" + searchTerm + "/" + gif.gfyName + ".gif");
                                else if (gif.gifUrl != null)
                                    WebClient.DownloadFile(gif.gifUrl,
                                        "search/" + searchTerm + "/" + gif.gfyName + ".gif");
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