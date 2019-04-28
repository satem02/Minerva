using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AzureStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minerva.Job.Extensions;
using Minerva.Shared.Extensions;

namespace Minerva.Job
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
            services.AddCap()
                .AddAzureStorage(new AzureStorageOptions()
                {
                    AccountKey = Configuration["AzureStorage:AccountKey"],
                    AccountName = Configuration["AzureStorage:AccountName"]
                })
                .AddConsumers()
                .AddRepositoryLayer()
                .AddServiceLayer()
                .AddCustomIdentity();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
