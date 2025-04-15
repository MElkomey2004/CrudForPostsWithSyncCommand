using CRUDforPostswithSyncCommand.Data;
using CRUDforPostswithSyncCommand.Entities;
using CRUDforPostswithSyncCommand.JwtFeatures;
using CRUDforPostswithSyncCommand.Mapper;
using CRUDforPostswithSyncCommand.Repositories;
using CRUDforPostswithSyncCommand.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IPostRepository, PostRepsitory>();
builder.Services.AddHttpClient(); // For IHttpClientFactory
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddDbContext<PostDbContext>(option =>

	option.UseSqlServer(builder.Configuration.GetConnectionString("PostConnection")).EnableSensitiveDataLogging() 
		   .LogTo(Console.WriteLine, LogLevel.Information) 
);
builder.Services.AddDbContext<AuthDbContext>(option =>

	option.UseSqlServer(builder.Configuration.GetConnectionString("PostAuthConnection"))
);


builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddAutoMapper(typeof(PostMappingProfile));

var jwtSettings = builder.Configuration.GetSection("JWTSettings");

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings["validIssuer"],
		ValidAudience = jwtSettings["validAudience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
			.GetBytes(jwtSettings.GetSection("securityKey").Value))
	};
});

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme.",
		Name = "Authorization",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
	});
	options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});



builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
	options.AddPolicy("ReviewerPolicy", policy => policy.RequireRole("Reviewer"));

	options.AddPolicy("AdminOrReviewerPolicy", policy => policy.RequireRole("Admin", "Reviewer"));
});


builder.Services.AddSingleton<JWTHandler>();


builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
