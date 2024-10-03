using AutoMapper;
using FSU.SPORTIDY.API.Payloads.Responses;
using FSU.SPORTIDY.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text;
using FSU.SPORTIDY.Service.Mapping;
using FSU.SPORTIDY.API.Middlewares;
using FSU.SPORTIDY.Repository.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FSU.SPORTIDY.Repository.Interfaces;
using FSU.SPORTIDY.Repository.Repositories;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Services;
using FSU.SPORTIDY.Service.Utils.Mail;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
using FSU.SPORTIDY.Service.Services.PaymentServices;
using FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels;
using FSU.SPORTIDY.API.Validations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SportidyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// return json with kabab case 
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Sportidy API", Version = "v1" });
    option.OperationFilter<FileUploadOperationFilter>();

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    }); option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
// Add Mapping profiles
var mapper = new MapperConfiguration(mc =>
{
    mc.AddProfile<MappingProfile>();
});

// config payment PayOS
builder.Services.Configure<PayOSKey>(builder.Configuration.GetSection("PayOS"));
builder.Services.AddScoped<PayOSKey>();
builder.Services.AddSingleton(mapper.CreateMapper());

// Register repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserTokenRepository, UserTokenRepository>();
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IPlayFieldFeedbackRepository, PlayFieldFeedbackRepository>();
builder.Services.AddScoped<ISystemFeedbackRepository, SystemFeedbackRepository>();
builder.Services.AddScoped<IPlayFieldRepository, PlayFieldRepository>();
builder.Services.AddScoped<IFriendShipRepository, FriendShipRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ICommentInMeetingRepository, CommentInMeetingRepository>();
builder.Services.AddScoped<IImageFieldRepository, ImageFeldReposiotory>();



// Register servicies
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IPlayFieldFeedbackService, PlayFieldFeedbackService>();
builder.Services.AddScoped<ISystemFeedbackService, SystemFeedbackService>();
builder.Services.AddScoped<IPlayFieldService, PlayFieldService>();
builder.Services.AddScoped<IFriendShipService, FriendShipService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICommentInMeetingService, CommentInMeetingService>();
builder.Services.AddScoped<WebSocketService>();

builder.Services.AddScoped<IPayOSService, PayOSService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IImageFieldService, ImageFieldService>();

// add mail settings
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));

// setup firebase
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("sportidy-447fd-firebase-adminsdk-7qf6b-a43214214a.json")
});


//Config Jwt Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

// Add CORS
builder.Services.AddCors(p => p.AddPolicy("Cors", policy =>
{
    policy.WithOrigins("*")
          .AllowAnyHeader()
          .AllowAnyMethod();
}));
// add  json option để tránh vòng lặp tại json khi trả về
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = false;
});

builder.Services.AddSwaggerGen(options => {
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});

var app = builder.Build();

app.UseCors("Cors");

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


// Config Middleware
//app.UseMiddleware<AccountStatusMiddleware>();
app.UseMiddleware<TokenValidationMiddleware>();

app.UseHttpsRedirection();
var webSocketOptions = new WebSocketOptions()
{
    KeepAliveInterval = TimeSpan.FromMinutes(5),
};

app.UseWebSockets(webSocketOptions);
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();