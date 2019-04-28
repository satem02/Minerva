using Minerva.Shared.Contract.Models;

namespace Minerva.Shared.Contract.Response.Account
{
    public class GetUserResponse : ResponseBase
    {
        public UserModel User { get; set; }
    }
}