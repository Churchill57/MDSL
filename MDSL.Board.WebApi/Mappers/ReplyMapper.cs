using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MDSL.Board;
using MDSL.Board.WebApi.Mappers.Interfaces;
using MDSL.Board.WebApi.Models;

namespace MDSL.Board.WebApi
{
    public class ReplyMapper : IReplyMapper
    {
        public DiscussionReply Map(ApiModels.Reply reply)
        {
            return new DiscussionReply
            {
                Id = reply.Id,
                Body = reply.Body,
                Created = reply.Created,
                CreatedBy = reply.CreatedBy,
                ThreadId = reply.ThreadId
            };
        }
        public ApiModels.Reply Map(DiscussionReply reply)
        {
            return new ApiModels.Reply
            {
                Id = reply.Id,
                Body = reply.Body,
                Created = reply.Created,
                CreatedBy = reply.CreatedBy,
                ThreadId = reply.ThreadId
            };
        }

    }
}