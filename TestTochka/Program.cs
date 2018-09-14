using System;

namespace TestTochka
{
    class Program
    {
        static void Main(string[] args)
		{
			var app = new Startup();
			var sp = app.Init();
			app.Start(sp);
        }
    }
}
