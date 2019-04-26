using System;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Minerva.Shared.Common;
using Minerva.Shared.Contract.Response.Post;
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
        public EmailConsumer(IEmailProvider emailProvider, IPostService postService)
        {
            _emailProvider = emailProvider;
            _postService = postService;
        }

        [CapSubscribe(Constants.EmailQueue)]
        public async Task SendEmail(SendEmailRequest request)
        {
            var response = await _postService.GetPostAsync(new GetPostRequest()
            {
                Id = request.PostId
            });

            if (!response.IsSuccess)
            {
                throw new Exception("Post is not found");
            }

            await _emailProvider.SendAsync(request.EmailAddress, "Kindle E-Book");
        }
    }

    public interface IEmailProvider
    {
        Task SendAsync(string emailAddress, string subject);
    }

    public class SendEmailRequest
    {
        public int PostId { get; set; }
        public string EmailAddress { get; set; }
    }
}