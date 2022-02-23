using System.Collections.Generic;

namespace GfycatDownloader_Rewrite_DNCore
{
    public class Config
    {
        public static Config Instance = new Config();
        public static string Path = "Config.json";

        public List<string> Users = new List<string>();
        public List<string> Searches = new List<string>();

        public int MinLikes = 0;
        public string DefaultDownloadType = "mp4";
        public int SleepBetweenApiRequests = 10000;
        public int SleepBetweenDownloads = 5000;

        public LogLevel LogLevel = LogLevel.Console;
        public bool FancyLog = false;
        public string LogTimeStampFormat = "dd.MM.yyyy HH:mm:ss.ffff";
        public string LogFileNameFormat = "dd.MM.yyyy";

        public string ShortIntro1 = "Users is a list of strings so you can add multiple users.";
        public string ShortIntro2 = "Searches is a list of strings so you can add multiple searches.";
        public string ShortIntro3 = "MinLikes is an integer and lets you set the minimum amount of likes a gfycat must have to be downloaded.";
        public string ShortIntro4 = "DefaultDownloadType is a string and lets you specify in what format the gfycat will be downloaded.";
        public string ShortIntro5 = "SleepBetweenApiRequests is an integer and lets you set the amount of milliseconds to wait between API requests.";
        public string ShortIntro6 = "SleepBetweenDownloads is an integer and lets you set the amount of milliseconds to sleep between downloading files.";
        public string ShortIntro7 = "LogLevel is a integer to which the following applies: 0 = Console, 1 = File, 2 = Both.";
        public string ShortIntro8 = "FancyLog is a boolean(true/false) and lets you specify if you want your logs to be given timestamps and colors(In the console).";
        public string ShortIntro9 = "LogTimeStampFormat is a string which lets you specify the format for the timestamps in the log file and or console. (See https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)";
        public string ShortIntro10 = "LogFileNameFormat is a string which lets you specify the format for the log file names. (See https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)";
    }
}