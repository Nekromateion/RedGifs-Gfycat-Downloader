using System.Collections.Generic;

namespace RedGifsDownloader.JsonObjects
{
    public class User
    {
        public string cursor { get; set; }
        public List<Gfycat> gfycats { get; set; }
    }
}