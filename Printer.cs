using System;
using System.Linq;
using System.Threading.Tasks;

namespace WPFWebServer
{
    public delegate void OnComplete(object sender, EventArgs e);
    public delegate void OnFailure(object sender, EventArgs e);

    public class Printer
    {
        private Printer(){}

        public static Printer Instance { get; } = new Printer();


        public void Print(string text)
        {
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                
                if (text.Count() > 10)
                {
                    OnFailure?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnComplete?.Invoke(this, EventArgs.Empty);
                }
            });
        }

        public event OnComplete OnComplete;
        public event OnFailure OnFailure;

    }
}
