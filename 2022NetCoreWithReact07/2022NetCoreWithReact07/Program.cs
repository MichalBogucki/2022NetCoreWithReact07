
using Microsoft.OpenApi.Models;
using _2022NetCoreWithReact07.Controllers;

namespace _2022NetCoreWithReact07
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
            //    c =>
            //    {
            //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            //
            //    }
            );

            builder.Services.AddHttpClient();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                //    c =>
                //    {
                //        c.SwaggerEndpoint("/v1/swagger.json", "My API V1");
                //    }
                );
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");
            
            app.Run();
        }
    }
}