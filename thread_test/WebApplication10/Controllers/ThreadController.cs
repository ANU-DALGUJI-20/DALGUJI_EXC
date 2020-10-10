using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace thread_test.Controllers
{
	public class ThreadController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
