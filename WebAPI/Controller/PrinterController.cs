using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System.Threading.Tasks;

namespace WPFWebServer.WebAPI.Controller
{
    class PrinterController: WebApiController
    {
        public PrinterController() : base(){ }

        [Route(HttpVerbs.Post, "/")]
        public async Task<string> Print()
        {
            var data = await HttpContext.GetRequestDataAsync<PrintData>();
            var printer = Printer.Instance;
            printer.Print(data.Text);
            return "OK";
        }
    }

    public class PrintData
    {
        public string Text { get; set; }
    }
}
