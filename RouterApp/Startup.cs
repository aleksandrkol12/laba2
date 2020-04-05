using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RouterApp
{
    public class Startup
    {
        string strings;

        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
                endpoints.MapPost("/create", async context =>
                {
                    await context.Response.WriteAsync(await CreateAsync(context));
                });
                endpoints.MapGet("/list", async context =>
                {
                    await context.Response.WriteAsync(Result());
                });
            });
        }

        public async Task<string> CreateAsync(HttpContext context)
        {
            string s;

            using (System.IO.StreamReader sr = new System.IO.StreamReader(context.Request.Body, System.Text.Encoding.UTF8, true, 1024, true))
            {
                s = await sr.ReadToEndAsync();
            }

            strings += s;
            strings += ", ";

            return context.Response.StatusCode.ToString();
        }

        public string Result()
        {
            return strings.TrimEnd(' ', ',');
        }
    }
}