using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerDiaryDevChallenge.Chat;
using PowerDiaryDevChallenge.Models;
using PowerDiaryDevChallenge.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PowerDiaryDevChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatAggregateService _chatAggregateService;

        public HomeController(IChatAggregateService chatAggregateService)
        {
            _chatAggregateService = chatAggregateService;
        }


        public IActionResult Index(string level = "Seconds")
        {
            var model = new ChatViewModel();
            model.SelectedAgrregationLevel = level;
            model.ChatEntries = _chatAggregateService.GetChatEntriesByAggregateLevel((AggregationLevels)Enum.Parse(typeof(AggregationLevels), level));
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
