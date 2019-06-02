using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MDSL.Board;
//using MDSL.Board.ApiClient.Interfaces;
//using MDSL.Board.ApiClient;

namespace MDSL.Board.UI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DiscussionBoardController : Controller
    {
        //private readonly IBoardApiClient client;

        //public DiscussionBoardController() : this(new BoardApiClient())
        //{
        //    // TODO: Poor man's dependency injection in lieu of working IOC
        //}
        //public DiscussionBoardController(IBoardApiClient client)
        //{
        //    this.client = client;
        //}

        [HttpGet("[action]")]
        public IEnumerable<ThreadViewModel> GetThreads()
        {
            var rng = new Random();
            var threads = Enumerable.Range(1, rng.Next(1, 10)).Select(i => new ThreadViewModel
            {
                id = i,
                title = $"Title#{i}",
                body = $"Body#{i}",
                created = DateTime.Now.AddDays(i).ToString("d"),
                createdBy = "The System"
            });
            return threads;
        }

        //[HttpGet("[action]")]
        //public IEnumerable<ThreadViewModel> GetThreads()
        //{
        //    Task task;
        //    IEnumerable<ApiModels.Thread> threads = null;
        //    task = Task.Run(async () => threads = await client.GetThreadsAsync());
        //    task.Wait();

        //    var threadsVM = from t in threads
        //                    select new ThreadViewModel
        //                    {
        //                        Id = t.Id,
        //                        Title = t.Title,
        //                        Body = t.Body,
        //                        Created = t.Created.ToString("d"),
        //                        CreatedBy = t.CreatedBy
        //                    };

        //    return threadsVM;
        //}

        public class ThreadViewModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
            public string created { get; set; }
            public string createdBy { get; set; }

        }

    }
}

