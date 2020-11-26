using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using HomeAutomationWebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HomeAutomationWebApp.Models.DbModels;
using HomeAutomationWebApp.Services.Interfaces;
using HomeAutomationWebApp.Services;
using HomeAutomationWebApp.Controllers;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Serilog;
using Serilog.Events;

namespace HomeAutomationWebApp
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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
#if DEBUG
                /*.WriteTo.MSSqlServer(
                    Configuration.GetSection("Serilog").GetValue<string>("MsSlqlConnectionString"),
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions { AutoCreateSqlTable = true, TableName = "Logs" })*/
                .WriteTo.UdpSyslog(Configuration.GetSection("Serilog").GetValue<string>("SyslogServer"))
#endif
                /*.WriteTo.Console(
                    outputTemplate: Configuration.GetSection("Serilog").GetValue<string>("OutputTemplate1"),
                    restrictedToMinimumLevel: LogEventLevel.Debug)*/
                .WriteTo.File(
                    AppDomain.CurrentDomain.BaseDirectory + @"/WebApp-Log-.txt",
                    outputTemplate: Configuration.GetSection("Serilog").GetValue<string>("OutputTemplate1"),
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddDbContext<AzureDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureFakeDbConnection")));
            services.AddIdentity<IotUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
                .AddEntityFrameworkStores<AzureDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailTokenProvider<IotUser>>("emailconfirmation");
            services.AddScoped<SignInManager<IotUser>>();
            services.AddScoped<UserManager<IotUser>>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddTransient<IWebAppEmailService, WebAppEmailService>();
            services.AddMailKit(options => options.UseMailKit(Configuration.GetSection("MailKitOptions").Get<MailKitOptions>()));

            services.AddControllersWithViews();
            //services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
