using System;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Minerva.Shared.Common;
using Minerva.Shared.Contract.Request.Account;
using Minerva.Shared.Contract.Request.Bookmark;
using Minerva.Shared.Contract.Response.Post;
using Minerva.Shared.Providers;
using Minerva.Shared.Services;

namespace Minerva.Job.Consumers
{
    public interface IEmailConsumer
    {
        Task SendEmail(SendEmailRequest request);
    }

    public class EmailConsumer : IEmailConsumer, ICapSubscribe
    {
        private readonly IEmailProvider _emailProvider;
        private readonly IPostService _postService;
        private readonly IAccountService _accountService;
        public EmailConsumer(IEmailProvider emailProvider, IPostService postService, IAccountService accountService)
        {
            _emailProvider = emailProvider;
            _postService = postService;
            _accountService = accountService;
        }

        [CapSubscribe(Constants.EmailQueue)]
        public async Task SendEmail(SendEmailRequest request)
        {
            var postResponse = await _postService.GetPostAsync(new GetPostRequest()
            {
                Id = request.PostId
            });

            if (!postResponse.IsSuccess)
            {
                throw new Exception("Post is not found");
            }

            var userResponse = await _accountService.GetUserAsync(new GetUserRequest()
            {
                UserId = request.UserId
            });

            if (userResponse.IsSuccess)
            {
                await _emailProvider.SendAsync(userResponse.User.EmailAddress, "Kindle E-Book", null,
                    postResponse.Post.BlobUrl);
            }
        }
    }
}