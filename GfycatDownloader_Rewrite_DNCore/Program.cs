using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GfycatDownloader_Rewrite_DNCore
{
    internal class Program
    {
        // dotnet publish -r linux-x64 -c Release /p:PublishSingleFile=true
        // dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true
        
        private static string _downloadType = null;
        private static string _userId = null;
        private static int _minLikes = -1;
        
        public static void Main(string[] args)
        {
            try
            {
                foreach (var arg in args)
                {
                    if (arg.StartsWith("--type="))
                    {
                        _downloadType = arg.Substring(7);
                    }
                    else if (arg.StartsWith("--user="))
                    {
                        _userId = arg.Substring(7);
                    }
                    else if (arg.StartsWith("--minlikes="))
                    {
                        _minLikes = int.Parse(arg.Substring(11));
                    }
                }

                if (args.Contains("freshconfig"))
                {
                    File.WriteAllText(Config.Path, JsonConvert.SerializeObject(new Config(), Formatting.Indented));
                    return;
                }
            
                if (File.Exists(Config.Path))
                {
                    Config.Instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Config.Path));
                }

                foreach (var configUid in Config.Instance.Users)
                {
                    RedGifs.DownloadUser(configUid, _downloadType ?? Config.Instance.DefaultDownloadType, _minLikes == -1 ? Config.Instance.MinLikes : _minLikes);
                }

                if (_userId != null)
                {
                    RedGifs.DownloadUser(_userId, _downloadType ?? Config.Instance.DefaultDownloadType, _minLikes == -1 ? Config.Instance.MinLikes : _minLikes);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
    }
}