using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using WebappTwitterApi.Contract;
using WebappTwitterApi.Data;
using WebappTwitterApi.Data.Entity;
using WebappTwitterApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'SQLServerIdentityConnection' not found."); ;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<ApplicationDbContext>();


//==========================================================================================
//builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
//builder.Services.AddAuthorization();

//builder.Services.AddIdentity<User, IdentityRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//    //options.SignIn.RequireConfirmedPhoneNumber = true;// ???? ?? ????? ????? ????
//    options.Password.RequireDigit = true; //????? ???? ???? ??? ????
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;


//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddApiEndpoints()
//    .AddDefaultTokenProviders();
//builder.Services.AddAuthorizationBuilder();

//======================================

//builder.Services.AddDefaultIdentity<User>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//    //options.SignIn.RequireConfirmedPhoneNumber = true;// ???? ?? ????? ????? ????
//    options.Password.RequireDigit = true; //????? ???? ???? ??? ????
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;


//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddApiEndpoints();
//builder.Services.AddAuthorizationBuilder();

//identity2

//builder.Services.AddDefaultIdentity<IdentityUser>(
//    options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>().AddApiEndpoints();
//builder.Services.AddAuthorizationBuilder(); ;


// 🔹 JWT Auth
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(key)
//    };
//});
//====================================================================================
builder.Services.AddAuthorization();

builder.Services.AddScoped<IUintOfWork,UnitOfWork>();
builder.Services.AddScoped<IUserServices, UserSevices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title="Auth Demo",
        Version = "v1"

    });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat="JWT",
        Scheme = "Bearer"
    });
 

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
      {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            //Scheme = "oauth2",
            //Name = "Bearer",
            //In = ParameterLocation.Header,

        },
        [] 
    }
          });
    
});



var app = builder.Build();

app.MapIdentityApi<User>();
//app.MapSwagger().RequireAuthorization("Admin");
//app.MapGet("/test", () => "Hello from minimal API");
//app.MapGroup("/api/user").MapIdentityApi<User>();
//app.MapGet("/api/user/claims", (ClaimsPrincipal user) => user.Identity?.Name)
//   .RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();




app.MapControllers();

 app.Run();
