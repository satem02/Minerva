using Minerva.Shared.Contract.Models;
using Minerva.Shared.Contract.Request.Bookmark;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Mappers
{
    public interface IBookmarkMapper : IMapper<BookmarkEntity, BookmarkModel>
    {
        BookmarkEntity ToEntity(AddBookmarkRequest request);
    }

    public class BookmarkMapper : IBookmarkMapper
    {
        public BookmarkEntity ToEntity(BookmarkModel model)
        {
            return new BookmarkEntity()
            {
                PostId = model.PostId,
                UserId = model.UserId
            };
        }

        public BookmarkModel ToModel(BookmarkEntity entity)
        {
            return new BookmarkModel()
            {
                UserId = entity.UserId,
                PostId = entity.PostId
            };
        }

        public BookmarkEntity ToEntity(AddBookmarkRequest request)
        {
            return new BookmarkEntity()
            {
                UserId = request.UserId,
                PostId = request.PostId
            };
        }
    }
}