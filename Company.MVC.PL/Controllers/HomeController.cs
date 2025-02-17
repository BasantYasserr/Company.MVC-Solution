using Company.MVC.PL.ViewModels;
using Company.MVC.PL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Company.MVC.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IScopedService _scoped01;
        //private readonly IScopedService _scoped02;
        //private readonly ITransientService _transient01;
        //private readonly ITransientService _transient02;
        //private readonly ISingletonService _singleton01;
        //private readonly ISingletonService _singleton02;

        public HomeController(ILogger<HomeController> logger)
                              //IScopedService scoped01,
                              //IScopedService scoped02,
                              //ITransientService transient01,
                              //ITransientService transient02,
                              //ISingletonService singleton01,
                              //ISingletonService singleton02)
        {
            _logger = logger;
            //_scoped01 = scoped01;
            //_scoped02 = scoped02;
            //_transient01 = transient01;
            //_transient02 = transient02;
            //_singleton01 = singleton01;
            //_singleton02 = singleton02;
        }

        //GET /Home/TestLifeTime
        //public string TestLifeTime()
        //{
        //    StringBuilder builder = new StringBuilder();

        //    builder.Append($"scoped01 :: {_scoped01.GetGuid()}\n");
        //    builder.Append($"scoped02 :: {_scoped02.GetGuid()}\n\n");

        //    builder.Append($"transient01 :: {_transient01.GetGuid()}\n");
        //    builder.Append($"transient02 :: {_transient02.GetGuid()}\n\n");
            
        //    builder.Append($"singleton01 :: {_singleton01.GetGuid()}\n");
        //    builder.Append($"singleton02 :: {_singleton02.GetGuid()}\n");
           
        //}

        public IActionResult Index()
        {
            return View();
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
