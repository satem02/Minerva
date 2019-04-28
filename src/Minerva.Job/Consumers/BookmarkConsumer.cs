using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Kindlegen;
using Kindlegen.Models;
using Microsoft.Extensions.AzureStorage;
using Minerva.Shared.Common;
using Minerva.Shared.Contract.Request.Bookmark;
using Minerva.Shared.Contract.Response.Post;
using Minerva.Shared.Services;

namespace Minerva.Job.Consumers
{
    public interface IBookmarkConsumer
    {
        Task ProcessBookmark(CreateBookmarkRequest request);
        Task SaveBookmark(SaveBookmarkRequest request);
    }

    public class BookmarkConsumer : IBookmarkConsumer, ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;
        private readonly HttpClient _httpClient;
        private readonly IBlobProvider _blobProvider;
        private readonly IPostService _postService;
        private readonly IBookmarkService _bookmarkService;
        public BookmarkConsumer(ICapPublisher capPublisher,
            IHttpClientFactory httpClientFactory,
            IBlobProvider blobProvider,
            IPostService postService,
            IBookmarkService bookmarkService)
        {
            _capPublisher = capPublisher;
            _blobProvider = blobProvider;
            _postService = postService;
            _bookmarkService = bookmarkService;
            _httpClient = httpClientFactory.CreateClient();
        }

        [CapSubscribe(Constants.SaveBookmarkQueue)]
        public async Task SaveBookmark(SaveBookmarkRequest request)
        {
            var storageUrl = await _blobProvider.UploadFile(Constants.ContainerName, request.FilePath);
            if (!string.IsNullOrEmpty(storageUrl))
            {
                var result = await _postService.AddPostAsync(new AddPostRequest()
                {
                    Url = request.Url,
                    BlobUrl = storageUrl
                });

                if (result.IsSuccess)
                {
                    await _bookmarkService.AddBookmarkAsync(new AddBookmarkRequest()
                    {
                        UserId = request.UserId,
                        PostId = result.Post.Id
                    });

                    await _capPublisher.PublishAsync(Constants.EmailQueue, new SendEmailRequest()
                    {
                        PostId = result.Post.Id,
                        UserId = request.UserId
                    });
                }
            }
        }

        [CapSubscribe(Constants.BookmarkQueue)]
        public async Task ProcessBookmark(CreateBookmarkRequest request)
        {
            var response = await _httpClient.GetAsync(request.Url);
            if (response.IsSuccessStatusCode)
            {
                var tempFileName = Path.GetTempFileName();
                using (var fileStream =
                    new FileStream(tempFileName, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    await stream.CopyToAsync(fileStream);
                }

                var result = KindleConverter.Create(tempFileName)
                    .SetCompressionLevel(CompressionLevel.NoCompression)
                    .Convert();
                if (result.IsSuccess)
                {
                    string filePath = tempFileName.Replace(".tmp", ".mobi");
                    await _capPublisher.PublishAsync(Constants.SaveBookmarkQueue, new SaveBookmarkRequest()
                    {
                        UserId = request.UserId,
                        Url = request.Url,
                        FilePath = filePath
                    });
                }
            }
        }
    }
}