using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class PredmetController : Controller
    {

        private readonly U_NetContext db;
        public PredmetController(U_NetContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View(await db.Specialnocti.ToListAsync());
        }
        
    }
}
