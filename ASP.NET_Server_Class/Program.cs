using ASP.NET_Server_Class;
using ASP.NET_Server_Class.Models;
using ASP.NET_Server_Class.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

//using ASP.NET_Server_Class.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true

        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Type JWT token"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
         }

    });
});
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


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
//builder.Services.AddScoped<ProductService>();
//builder.Services.AddScoped<OrderService>();
//builder.Services.AddScoped<ProductsInOrdersService>();
//builder.Services.AddScoped<DoctorsService>();
//builder.Services.AddScoped<DepartmentsService>();
//builder.Services.AddScoped<DoctorsSpecializationsService>();
//builder.Services.AddScoped<SpecializationsService>();
builder.Services.AddScoped<FieldService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
