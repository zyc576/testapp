﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            int a=1;
            return View();
        }
    }
}
