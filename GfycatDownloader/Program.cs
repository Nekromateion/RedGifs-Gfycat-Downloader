using System;

namespace RedGifsDownloader
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Do you want to download from gfycat(type g) or redgifs(type r)?");
            switch (Console.ReadLine())
            {
                case "redgifs":
                case "r":
                    ApiEndpoints.BaseUrl = "https://api.redgifs.com/v1/";
                    break;
                case "gfycat":
                case "g":
                    ApiEndpoints.BaseUrl = "https://api.gfycat.com/v1/";
                    break;
            }
            Console.Write("Do you want to download by user(type 1) or by search term(2)");
            var inp = Console.ReadLine();
            Console.Write(inp == "1"
                ? "Please enter the id of the user you want to download: "
                : "Please enter the search term you want to download: ");
            var input = Console.ReadLine().ToLower();
            Console.Title = inp == "1" ? "Downloading User: " + input : "Downloading Search: " + input;
            Console.Write("Do you want to download the mp4s(type mp4) or gifs(type gif): ");
            var downloadMp4 = Console.ReadLine() == "mp4";
            Console.Write("What is the minimum amount of likes a gif/video should have? ");
            var minLikes = Convert.ToInt32(Console.ReadLine());
            try
            {
                if (inp == "1")
                    Api.DownloadUser(input, downloadMp4, minLikes);
                else
                    Api.DownloadBySearch(input, downloadMp4, minLikes);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Something failed wile downloading...\nPlease report this on the github repo (https://github.com/Nekromateion/GfycatDownloader)");
                Console.WriteLine(e);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DOWNLOAD FINISHED!");
            Console.Title =
                inp == "1" ? "FINISHED Downloading User: " + input : "FINISHED Downloading Search: " + input;
            Console.ReadLine();
        }
    }
}