using System.Net;

namespace Minerva.Shared.Contract
{
    public class ResponseBase
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
    }
}