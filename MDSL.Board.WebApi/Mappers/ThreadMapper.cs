using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MDSL.Board;
using MDSL.Board.WebApi.Mappers.Interfaces;
using MDSL.Board.WebApi.Models;

namespace MDSL.Board.WebApi
{
    internal class ThreadMapper : IThreadMapper
    {
        public DiscussionThread Map(ApiModels.Thread thread)
        {
            return new DiscussionThread
            {
                Id = thread.Id,
                Title = thread.Title,
                Body = thread.Body,
                Created = thread.Created,
                CreatedBy = thread.CreatedBy
            };
        }
        public ApiModels.Thread Map(DiscussionThread thread)
        {
            return new ApiModels.Thread
            {
                Id = thread.Id,
                Title = thread.Title,
                Body = thread.Body,
                Created = thread.Created,
                CreatedBy = thread.CreatedBy
            };
        }
    }
}