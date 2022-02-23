using System;
using System.IO;

namespace GfycatDownloader_Rewrite_DNCore
{
    public class Logger
    {
        public static void Log(string message)
        {
            if(Config.Instance.LogLevel == LogLevel.Console || Config.Instance.LogLevel == LogLevel.Both)
            {
                if (Config.Instance.FancyLog)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(DateTime.Now.ToString(Config.Instance.LogTimeStampFormat));
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("] [");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("LOG");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("] : ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(message);
            }

            if (Config.Instance.LogLevel == LogLevel.File || Config.Instance.LogLevel == LogLevel.Both)
            {
                string writeString = string.Empty;
                if (Config.Instance.FancyLog) writeString += "[" + DateTime.Now.ToString(Config.Instance.LogTimeStampFormat) + "] [LOG] : ";
                writeString += message;
                Directory.CreateDirectory("Logs");
                File.AppendAllText($"Logs/{DateTime.Now.ToString(Config.Instance.LogFileNameFormat)}.txt", writeString + Environment.NewLine);
            }
        }
        
        public static void Error(string message)
        {
            if(Config.Instance.LogLevel == LogLevel.Console || Config.Instance.LogLevel == LogLevel.Both)
            {
                if (Config.Instance.FancyLog)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(DateTime.Now.ToString(Config.Instance.LogTimeStampFormat));
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("] [");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("ERROR");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("] : ");
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine(message);
            }

            if (Config.Instance.LogLevel == LogLevel.File || Config.Instance.LogLevel == LogLevel.Both)
            {
                string writeString = string.Empty;
                if (Config.Instance.FancyLog) writeString += "[" + DateTime.Now.ToString(Config.Instance.LogTimeStampFormat) + "] [ERROR] : ";
                writeString += message;
                Directory.CreateDirectory("Logs");
                File.AppendAllText($"Logs/{DateTime.Now.ToString(Config.Instance.LogFileNameFormat)}.txt", writeString + Environment.NewLine);
            }
        }
    }
}