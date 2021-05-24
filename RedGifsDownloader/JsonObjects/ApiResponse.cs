using System.Collections.Generic;

namespace RedGifsDownloader.JsonObjects
{
    public class ApiResponse
    {
        public string cursor { get; set; }
        public List<Gfycat> gfycats { get; set; }
    }
}