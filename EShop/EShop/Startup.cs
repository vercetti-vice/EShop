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
using Microsoft.AspNetCore.Http;
using EShop.Extensions;
using Microsoft.AspNetCore.Diagnostics;


namespace EShop
{
  public class Startup
  {
    private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

    public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      // TODO : Uncomment
      // services.AddDbContext<ApplicationDbContext>(options =>
      //   options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

      // TODO : Comment
      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("ApplicationDbContext"));

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
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      // TODO : Uncomment
      //using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      //{
      //  scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
      //}

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
    }
  }
}
