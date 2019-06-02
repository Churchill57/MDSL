using MDSL.Board.ApiModels;
using MDSL.Board.WebApi.Models;

namespace MDSL.Board.WebApi.Mappers.Interfaces
{
    public interface IReplyMapper
    {
        Reply Map(DiscussionReply reply);
        DiscussionReply Map(Reply reply);
    }
}