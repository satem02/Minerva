using System;

namespace Minerva.Shared.Data.Entities
{
    public class BookmarkEntity : EntityBase<Guid>
    {
        public int PostId { get; set; }
        public string UserId { get; set; }

        //Relations
        public PostEntity Post { get; set; }
        public UserEntity User { get; set; }
    }
}