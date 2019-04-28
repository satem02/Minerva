using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Minerva.Shared.Common;
using Minerva.Shared.Contract.Request.Bookmark;

namespace Minerva.Job.Consumers
{
    public interface IBookmarkConsumer
    {
        Task ProcessBookmark(CreateBookmarkRequest request);
    }

    public class BookmarkConsumer : IBookmarkConsumer, ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;
        private readonly HttpClient _httpClient;

        public BookmarkConsumer(ICapPublisher capPublisher, IHttpClientFactory httpClientFactory)
        {
            _capPublisher = capPublisher;
            _httpClient = httpClientFactory.CreateClient();
        }

        [CapSubscribe(Constants.BookmarkQueue)]
        public async Task ProcessBookmark(CreateBookmarkRequest request)
        {
            var response = await _httpClient.GetAsync(request.Url);
            if (response.IsSuccessStatusCode)
            {
                using (var fileStream =
                    new FileStream(Path.GetTempFileName(), FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    await stream.CopyToAsync(fileStream);
                }
            }
        }
    }