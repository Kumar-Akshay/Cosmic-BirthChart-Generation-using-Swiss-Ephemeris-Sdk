using CosmicGameAPI.Entities;
using CosmicGameAPI.Service.Helper;
using CosmicGameAPI.Service.Implementation;
using CosmicGameAPI.Service.Interface;
using CosmicGameAPI.Utility;
using CosmicGameAPI.Utility.Constant;
using CosmicGameAPI.Utility.JWT;
using CosmicGameAPI.Utility.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register the services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<JWTAuthentication>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<IChartHolderService, ChartHolderService>();
builder.Services.AddTransient<IChartsService, ChartsService>();
builder.Services.AddTransient<IBhavaPlanetService, BhavaPlanetService>();
builder.Services.AddSingleton(typeof(IBaseAutoMapper<,>), typeof(BaseAutoMapper<,>));


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GlobalVars.JwtKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration.GetSection("AuthToken").GetSection("Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("AuthToken").GetSection("Audience").Value,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
         .SetIsOriginAllowed(_ => true)
        .AllowCredentials();
}));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Astrology API's",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddDbContext<CosmicDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.MapControllers();

app.Run();
