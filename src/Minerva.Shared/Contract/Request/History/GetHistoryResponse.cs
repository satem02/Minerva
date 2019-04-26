using Minerva.Shared.Contract.Models;

namespace Minerva.Shared.Contract.Request.History
{
    public class GetHistoryResponse : ResponseBase
    {
        public HistoryModel History { get; set; }
    }
}