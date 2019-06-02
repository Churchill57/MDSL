using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MDSL.Board.WebApi.Models;
using MDSL.Board.WebApi.Mappers.Interfaces;
using System.Web.Http.Cors;

namespace MDSL.Board.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ThreadsController : ApiController
    {
        private readonly BoardDBContext db;
        private readonly IThreadMapper mapper;

        public ThreadsController() : this(new BoardDBContext(), new ThreadMapper())
        {
            // TODO: Poor man's dependency injection in lieu of working IOC
        }

        public ThreadsController(BoardDBContext db, IThreadMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        // TODO: Ideally implement paging here
        [Route("GetThreadsAsync")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ApiModels.Thread>))]
        public async Task<IHttpActionResult> GetThreadsAsync()
        {
            var discussionThreads = await (
                db.DiscussionThreads.OrderByDescending(t => t.Id) //// newest first, oldest last
                ).ToListAsync();

            var threads =
                from t in discussionThreads
                select mapper.Map(t);

            return Ok(threads);
        }

        [Route("GetThreadAsync/{Id}")]
        [HttpGet]
        [ResponseType(typeof(ApiModels.Thread))]
        public async Task<IHttpActionResult> GetThreadAsync(int id)
        {
            var discussionThread = await db.DiscussionThreads.FindAsync(id);
            if (discussionThread == null) return NotFound();
            var thread = mapper.Map(discussionThread);
            return Ok(thread);
        }

        [Route("PostThreadAsync")]
        [HttpPost]
        [ResponseType(typeof(ApiModels.Thread))]
        public async Task<IHttpActionResult> PostThreadAsync(ApiModels.Thread thread)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var discussionThread = mapper.Map(thread);
            db.DiscussionThreads.Add(discussionThread);
            await db.SaveChangesAsync();
            return Ok(mapper.Map(discussionThread));
        }

    }
}