using System;

namespace Minerva.Shared.Contract.Request.Bookmark
{
    public class CreateBookmarkRequest
    {
        public string Url { get; set; }
        public string UserId { get; set; }
    }
}