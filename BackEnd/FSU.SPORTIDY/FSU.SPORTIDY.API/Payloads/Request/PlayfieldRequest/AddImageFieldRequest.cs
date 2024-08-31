namespace FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest
{
    [AtLeastOneRequired(nameof(ImageUrl), nameof(VideoUrl))]
    public class AddImageFieldRequest
    {
        public IFormFile? ImageUrl { get; set; }

        public IFormFile? VideoUrl { get; set; }

        public int? ImageIndex { get; set; }
    }
}
