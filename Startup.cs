using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace restclient
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                string strOutput = "List of repos at https://api.github.com/orgs/dotnet/repos:" + Environment.NewLine;
                strOutput += Environment.NewLine;

                var repositories = ProcessRepositories().Result;
                foreach (var repo in repositories)
                    strOutput += repo.ToString() + Environment.NewLine;
                strOutput += Environment.NewLine;
                strOutput += "Coded by Edsel Clarin.";

                return context.Response.WriteAsync(strOutput);
            });
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            var client = new HttpClient();

            // Reset headers.
            client.DefaultRequestHeaders.Accept.Clear();

            // Configure to accept JSON response.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/vnd.github.v3+json"));

            // Add the User Agent.
            client.DefaultRequestHeaders.Add(
                "User-Agent", ".NET Foundation Repository Reporter");

            // Make the web request, and deserialize the response into a list 
            // of respositories.
            var serializer = new DataContractJsonSerializer(
                typeof(List<Repository>));
            var streamTask = client.GetStreamAsync(
                "https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask)
                as List<Repository>;

            return repositories;
        }
    }
}