using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServerHost.Quickstart.UI;
using IDP.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Users;
using Services;
using Services.Context;
using Services.PolicyManger;
using System;
using System.Reflection;

namespace IDP
{
    public class Startup
    {
        readonly IWebHostEnvironment _currentEnvironment;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _currentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //if (_currentEnvironment.IsDevelopment())
            //{
                // development
                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlServer(DbConnectionStrings.DevelopmentConnectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    });

                }, ServiceLifetime.Scoped);


                services.AddIdentity<ApplicationUser, Role>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

                services.AddIdentityServer()
                .AddOperationalStore(options =>

                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(DbConnectionStrings.DevelopmentConnectionString, sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(migrationsAssembly);

                            sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);

                        }))

                //.AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiScopes(Config.GetScopes())
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryCaching()
                .AddDeveloperSigningCredential();
            //}

            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            services.AddTransient<IProfileService, IdentityClaimsProfileService>();
            services.AddScoped<IMailService, MailService>();

            services.AddCors();
            services.AddTransient<ICorsPolicyProvider, CorsPolicyManager>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDbTestData(app);

            app.UseCors("default");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddleware(typeof(ErrorTrackingMiddleware));
            app.UseIdentityServer();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void InitializeDbTestData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                //scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
                //scope.ServiceProvider.GetRequiredService<IDPContext>().Database.Migrate();
            }
        }
    }
}
