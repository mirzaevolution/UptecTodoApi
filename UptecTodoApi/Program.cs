using Microsoft.Identity.Web;
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

            services.AddScoped<ITodoService, TodoService>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}