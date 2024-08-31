using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCRev.PL.Models;
using MVCRev.PL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRev.PL.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly IScopedServies scoped1;
        private readonly IScopedServies scoped2;

        private readonly ITransientServies transient1;
        private readonly ITransientServies transient2;

        private readonly ISingletonServies singleton1;
        private readonly ISingletonServies singleton2;

        public HomeController(ILogger<HomeController> logger , 
            IScopedServies scoped1
            ,IScopedServies scoped2
            ,ITransientServies transient1,
            ITransientServies transient2,
            ISingletonServies singleton1,
            ISingletonServies singleton2)
        {
            _logger = logger;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.singleton1 = singleton1;
            this.singleton2 = singleton2;
        }

        public IActionResult Index()
        {
            return View();
        }


        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scoped01 :: {scoped1.GetGuid()}\n");
            builder.Append($"scoped02 :: {scoped2.GetGuid()}\n\n");
            
            builder.Append($"transient1 :: {transient1.GetGuid()}\n");
            builder.Append($"transient2 :: {transient2.GetGuid()} \n\n");

            builder.Append($"singleton1 :: {singleton1.GetGuid()} \n");
            builder.Append($"singleton2 :: {singleton2.GetGuid()} \n\n");

            return builder.ToString();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
