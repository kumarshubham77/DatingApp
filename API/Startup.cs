// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using API.Data;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.OpenApi.Models;
// using API.Interfaces;
// using API.Services;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using API.Extensions;
// using API.Middleware;
// using API.SignalR;

// namespace API
// {
//     public class Startup
//     {
//         private readonly IConfiguration _config;

//         public Startup(IConfiguration config)
//         {
//             _config = config;

//         }


//         // This method gets called by the runtime. Use this method to add services to the container.
//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddApplicationServices(_config);
//             services.AddControllers();
//             // services.AddSwaggerGen(c =>
//             // {
//             //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
//             // });
//             services.AddCors();
//             services.AddIdentityServices(_config);

//             // services.AddMvc(options => {
//             //     options.SuppressAsyncSuffixInActionNames = false;
//             // });
            
//         }

//         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             // if (env.IsDevelopment())
//             // {
//             //     app.UseDeveloperExceptionPage();
//             //     // app.UseSwagger();
//             //     // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
//             // }

//             app.UseMiddleware<ExceptionMiddleware>();
//             //If we come in for http address then we get redirected to the https endpoints.
//             app.UseHttpsRedirection();

//             app.UseRouting();

//             app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200"));

//             app.UseAuthentication();

//             app.UseAuthorization();

//             app.UseDefaultFiles();
//             app.UseStaticFiles();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//                 endpoints.MapHub<PresenceHub>("hubs/presence"); 
//                 endpoints.MapHub<MessageHub>("hubs/message");
//                 endpoints.MapFallbackToController("Index","Fallback");
//             });
            
//         }
//     }
// }
