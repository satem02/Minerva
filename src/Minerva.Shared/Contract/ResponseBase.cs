using System.Net;
using Minerva.Shared.Contract.Models;

namespace Minerva.Shared.Contract
{
    public class ResponseBase
    {
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
    }
}