using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using DigitalPlanner.Models;

namespace DigitalPlanner.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly DBContext db;

        public CalendarController(DBContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return GoToMonth(DateTime.Now.ToString());
        }

        [Route("/Calendar/GoToMonth/{date}")]
        public IActionResult GoToMonth(string date)
        {
            return View("CalendarIndex", new Calendar(Convert.ToDateTime(date)));
        }
    }
}
