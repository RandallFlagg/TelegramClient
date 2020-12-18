using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Terminal.Gui;

namespace TelegramClient
{
    partial class Program
    {
        private static MyTelegramOptions _options;

        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            Application.Init();
            // Application code should start here.

            //await host.RunAsync();
            //Application.Run(new TelegramTerminalClientWindow());
            //Application.Top.Add(new TelegramTerminalClientWindow());
            var top = Application.Top;
            var a = new TelegramTerminalClientWindow(_options);
            top.Add(a);
            Application.Run();
            //Application.Run<TelegramTerminalClientWindow>();
            //Application.Run();
            //StartClient();
            //Console.WriteLine("Press any key to exit");
            //Console.ReadLine();
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                IConfigurationRoot configurationRoot = configuration.Build();

                //var options = new MyTelegramOptions();
                _options = new MyTelegramOptions();
                configurationRoot.GetSection(nameof(MyTelegramOptions)).Bind(_options);

                Console.WriteLine($"MyTelegramOptions.apiHash={_options.apiHash}");
                Console.WriteLine($"MyTelegramOptions.apiId={_options.apiId}");
                Console.WriteLine($"MyTelegramOptions.phoneNumber={_options.phoneNumber}");
            });
    }
}