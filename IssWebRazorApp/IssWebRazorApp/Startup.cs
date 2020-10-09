using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;

namespace IssWebRazorApp
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
            services.AddRazorPages();

            services.AddDbContext<IssWebRazorAppContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("IssWebRazorAppContext")));

            //セッション機能の追加
            services.AddSession(option => 
            {
                option.Cookie.Name = "IssApp";
                option.IdleTimeout = TimeSpan.FromDays(10);
                option.Cookie.IsEssential = true;
            });
            
            //ObjectRepositoryの為
            services.AddScoped<IPlaybookRepository,PlaybookRepository>();
            services.AddScoped<IScheduleRepository,ScheduleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFootballNoteRepository, FootballNoteRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            //AWS Configファイル読み込みの為
            services.AddOptions();
            services.Configure<AmazonWebServiceConfig>(Configuration.GetSection(nameof(AmazonWebServiceConfig)));
            services.Configure<LINENotifyServiceConfig>(Configuration.GetSection(nameof(LINENotifyServiceConfig)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
