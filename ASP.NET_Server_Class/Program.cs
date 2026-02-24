using ASP.NET_Server_Class;
using ASP.NET_Server_Class.Models;
using ASP.NET_Server_Class.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConection"));
});


builder.Services.AddCors(options => options.AddPolicy("AllowReact", policy => 
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    }
));
//builder.WebHost.UseUrls("http://0.0.0.0:5000");

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductsInOrdersService>();
builder.Services.AddScoped<DoctorsService>();
builder.Services.AddScoped<DepartmentsService>();
builder.Services.AddScoped<DoctorsSpecializationsService>();
builder.Services.AddScoped<SpecializationsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
