using RestSharp;
using System.Collections.Generic;
using TestWebAPI.Model;
using Serilog;

namespace TestWebAPI.API
{
    public class WorkersResource
    {
        readonly string resource = "workers";
        readonly WebAPI api;

        public WorkersResource(WebAPI api) => this.api = api;

        public IRestResponse<Worker> Create(Worker worker)
        {
            var resource = this.resource;
            var client = api.GetClient();
            var request = new RestRequest(resource, Method.POST);
            request.AddJsonBody(worker);

            //Log.Information($"User sends POST request with Body \r\n {request.Body.Value}");
            Log.Information($"User sends POST request with Body ");

            var response = client.Execute<Worker>(request);

            //Log.Information();

            return response;
        }

        public IEnumerable<IRestResponse<Worker>> Create(IEnumerable<Worker> workers)
        {
            var responses = new List<IRestResponse<Worker>>();

            foreach (var wkr in workers)
            {
                var response = Create(wkr);
                responses.Add(response);
            }

            return responses;
        }

        public IRestResponse<List<Worker>> Read()
        {
            var resource = this.resource;
            var client = api.GetClient();
            var request = new RestRequest(resource, Method.GET);
            var response = client.Execute<List<Worker>>(request);

            return response;
        }

        public IRestResponse<Worker> Read(string id)
        {
            var resource = $"{this.resource}/{id}";
            var client = api.GetClient();
            var request = new RestRequest(resource, Method.GET);
            var response = client.Execute<Worker>(request);

            return response;
        }
        
        public IRestResponse Update(string id, Worker wkr)
        {
            var resource = this.resource;
            var client = api.GetClient();
            var request = new RestRequest($"{resource}/{id}", Method.PUT);
            request.AddJsonBody(wkr);
            var response = client.Execute(request);

            return response;
        }

        public IRestResponse Delete(string id)
        {
            var resource = this.resource;
            var client = api.GetClient();
            var request = new RestRequest($"{resource}/{id}", Method.DELETE);
            var response = client.Execute(request);

            return response;
        }

        public IEnumerable<IRestResponse> Delete(IEnumerable<string> ids)
        {
            var resource = this.resource;
            var client = api.GetClient();
            var responses = new List<IRestResponse>();

            foreach (var id in ids)
            {
                var request = new RestRequest($"{resource}/{id}", Method.DELETE);
                var response = client.Execute(request);
                responses.Add(response);
            }
            
            return responses;
        }
    }
}
