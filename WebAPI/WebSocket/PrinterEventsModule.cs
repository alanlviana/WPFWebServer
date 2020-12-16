using EmbedIO.WebSockets;
using System.Threading.Tasks;

namespace WPFWebServer.WebAPI.WebSocket
{
    public class PrinterEventsModule: WebSocketModule
    {

        private readonly Printer Printer;

        public PrinterEventsModule(string urlPath)
            : base(urlPath, true)
        {
            Printer = Printer.Instance;
            Printer.OnComplete += Printer_OnComplete;
            Printer.OnFailure += Printer_OnFailure;
        }

        private void Printer_OnFailure(object sender, System.EventArgs e)
        {
            BroadcastAsync("Printing failure");
        }

        private void Printer_OnComplete(object sender, System.EventArgs e)
        {
            BroadcastAsync("Completed printing");

        }

        protected override Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
        {
            // do nothing
            return Task.CompletedTask;
        }
    }
}
