using System;
using System.Net;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Common;
using Minerva.Shared.Contract;
using Minerva.Shared.Contract.Request.Bookmark;
using Minerva.Shared.Mappers;
using Minerva.Shared.Repositories;

namespace Minerva.Shared.Services.Implementations
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ICapPublisher _capPublisher;
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IBookmarkMapper _bookmarkMapper;
        private readonly ILogger<BookmarkService> _logger;

        public BookmarkService(ICapPublisher capPublisher, IBookmarkRepository bookmarkRepository, IBookmarkMapper bookmarkMapper, ILogger<BookmarkService> logger)
        {
            _capPublisher = capPublisher;
            _bookmarkRepository = bookmarkRepository;
            _bookmarkMapper = bookmarkMapper;
            _logger = logger;
        }

        public async Task<ResponseBase> AddToQueueAsync(CreateBookmarkRequest request)
        {
            var response = new ResponseBase();
            try
            {
                await _capPublisher.PublishAsync(Constants.BookmarkQueue, request);
                response.StatusCode = (int) HttpStatusCode.Accepted;
            }
            catch (Exception e)
            {
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}