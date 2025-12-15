using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using System.Text;
using WeblogSample.Data.Contexts;
using WeblogSample.Service.Interfaces;
using WeblogSample.Service.Mappers.Articles;
using WeblogSample.Service.Mappers.Categories;
using WeblogSample.Service.Mappers.Comments;
using WeblogSample.Service.Mappers.Persons;
using WeblogSample.Service.Mappers.Roles;
using WeblogSample.Service.Services;
using WeblogSample.WebApp.Infra.JWTGenerator;
using WeblogSample.WebApp.Infra.PdfGenerator;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddScoped<IArticlePdfGenerator, QuestArticlePdfGenerator>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataBaseContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("access_token"))
            {
                context.Token = context.Request.Cookies["access_token"];
            }
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            context.Response.Redirect("/Account/AccessDenied");
            return Task.CompletedTask;
        }
    };

    options.Events.OnChallenge = context =>
    {
        context.Response.Redirect("/Account/Login");
        context.HandleResponse();
        return Task.CompletedTask;
    };
});

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddAuthorization();

builder.Services.AddScoped<ArticleMapper>();
builder.Services.AddScoped<CategoryMapper>();
builder.Services.AddScoped<CommentMapper>();
builder.Services.AddScoped<PersonMapper>();
builder.Services.AddScoped<RoleMapper>();

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStatisticsService,StatisticsService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.UseAuthentication();
app.UseAuthorization();

app.Run();
