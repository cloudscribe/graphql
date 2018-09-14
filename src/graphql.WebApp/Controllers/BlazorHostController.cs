using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace graphql.WebApp.Controllers
{
    public class BlazorHostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
