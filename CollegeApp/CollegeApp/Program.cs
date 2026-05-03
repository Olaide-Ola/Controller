using CollegeApp.Configuration;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.MyLogging;
using CollegeApp.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().
    MinimumLevel.Information()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
    .CreateLogger();

builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});

builder.Logging.AddSerilog();
// Add services to the container.
builder.Logging.AddSerilog();

builder.Services.AddControllers(
    //options => options.ReturnHttpNotAcceptable = true
    ).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddScoped<IUserService,UserService>();

builder.Services.AddScoped<IMyLogger, LogToServerMemory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//we want to confiugre JWT authentication for Swagger here
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization Header using the bearer scheme. Enter Bearer [space] add your token in the text input. Example: Bearer 4h4738@#!",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",

                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});




//builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddAutoMapper(cfg => { /*optional global config */ },
    typeof(Program).Assembly);

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));

//CORS Configuration

builder.Services.AddCors(options =>
{
    //options.AddDefaultPolicy(policy =>                          //this is a default policy. The app will use DefaultPolicy. make sure it is app.UseCors()
    //{
    //    //Allow all origin
    //    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();

    //});
    options.AddPolicy("AllowFrontend", policy =>
    {
        //Allow few origins
        policy.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
    options.AddPolicy("AllowGoogle", policy =>
    {
        //Allow few origins
        policy.WithOrigins("https://google.com/", "https://gmail.com/", "https://drive.com/").AllowAnyHeader().AllowAnyMethod();
    });

});

//JWT Authentication Configuration

var FrontEndkey = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWT:Frontend:Key"));
var MSkey = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWT:MSUser:Key"));
var LocalUSerkey = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWT:Local:Key"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer("LoginFrontendJS", options =>
{
    options.RequireHttpsMetadata = false; //CHECK WITH TUTOR AGAIN
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(FrontEndkey),

        ValidateIssuer = true,
        ValidateAudience = true,

        ValidIssuer = builder.Configuration["JWT:Frontend:Issuer"],
        ValidAudience = builder.Configuration["JWT:Frontend:Audience"]
    };
}).AddJwtBearer("LoginMSUser", options =>
{
    options.RequireHttpsMetadata = false; //CHECK WITH TUTOR AGAIN
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(MSkey),

        ValidateIssuer = true,
        ValidateAudience = true,

        ValidIssuer = builder.Configuration["JWT:MSUser:Issuer"],
        ValidAudience = builder.Configuration["JWT:MSUser:Audience"]
    };
}).AddJwtBearer("LoginLocalUsers", options =>
{
    options.RequireHttpsMetadata = false; //CHECK WITH TUTOR AGAIN
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(LocalUSerkey),

        ValidateIssuer = true,
        ValidateAudience = true,

        ValidIssuer = builder.Configuration["JWT:Local:Issuer"],
        ValidAudience = builder.Configuration["JWT:Local:Audience"]
    };
});
//builder.Services.AddAuthorizationBuilder()
//    .AddPolicy("LoginFrontendJS", policy =>
//    policy.RequireClaim(ClaimTypes.Role, "Admin"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); //check this if correct and research about it with mapcontroller

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("api/testingendpoint",
//        context => context.Response.WriteAsync("Test Response"))
//        .RequireCors("AllowGoogle");

//    endpoints.MapControllers()
//             .RequireCors("AllowFrontend");

//    endpoints.MapGet("api/testingendpoint2",
//        context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWT:Key")));

//    //endpoints.MapRazorPages();
//});

app.MapControllers();

app.Run();
