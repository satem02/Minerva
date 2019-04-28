using System;
using System.Net;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Common;
using Minerva.Shared.Contract;
using Minerva.Shared.Contract.Request.Bookmark;
using Minerva.Shared.Mappers;
using Minerva.Shared.Providers;
using Minerva.Shared.Repositories;

namespace Minerva.Shared.Services.Implementations
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ICapPublisher _capPublisher;
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IBookmarkMapper _bookmarkMapper;
        private readonly ILogger<BookmarkService> _logger;
        private readonly IUserProvider _userProvider;
        public BookmarkService(ICapPublisher capPublisher, IBookmarkRepository bookmarkRepository, IBookmarkMapper bookmarkMapper, ILogger<BookmarkService> logger, IUserProvider userProvider)
        {
            _capPublisher = capPublisher;
            _bookmarkRepository = bookmarkRepository;
            _bookmarkMapper = bookmarkMapper;
            _logger = logger;
            _userProvider = userProvider;
        }

        public async Task<ResponseBase> AddToQueueAsync(CreateBookmarkRequest request)
        {
            var response = new ResponseBase();
            try
            {
                request.UserId = _userProvider.GetUserId();
                await _capPublisher.PublishAsync(Constants.BookmarkQueue, request);
                response.StatusCode = (int) HttpStatusCode.Accepted;
            }
            catch (Exception)
            {
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<ResponseBase> AddBookmarkAsync(AddBookmarkRequest request)
        {
            var response = new ResponseBase();
            request.UserId = _userProvider.GetUserId();

            var entity = _bookmarkMapper.ToEntity(request);
            var isSuccess = await _bookmarkRepository.AddAsync(entity);

            if (!isSuccess)
            {
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}