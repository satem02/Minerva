using Minerva.Shared.Contract.Models;

namespace Minerva.Shared.Contract.Request.Post
{
    public class GetPostResponse : ResponseBase
    {
        public PostModel Post { get; set; }
    }
}