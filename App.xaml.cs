using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.BearerToken;
using EmbedIO.WebApi;
using System;
using System.Threading.Tasks;
using System.Windows;
using WPFWebServer.WebAPI;
using WPFWebServer.WebAPI.Controller;
using WPFWebServer.WebAPI.WebSocket;

namespace WPFWebServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string INTERNAL_API_URL = "http://localhost:9696/";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Task.Factory.StartNew(async () =>
            {
                var server = CreateWebServer();
                server.StateChanged += (s, e_log) => Console.WriteLine($"WebServer New State - {e_log.NewState}");
                await server.RunAsync();
            });
        }

        private IWebServer CreateWebServer()
        {
            return new WebServer(o => o.WithUrlPrefix(INTERNAL_API_URL).WithMode(HttpListenerMode.EmbedIO))
                            .WithLocalSessionManager()
                            .WithCors()
                            .WithBearerToken("/api", "0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9eyJjbGF", new UserAuthenticationProvider())
                            .WithWebApi("/api/printer", m => m.WithController<PrinterController>())
                            .WithModule(new PrinterEventsModule("/printer/events"))
                            .WithStaticFolder("/", GetHtmlFilesPath(), false, null)
                            .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));
        }

        private string GetHtmlFilesPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
