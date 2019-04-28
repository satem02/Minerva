using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Contract;
using Minerva.Shared.Contract.Request.Post;
using Minerva.Shared.Contract.Response.Post;
using Minerva.Shared.Mappers;
using Minerva.Shared.Repositories;

namespace Minerva.Shared.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostMapper _postMapper;
        private readonly ILogger<PostService> _logger;
        public PostService(IPostRepository postRepository, IPostMapper postMapper, ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _postMapper = postMapper;
            _logger = logger;
        }

        public async Task<GetPostResponse> GetPostAsync(GetPostRequest request)
        {
            var response = new GetPostResponse();

            var entity = await _postRepository.GetPostByUrlAsync(request.Url);
            if (entity == null)
            {
                response.StatusCode = (int) HttpStatusCode.NotFound;
                return response;
            }

            response.Post = _postMapper.ToModel(entity);
            return response;
        }

        public async Task<AddPostResponse> AddPostAsync(AddPostRequest request)
        {
            var response = new AddPostResponse();

            bool isExists = await _postRepository.IsExistsByUrlAsync(request.Url);
            if (isExists)
            {
                response.StatusCode = (int) HttpStatusCode.Conflict;
                return response;
            }

            var entity = _postMapper.ToEntity(request);
            bool isSuccess = await _postRepository.AddAsync(entity);

            if (!isSuccess)
            {
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return response;
            }

            response.Post = _postMapper.ToModel(entity);
            response.StatusCode = (int) HttpStatusCode.Created;
            return response;
        }
    }
}