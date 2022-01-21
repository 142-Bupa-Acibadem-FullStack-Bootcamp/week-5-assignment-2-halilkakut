using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Northwind.BusinessLayer;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete.Entityframework.Context;
using Northwind.DataAccess.Concrete.Entityframework.Repository;
using Northwind.DataAccess.Concrete.Entityframework.UnitOfWork;
using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.WebApi
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
            #region JWtTokenService

            //JwtSecurityTokenHandler
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(cfg=>
            {
                cfg.SaveToken = true;
                cfg.RequireHttpsMetadata = false;
                cfg.TokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuer = true,
                    RoleClaimType = "Roles",
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Issuer"], // ["Tokens:Audience"] 
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
            });

            #endregion
            
            #region ServiceSection

            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IMD5Service, MD5Manager>();
            #endregion
            
            #region RepositorySection
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region UnitOfWorks
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
            
            #region Cores Settings

            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("CorsPolicy",
            //        builder => builder
            //        //.WithOrigins("www.api.com"));
            //        .AllowAnyOrigin()
            //        //.WithMethods("GET","POST")
            //        .AllowAnyMethod()
            //        //.WithHeaders("accept","content-type")
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //        );
            //});

            #endregion
            
            #region ApplicationContext
            //DB Baglantý Yontem 2
            //services.AddScoped<DbContext, NorthwindContext>();
            //services.AddDbContext<DbContext, NorthwindContext>();

            //DB Baglantý Yöntem2
            services.AddScoped<DbContext, NorthwindContext>();
            services.AddDbContext<NorthwindContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlOpt =>
                {
                    sqlOpt.MigrationsAssembly("Northwind.DataAccess");
                });
            });
            #endregion
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
