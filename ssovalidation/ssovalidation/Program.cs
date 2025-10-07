//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Identity.Web;
//using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Configure Azure AD authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//	.AddMicrosoftIdentityWebApi(options =>
//	{
//		builder.Configuration.Bind("AzureAd", options);

//		// Optional: loosen validation temporarily for testing
//		options.TokenValidationParameters.ValidateIssuer = true;
//		options.TokenValidationParameters.ValidAudience = builder.Configuration["AzureAd:Audience"];
//	},
//	options =>
//	{
//		builder.Configuration.Bind("AzureAd", options);
//	});

//builder.Services.AddAuthorization();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen(opt =>
//{
//	opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
//	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//	{
//		In = ParameterLocation.Header,
//		Description = "Please enter token",
//		Name = "Authorization",
//		Type = SecuritySchemeType.Http,
//		BearerFormat = "JWT",
//		Scheme = "bearer"
//	});

//	opt.AddSecurityRequirement(new OpenApiSecurityRequirement
//	{
//		{
//			new OpenApiSecurityScheme
//			{
//				Reference = new OpenApiReference
//				{
//					Type=ReferenceType.SecurityScheme,
//					Id="Bearer"
//				}
//			},
//			new string[]{}
//		}
//	});
//});

//var app = builder.Build();

//// Enable Swagger
//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure JWT Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = "https://login.microsoftonline.com/3dd2467e-56db-4aec-98c8-6a014ddf0376/v2.0";
		options.Audience = "api://93b0b079-15f7-4313-acc2-12155fa32f73";
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = "https://sts.windows.net/3dd2467e-56db-4aec-98c8-6a014ddf0376/",
			ValidateAudience = true,
			ValidAudience = "api://93b0b079-15f7-4313-acc2-12155fa32f73",
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true
		};
	});

builder.Services.AddAuthorization();

// Add Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "TokenProtectedApi", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter 'Bearer' followed by your token"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
