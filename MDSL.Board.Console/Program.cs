using MDSL.Board.ApiClient;
using MDSL.Board.ApiClient.Interfaces;
using MDSL.Board.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSL.Board.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin Board API console test");
            Console.WriteLine("Press a key to continue...");
            Console.ReadLine();

            IBoardApiClient client = new BoardApiClient(); // TODO: Use dependency resolver here

            Task task;

            IEnumerable<Thread> threads = null;
            task = Task.Run(async () => threads = await client.GetThreadsAsync());
            task.Wait();
            Console.WriteLine($"Thread count {threads.Count()}");


            var threadNew = new Thread
            {
                Title = "Test post method",
                Body = "Once upon a time in a galaxy far, far away...",
                Created = DateTime.Now,
                CreatedBy = "The System"
            };
            Thread threadCreated = null;
            task = Task.Run(async () => threadCreated = await client.PostThreadAsync(threadNew));
            task.Wait();
            Console.WriteLine($"Thread {threadCreated.Id} added");


            var replyNew = new Reply
            {
                ThreadId = threadCreated.Id,
                Body = "This is a system generated reply",
                Created = DateTime.Now,
                CreatedBy = "The System"
            };
            Reply replyCreated = null;
            task = Task.Run(async () => replyCreated = await client.PostReplyAsync(replyNew));
            task.Wait();
            Console.WriteLine($"Reply {replyCreated.Id} added to thread {threadCreated.Id}");

            Reply replyJustCreated;
            task = Task.Run(async () => replyJustCreated = await client.GetReplyAsync(replyCreated.Id));
            task.Wait();
            Console.WriteLine($"Reply {replyCreated.Id} body {replyCreated.Body}");

            IEnumerable<Reply> replies = null;
            task = Task.Run(async () => replies = await client.GetRepliesByThreadAsync(threadCreated.Id));
            task.Wait();
            Console.WriteLine($"Thread {threadCreated.Id} has reply count {replies.Count()}");

            Console.WriteLine();
            Console.WriteLine("Press a key to end");
            Console.ReadLine();

        }
    }
}
