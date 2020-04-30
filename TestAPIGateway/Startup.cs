using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;

namespace TestAPIGateway
{
    public class Startup
    {
        IConfiguration config;
        public Startup(IConfiguration configuration)
        {
            config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot(config).AddSingletonDefinedAggregator<CustomAggregator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOcelot().Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }

    public class CustomAggregator : IDefinedAggregator
    {
        //public Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            var movieContext = responses.First(r => r.DownstreamReRoute.Key.Equals("movies"));
            var userContext = responses.First(r => r.DownstreamReRoute.Key.Equals("users"));

            var content1 = await userContext.DownstreamRequest.Content.ReadAsStringAsync();

            var content2 = await userContext.DownstreamResponse.Content.ReadAsStringAsync();

            var stringContent = new StringContent("Nothing Found")
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
