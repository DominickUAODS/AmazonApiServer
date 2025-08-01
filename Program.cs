using Microsoft.EntityFrameworkCore;
using AmazonApiServer.Data;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Repositories;
using AmazonApiServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    ).LogTo(Console.WriteLine, LogLevel.Information)
);

builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();
builder.Services.AddSingleton<IImageService, ImageService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dbContext.Database.Migrate();
}

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
