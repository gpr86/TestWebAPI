using System;
using System.Linq;
using System.Collections.Generic;
using TestWebAPI.Model;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace TestWebAPI.API
{
    public class WebAPI
    {
        public RestClient GetClient()
        {
            var client = new RestClient(@"https://crudcrud.com/api/d6a89cad82f3455280159e79078a87e2");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            RestSharp.Serializers.SystemTextJson.RestClientExtensions.UseSystemTextJson(client, options);
            return client;
        }
    }
}
