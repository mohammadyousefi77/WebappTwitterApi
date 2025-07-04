using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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


builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    //options.SignIn.RequireConfirmedPhoneNumber = true;// ???? ?? ????? ????? ????
    options.Password.RequireDigit = true; //????? ???? ???? ??? ????
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    

})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUintOfWork,UnitOfWork>();
builder.Services.AddScoped<IUserServices, UserSevices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
