using System;

namespace RedGifsDownloader
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Do you want to download by user(type 1) or by search term(2)");
            string inp = Console.ReadLine();
            Console.Write(inp == "1"
                ? "Please enter the id of the user you want to download: "
                : "Please enter the search term you want to download: ");
            string input = Console.ReadLine().ToLower();
            Console.Title = inp == "1" ? "Downloading User: " + input : "Downloading Search: " + input;
            Console.Write("Do you want to download the mp4s(type mp4) or gifs(type gif): ");
            bool downloadMp4 = Console.ReadLine() == "mp4";
            Console.Write("What is the minimum amount of likes a gif/video should have? ");
            int minLikes = Convert.ToInt32(Console.ReadLine());
            try
            {
                if (inp == "1")
                {
                    RedGifs.DownloadUser(input, downloadMp4, minLikes);
                }
                else
                {
                    RedGifs.DownloadBySearch(input, downloadMp4, minLikes);
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something failed wile downloading...");
                Console.WriteLine(e);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DOWNLOAD FINISHED!");
            Console.Title = inp == "1" ? "FINISHED Downloading User: " + input : "FINISHED Downloading Search: " + input;
            Console.ReadLine();
        }
    }
}