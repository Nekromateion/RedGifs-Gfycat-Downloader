using System.Collections.Generic;

namespace RedGifsDownloader.JsonObjects
{
    public class Gfycat
    {
        public string avgColor { get; set; }
        public object captionsUrl { get; set; }
        public ContentUrls content_urls { get; set; }
        public int createDate { get; set; }
        public int dislikes { get; set; }
        public List<object> domainWhitelist { get; set; }
        public bool encoding { get; set; }
        public bool finished { get; set; }
        public int frameRate { get; set; }
        public int gatekeeper { get; set; }
        public List<object> geoWhitelist { get; set; }
        public string gfyId { get; set; }
        public string gfyName { get; set; }
        public string gif100px { get; set; }
        public string gifUrl { get; set; }
        public bool hasAudio { get; set; }
        public bool hasTransparency { get; set; }
        public int height { get; set; }
        public List<object> languageCategories { get; set; }
        public int likes { get; set; }
        public string max1mbGif { get; set; }
        public string max2mbGif { get; set; }
        public string max5mbGif { get; set; }
        public string miniPosterUrl { get; set; }
        public string miniUrl { get; set; }
        public int mobileHeight { get; set; }
        public string mobilePosterUrl { get; set; }
        public string mobileUrl { get; set; }
        public int mobileWidth { get; set; }
        public string mp4Url { get; set; }
        public bool nsfw { get; set; }
        public int numFrames { get; set; }
        public string posterUrl { get; set; }
        public int published { get; set; }
        public int source { get; set; }
        public List<string> tags { get; set; }
        public string thumb100PosterUrl { get; set; }
        public List<object> userData { get; set; }
        public string userName { get; set; }
        public int views { get; set; }
        public int views5 { get; set; }
        public int width { get; set; }
        public double duration { get; set; }
    }
}