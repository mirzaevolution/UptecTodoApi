using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using UptecTodoApi.Services;

namespace UptecTodoApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var config = builder.Configuration;
			var services = builder.Services;

			services.AddControllers();
			services.AddMicrosoftIdentityWebApiAuthentication(config);

			string[] scopes =
				config["Swagger:Scopes"]?.Split(' ') ?? new string[0];

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "UptecTodoApi",
					Version = "v1",
				});
				c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
				{
					Name = "OAuth2",
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						AuthorizationCode = new OpenApiOAuthFlow
						{
							AuthorizationUrl = new Uri(config["Swagger:AuthorizationEndpoint"] ?? string.Empty),
							TokenUrl = new Uri(config["Swagger:TokenEndpoint"] ?? string.Empty),
							Scopes = scopes?.ToDictionary(c => c)
						}
					}
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme, Id = "OAuth2"
							}
						},
						scopes
					}
				});
			});

			services.AddScoped<ITodoService, TodoService>();

			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo Api v1");
				c.OAuthClientId(config["Swagger:ClientId"]);
				c.OAuthUsePkce();
				c.OAuthScopes(scopes);
			});

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}