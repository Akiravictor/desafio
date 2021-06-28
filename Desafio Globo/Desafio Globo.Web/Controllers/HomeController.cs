using Desafio_Globo.Application.Interfaces;
using Desafio_Globo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio_Globo.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IGetProcessingDataUseCase processingDataUseCase;

		public HomeController(ILogger<HomeController> logger,
			IGetProcessingDataUseCase processingDataUseCase)
		{
			_logger = logger;
			this.processingDataUseCase = processingDataUseCase;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> AwsUsage()
		{

			var model = await processingDataUseCase.ExecuteAsync();

			return View("AwsUsage", model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
