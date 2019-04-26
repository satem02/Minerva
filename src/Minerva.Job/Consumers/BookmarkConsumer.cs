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
        public BookmarkConsumer(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        [CapSubscribe(Constants.BookmarkQueue)]
        public async Task ProcessBookmark(CreateBookmarkRequest request)
        {

        }
    }
}