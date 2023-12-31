using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TWSE_Trade_Web_API.Helpers;
using TWSE_Trade_Web_API.Helpers.Interface;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.Repositories;
using TWSE_Trade_Web_API.Service;
using TWSE_Trade_Web_API.Service.Interface;

namespace TWSE_Trade_Web_API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TWSE_Trade_Web_API", Version = "v1" });
            });
            //Scaffold-DbContext "Server=LAPTOP-D0C7EI0R;Database=TWSETrade;Trusted_Connection=True;TrustServerCertificate=True;User ID=sa;Password=" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
            services.AddDbContext<TWSETradeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TWSETradeDatabase")));
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<TradeRepository>();
            services.AddScoped<ITwseRequestHelper, TwseRequestHelper>();
            services.AddScoped<ITwseService, TwseService>();
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<IStockService, StockService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TWSE_Trade_Web_API v1"));
            }
            app.UseCors("CorsPolicy");
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
