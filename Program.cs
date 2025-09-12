using AmazonApiServer.Data;
using AmazonApiServer.Helpers;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
{
	options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
	new MySqlServerVersion(new Version(8, 0, 36)),
	mysqlOptions =>
	{
		mysqlOptions.EnableRetryOnFailure();
	})
	.LogTo(Console.WriteLine, LogLevel.Information)
	.EnableSensitiveDataLogging()
	.EnableDetailedErrors();
});

builder.Services.AddCors(options => options.AddPolicy("corspolicy", build =>
{
	build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddScoped<IToken, TokenRepository>();
builder.Services.AddScoped<IEmail, EmailRepository>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();
builder.Services.AddScoped<IProductRepo, ProductRepository>();
builder.Services.AddScoped<IOrder, OrderRepository>();


var tokenValidationParameters = JwtConfigHelper.GetTokenValidationParameters(builder.Configuration);

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = true;
	options.SaveToken = true;
	options.TokenValidationParameters = tokenValidationParameters;
	options.MapInboundClaims = false;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUser, UserRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	try
	{
		var dbContext = services.GetRequiredService<ApplicationContext>();
		dbContext.Database.Migrate();

		var applicationContext = services.GetRequiredService<ApplicationContext>();
		await ContentInitializer.InitializeAsync(applicationContext);
	}
	catch (Exception ex)
	{
		var logger = services.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred while seeding the database.");
	}

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
