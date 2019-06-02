using MDSL.Board.ApiModels;
using MDSL.Board.WebApi.Models;

namespace MDSL.Board.WebApi.Mappers.Interfaces
{
    public interface IThreadMapper
    {
        Thread Map(DiscussionThread thread);
        DiscussionThread Map(Thread thread);
    }
}