using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using RedGifsDownloader.JsonObjects;

namespace RedGifsDownloader
{
    public class RedGifs
    {
        private static readonly WebClient WebClient = new WebClient();
        internal static void DownloadUser(string userId, bool downloadMp4, int minLikes)
        {
            Directory.CreateDirectory(userId);
            User user = JsonConvert.DeserializeObject<User>(WebClient.DownloadString($"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100"));
            if (user != null)
            {
                foreach (var gif in user.gfycats)
                {
                    if (gif.likes >= minLikes)
                    {
                        Console.WriteLine($"Downloading {gif.gfyName}\t({gif.views} views & {gif.likes} likes)");
                        if (downloadMp4)
                        {
                            if (gif.mp4Url != null)
                            {
                                WebClient.DownloadFile(gif.mp4Url, userId + "/" + gif.gfyName + ".mp4");
                            }
                            else
                            {
                                WebClient.DownloadFile(gif.mobileUrl, userId + "/" + gif.gfyName + ".mp4");
                            }
                        }
                        else
                        {
                            if (gif.content_urls != null && gif.content_urls.largeGif != null && gif.content_urls.largeGif.url != null)
                            {
                                WebClient.DownloadFile(gif.content_urls.largeGif.url, userId + "/" + gif.gfyName + ".gif");
                            }
                            else if(gif.gifUrl != null)
                            {
                                WebClient.DownloadFile(gif.gifUrl, userId + "/" + gif.gfyName + ".gif");
                            }
                        }
                    }
                }
                Console.WriteLine("Finished first batch of 100 gifs/videos");

                while (user.cursor != null)
                {
                    user = JsonConvert.DeserializeObject<User>(WebClient.DownloadString($"{Api.BaseUrl}{Api.UsersEndpoint}{userId}{Api.UserGfysEndpoint}?count=100&cursor={user.cursor}"));
                    foreach (var gif in user.gfycats)
                    {
                        if (gif.likes >= minLikes)
                        {
                            Console.WriteLine($"Downloading {gif.gfyName}\t({gif.views} views & {gif.likes} likes)");
                            if (downloadMp4) WebClient.DownloadFile(gif.mp4Url, userId + "/" + gif.gfyName + ".mp4");
                            else WebClient.DownloadFile(gif.content_urls.largeGif.url, userId + "/" + gif.gfyName + ".gif");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Unable to fetch users gifs...");
            }
        }
    }
}