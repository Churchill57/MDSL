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
    public class RepliesController : ApiController
    {
        private readonly BoardDBContext db;
        private readonly IReplyMapper mapper;

        public RepliesController() : this(new BoardDBContext(), new ReplyMapper())
        {
            // TODO: Poor man's dependency injection in lieu of working IOC
        }

        public RepliesController(BoardDBContext db, IReplyMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [Route("GetReplyAsync/{id}")]
        [HttpGet]
        [ResponseType(typeof(ApiModels.Reply))]
        public async Task<IHttpActionResult> GetReplyAsync(int id)
        {
            var discussionReply = await db.DiscussionReplies.FindAsync(id);
            if (discussionReply == null) return NotFound();
            var reply = mapper.Map(discussionReply);
            return Ok(reply);
        }

        // TODO: Consider implementing paging here
        [Route("GetRepliesByThreadAsync/{id}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ApiModels.Reply>))]
        public async Task<IHttpActionResult> GetRepliesByThreadAsync(int id)
        {
            var discussionThread = await db.DiscussionThreads.FindAsync(id);
            if (discussionThread == null) return NotFound();
            var replies = from r in discussionThread.DiscussionReplies
                          orderby r.Id ascending // oldest first, newest last
                          select mapper.Map(r);
            return Ok(replies);
        }


        [Route("PostReplyAsync")]
        [HttpPost]
        [ResponseType(typeof(ApiModels.Reply))]
        public async Task<IHttpActionResult> PostReplyAsync(ApiModels.Reply reply)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var discussionReply = mapper.Map(reply);
            db.DiscussionReplies.Add(discussionReply);
            await db.SaveChangesAsync();
            return Ok(mapper.Map(discussionReply));
        }

    }
}