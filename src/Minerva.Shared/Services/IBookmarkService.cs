using System;
using System.Threading.Tasks;
using Minerva.Shared.Contract;
using Minerva.Shared.Contract.Request.Bookmark;

namespace Minerva.Shared.Services
{
    public interface IBookmarkService
    {
        Task<ResponseBase> AddToQueueAsync(CreateBookmarkRequest request);
        Task<ResponseBase> AddBookmarkAsync(AddBookmarkRequest request);
    }
    
}