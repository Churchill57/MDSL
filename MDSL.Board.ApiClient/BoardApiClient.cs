using MDSL.Board.ApiClient.Interfaces;
using MDSL.Board.ApiModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MDSL.Board.ApiClient
{
    public class BoardApiClient : IBoardApiClient
    {
        private static readonly Lazy<HttpClient> lazyHttpClient = new Lazy<HttpClient>(() =>
        {
            var handler = new HttpClientHandler() { UseDefaultCredentials = true };
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["MDSL.Board.WebApi.BaseURL"])
            };
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        });

        private HttpClient httpClient => lazyHttpClient.Value;


        public async Task<Thread> GetThreadAsync(long Id)
        {
            var response = await httpClient.GetAsync($"GetThreadAsync/{Id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidOperationException($"Discussion thread {Id} does not exist");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<Thread>().Result;
        }

        public async Task<IEnumerable<Thread>> GetThreadsAsync()
        {
            var response = await httpClient.GetAsync($"GetThreadsAsync");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidOperationException($"Failed to load discussion threads");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<IEnumerable<Thread>>().Result;
        }

        public async Task<Thread> PostThreadAsync(Thread thread)
        {
            var response = await httpClient.PostAsJsonAsync<Thread>("PostThreadAsync", thread);
            response.EnsureSuccessStatusCode();
            //var newResourceLocation = response.Headers.Location;
            return await response.Content.ReadAsAsync<Thread>();
        }


        public async Task<Reply> GetReplyAsync(long Id)
        {
            var response = await httpClient.GetAsync($"GetReplyAsync/{Id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidOperationException($"Discussion reply {Id} does not exist");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<Reply>().Result;
        }

        public async Task<IEnumerable<Reply>> GetRepliesByThreadAsync(long threadId)
        {
            var response = await httpClient.GetAsync($"GetRepliesByThreadAsync/{threadId}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidOperationException($"Failed to load discussion replies for thread {threadId}");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<IEnumerable<Reply>>().Result;
        }

        public async Task<Reply> PostReplyAsync(Reply reply)
        {
            var response = await httpClient.PostAsJsonAsync<Reply>("PostReplyAsync", reply);
            response.EnsureSuccessStatusCode();
            //var newResourceLocation = response.Headers.Location;
            return await response.Content.ReadAsAsync<Reply>();
        }


    }
}
