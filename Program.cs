using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace VSCodeTest
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static readonly HttpClient client = new HttpClient();

        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");


            var tStream = await streamTask;
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(tStream);

            Console.WriteLine("Found Repositories:\n------------");
            foreach (var repository in repositories)
            {
                Console.WriteLine(repository.name);
            }
        }




        public class Repository
        {
            public string name { get; set; }

        }

    }


}