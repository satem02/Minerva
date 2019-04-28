namespace Minerva.Shared.Contract.Request.Bookmark
{
    public class SaveBookmarkRequest
    {
        public string FilePath { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
    }
}