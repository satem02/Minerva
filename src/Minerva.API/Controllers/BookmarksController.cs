using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minerva.API.Common;
using Minerva.Shared.Contract.Request.Bookmark;
using Minerva.Shared.Services;

namespace Minerva.API.Controllers
{
    [Authorize]
    [Route("v1/bookmarks")]
    public class BookmarksController : ApiControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        public BookmarksController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Create([FromBody] CreateBookmarkRequest request)
        {
            var response = await _bookmarkService.AddToQueueAsync(request);
            return Result(response);
        }
    }
}