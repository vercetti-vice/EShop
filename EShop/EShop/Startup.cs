using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EShop.Helpers;
using EShop.Services;
using EShop.Infrastructure.Data;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EShop.Core.Entities;
using Microsoft.AspNetCore.Http;
using EShop.Extensions;
using EShop.Extensions.Sieve;
using Microsoft.AspNetCore.Diagnostics;
using Sieve.Models;
using Sieve.Services;
using Microsoft.AspNetCore.Identity;

namespace EShop
{
  public class Startup
  {
    private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

    public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration, IHostingEnvironment env)
    {
      Configuration = configuration;
      Environment = env;
    }

    public IConfiguration Configuration { get; }
    public IHostingEnvironment Environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddTransient<DbInitializer>();

      services.AddScoped<ISieveProcessor, SieveProcessor>();
      services.AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>();
      services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
      services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));

      if (Environment.IsDevelopment())
      {
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
      }
      else
      {
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
        //services.AddDbContext<ApplicationDbContext>(options =>
          //options.UseMySql(Configuration.GetConnectionString("DefaultConnection"))); 
      }
      services.AddAutoMapper();
      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.Events = new JwtBearerEvents
        {
          OnTokenValidated = context =>
          {
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var userId = context.Principal.Identity.Name;
            var user = userService.GetById(userId);
            if (user == null)
            {
              // return unauthorized if user no longer exists
              context.Fail("Unauthorized");
            }
            return Task.CompletedTask;
          }
        };
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
      services.AddScoped<IUserService, UserService>();
    }
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer dbInitializer)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      
      if (!env.IsDevelopment())
        using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
          //scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }


      using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var admin = context.Roles.Add(new Core.Entities.Role("Admin"));
        System.Console.WriteLine("Admin created");
        context.Roles.Add(new Core.Entities.Role("User"));
        context.Roles.Add(new Core.Entities.Role("CatalogManager"));
        context.Roles.Add(new Core.Entities.Role("DilevryAgent"));
        var user = scope.ServiceProvider.GetRequiredService<IUserService>().Create(new Core.Entities.AppUser() { UserName = "admin", FirstName = "anton" }, "fynjyufyljy");
        System.Console.WriteLine("registered!");
        context.UserRoles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = admin.Entity.Id });
        System.Console.WriteLine("Role added");
        context.SaveChanges();
        System.Console.WriteLine("Changes saved");
      }


      app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());

      app.UseAuthentication();

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseMvc();

      dbInitializer.Initialize();
    }
  }
}
