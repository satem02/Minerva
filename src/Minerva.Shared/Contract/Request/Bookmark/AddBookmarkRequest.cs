namespace Minerva.Shared.Contract.Request.Bookmark
{
    public class AddBookmarkRequest
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
    }
}