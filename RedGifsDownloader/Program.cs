using System;

namespace RedGifsDownloader
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Please enter the id of the user you want to download: ");
            string id = Console.ReadLine().ToLower();
            Console.Write("Do you want to download the mp4s(type mp4) or gifs(type gif): ");
            bool downloadMp4 = Console.ReadLine() == "mp4";
            Console.Write("What is the minimum amount of likes a gif/video should have? ");
            int minLikes = Convert.ToInt32(Console.ReadLine());
            try
            {
                RedGifs.DownloadUser(id, downloadMp4, minLikes);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed wile downloading...");
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }
    }
}