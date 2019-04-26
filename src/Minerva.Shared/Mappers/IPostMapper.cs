using Minerva.Shared.Contract.Models;
using Minerva.Shared.Contract.Response.Post;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Mappers
{
    public interface IPostMapper : IMapper<PostEntity, PostModel>
    {
        PostEntity ToEntity(AddPostRequest request);
    }
}