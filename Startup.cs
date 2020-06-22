using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreBackEnd.Models;
using BookStoreBackEnd.Models.DataManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BookStoreBackEnd.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("BooksDB")));
            services.AddScoped<IDataRepository<Author, AuthorDto>, AuthorDataManager>();

            services.AddControllers()
                .AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore// This will make sure that the case matches the what json requires
                );
        }
        //https://github.com/CodeMazeBlog/ef-db-first/blob/master/EFCoreDatabaseFirstSample/EFCoreDatabaseFirstSample/Controllers/BooksController.cs

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //https://code-maze.com/asp-net-core-web-api-with-ef-core-db-first-approach/
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
