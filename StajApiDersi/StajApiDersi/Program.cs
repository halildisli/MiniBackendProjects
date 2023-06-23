using Microsoft.EntityFrameworkCore;
using StajApiDersi.Context;
using StajApiDersi.Repositories.Abstract;
using StajApiDersi.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
}));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StajApiDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myHome")));

builder.Services.AddTransient<DbContext, StajApiDBContext>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

app.UseCors();

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
