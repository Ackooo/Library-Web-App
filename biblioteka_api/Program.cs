using biblioteka_api.Extensions;
using biblioteka_api.Hubs;
using biblioteka_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace biblioteka_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); 

            builder.Services.AddDbContext<BooksContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BooksContext")));
            var frontendURL = builder.Configuration.GetValue<string>("frontend_url");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                        .WithOrigins(frontendURL)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()//);
                        .WithExposedHeaders(new string[] { "maxNumberOfUnits" }));
            });


            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<SeederService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BooksContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = false,
                        ValidateAudience= false,
                        ValidateLifetime = false, // 
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))//,
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("isLibrarian", policy => policy.RequireClaim("role", "librarian"));
                options.AddPolicy("isVisitor", policy => policy.RequireClaim("role", "visitor"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<RefreshDataHub>("/api/refreshdata");


            var scope = app.Services.CreateScope();
            var seederService = scope.ServiceProvider.GetRequiredService<SeederService>();
            seederService.SeedData();

            app.Run();
        }

    }
}