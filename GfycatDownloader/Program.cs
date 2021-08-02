using System;

namespace RedGifsDownloader
{
    internal static class Program
    {
        internal static bool isCli = false;
        
        public static void Main(string[] args)
        {
            string inp;
            string input;
            bool downloadMp4;
            int minLikes;
            if (args.Length != 0)
            {
                if (args.Length >= 5)
                {
                    isCli = true;
                    switch (args[0])
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

                    inp = args[1];
                    input = args[2];
                    downloadMp4 = args[3] == "mp4";
                    minLikes = Convert.ToInt32(args[4]);
                }
                else
                {
                    Console.WriteLine("Please give all parameters (GfycatDownloader.exe [redgifs/gfycat] [user(1)/search(2)] [UserID/Search Term] [mp4/gif] [minLikes(for example 13)])");
                    return;
                }
            }
            else
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
                inp = Console.ReadLine();
                Console.Write(inp == "1"
                    ? "Please enter the id of the user you want to download: "
                    : "Please enter the search term you want to download: ");
                input = Console.ReadLine().ToLower();
                Console.Title = inp == "1" ? "Downloading User: " + input : "Downloading Search: " + input;
                Console.Write("Do you want to download the mp4s(type mp4) or gifs(type gif): ");
                downloadMp4 = Console.ReadLine() == "mp4";
                Console.Write("What is the minimum amount of likes a gif/video should have? ");
                minLikes = Convert.ToInt32(Console.ReadLine());
            }
            
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
            if (!isCli)
            {
                Console.Title =
                    inp == "1" ? "FINISHED Downloading User: " + input : "FINISHED Downloading Search: " + input;
                Console.ReadLine();
            }
        }
    }
}