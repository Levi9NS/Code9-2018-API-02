using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code9Insta.API.Core.DTO;
using Code9Insta.API.Helpers;
using Code9Insta.API.Helpers.Interfaces;
using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Interfaces;
using Code9Insta.API.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Code9Insta.API_02
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
            var connectionString = Configuration["connectionStrings:CodeNineDBConnectionString"];
            services.AddDbContext<CodeNineDbContext>(o => o.UseSqlServer(connectionString));

            // ===== Add Identity ========
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<CodeNineDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "code9.com",
                        ValidAudience = "code9.com",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                    };
                });

            // register the repository
            services.AddScoped<IValidateRepository, ValidateRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            //register helpers
            services.AddScoped<IAuthorizationManager, AuthorizationManager>();

            //AutoMapper configuration
            AutoMapper.Mapper.Initialize(conf =>
            {
                conf.CreateMap<CreateProfileDto, Profile>();
                conf.CreateMap<AccountDto, ApplicationUser>();
                conf.CreateMap<Profile, GetProfileDto>()
                    .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CodeNineDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            dbContext.Database.EnsureCreated();
        }
    }
}
