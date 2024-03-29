using System.Threading.Tasks;
using Minerva.Shared.Contract;
using Minerva.Shared.Contract.Models;
using Minerva.Shared.Contract.Request.Post;
using Minerva.Shared.Contract.Response.Post;

namespace Minerva.Shared.Services
{
    public interface IPostService
    {
        Task<GetPostResponse> GetPostAsync(GetPostRequest request);
        Task<AddPostResponse> AddPostAsync(AddPostRequest request);
    }

    public class AddPostResponse : ResponseBase
    {
        public PostModel Post { get; set; }
    }
}