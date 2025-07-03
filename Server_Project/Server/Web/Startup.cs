using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Server.Database;
using Server.Services;
using Server.Services.IService;
using System;
using System.Text.Json;

namespace Server.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 웹 API 컨트롤러 추가
            services.AddControllers();

            // API Explorer (Swagger용)
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Restaurant API", Version = "v1" });
            });

            // CORS 설정
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // 데이터베이스 컨텍스트 설정
            services.AddDbContext<ServerdbContext>();

            // JSON 설정
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    options.JsonSerializerOptions.WriteIndented = false;
                });

            // 서비스 등록
            services.AddScoped<IKioskService, KioskService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IWaitingService, WaitingService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IMenuService, MenuService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API v1"));
            }

            // 로깅 미들웨어
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {context.Request.Method} {context.Request.Path}");
                await next();
            });

            // CORS 적용
            app.UseCors("AllowAll");

            // 정적 파일 서빙
            app.UseStaticFiles();

            // 라우팅
            app.UseRouting();

            // API 엔드포인트 설정
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // 헬스체크 엔드포인트
                endpoints.MapGet("/health", async context =>
                {
                    var response = new
                    {
                        status = "healthy",
                        timestamp = DateTime.UtcNow,
                        server = "Restaurant API Server"
                    };

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
                });
            });
        }

        public static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = false
        };
    }
}