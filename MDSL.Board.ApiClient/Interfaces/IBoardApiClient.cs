using System.Collections.Generic;
using System.Threading.Tasks;
using MDSL.Board.ApiModels;

namespace MDSL.Board.ApiClient.Interfaces
{
    public interface IBoardApiClient
    {
        Task<IEnumerable<Reply>> GetRepliesByThreadAsync(long threadId);
        Task<Reply> GetReplyAsync(long Id);
        Task<Thread> GetThreadAsync(long Id);
        Task<IEnumerable<Thread>> GetThreadsAsync();
        Task<Reply> PostReplyAsync(Reply reply);
        Task<Thread> PostThreadAsync(Thread thread);
    }
}