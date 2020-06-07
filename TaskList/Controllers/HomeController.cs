using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskList.Models;
using TaskList.ViewModels;
using Microsoft.AspNetCore.Authorization;
using TaskList.ViewModel;

namespace TaskList.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _applicationContext;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexModel indexModel = new IndexModel();
            if (HttpContext.User.Identity.Name != null)
            {
                // indexModel.userTasks = await _applicationContext.Tasks.FindAsync();
            }
            return View();
        }


    }

}
