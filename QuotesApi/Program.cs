using Microsoft.AspNetCore.Authentication.JwtBearer;
using QuotesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuotesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Auth:Token").Value))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddDbContext<QuotesContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("QuotesConnectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(typeof(IQuoteService), typeof(QuoteService));
builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(
    options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
);
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