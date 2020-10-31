using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomationWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomationWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult WeatherTableView()
        {
            var model = _dashboardService.GetAll();
            return View(model);
        }

        [Authorize]
        public IActionResult PhoneRing()
        {
            _dashboardService.PhoneRing();
            return RedirectToAction("Index", "Home");
        }

        // GET: TemperatureSensorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TemperatureSensorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TemperatureSensorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TemperatureSensorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TemperatureSensorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TemperatureSensorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
