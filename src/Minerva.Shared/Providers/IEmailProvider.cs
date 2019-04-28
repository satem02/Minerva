using System.Threading.Tasks;

namespace Minerva.Shared.Providers
{
    public interface IEmailProvider
    {
        Task SendAsync(string emailAddress, string subject);
        Task SendAsync(string emailAddress, string subject, string contentBody, string attachment);
    }
}