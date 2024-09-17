using GeekShopping.ProductAPI.Model.Context;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using GeekShopping.ProductAPI.Services;
using GeekShopping.ProductAPI.Repositories;
using Newtonsoft.Json;
using AutoMapper;
using GeekShopping.ProductAPI.Config;

namespace GeekShopping.ProductAPI
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
            var connection = Configuration.GetConnectionString("MySQLConnectionString");

            //services.AddDbContext<MySQLContext>(opt =>
            //{
            //    opt.UseMySql(Configuration.GetConnectionString("MySqlConnectionPC"));
            //    //opt.UseMySql(connection, new MySqlServerVersion(new Version(8,0,34)));
            //});
            services.AddDbContext<MySQLContext>(opt => 
            opt.UseMySql(connection, new MySqlServerVersion(new Version(8,0,39))));

            services.AddControllers()
                .AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddScoped<ProductService>();
            services.AddScoped<IRepository, Repository>();

            #region DI do automapper
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.ProductAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekShopping.ProductAPI v1"));
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
