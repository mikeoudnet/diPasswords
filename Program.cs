using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using diPasswords.Infrastructure.Data;
using diPasswords.Infrastructure.Logging;
using diPasswords.Application.Services;
using diPasswords.Application.Interfaces;
using diPasswords.Presentation.Managers;
using diPasswords.Presentation.Views;
using diPasswords.Infrastructure;

namespace diPasswords
{
    internal static class Program
    {
        /// <summary>
        /// Dependencies customization
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=diPasswords;Trusted_Connection=True;";                        
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataService, DataService>();

            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IValidator, Validator>();
            services.AddSingleton<IElementView, ElementView>();
            services.AddSingleton<IDataValidator, DataValidator>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IDataListShower, DataListShower>();
            services.AddSingleton<IEncrypter, Encrypter>();

            services.AddTransient<diPasswordsForm>();
            services.AddTransient<DataEditorForm>();
        }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            ConfigureServices(services);

            using var provider = services.BuildServiceProvider();
            var scope = provider.CreateScope();
            var form = scope.ServiceProvider.GetRequiredService<diPasswordsForm>();

            System.Windows.Forms.Application.Run(form);
        }
    }
}