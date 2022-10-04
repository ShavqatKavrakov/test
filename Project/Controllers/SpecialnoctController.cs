using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
namespace Project.Controllers
{
    public class SpecialnoctController : Controller
    {
        private readonly U_NetContext db;
        public SpecialnoctController(U_NetContext db)
        {
            this.db = db;
        }

        #region Интерфейс Специальность + Сортировка + Фильтр 
        public async Task<IActionResult> Index(string? sortOrder,string? searchByName)
        {  
         
            ViewBag.IdSortParam = String.IsNullOrEmpty(sortOrder) ? "Id_Desc" : "";
            ViewBag.NameSortParam = (sortOrder == "SpecName") ? "SpecName_Desc" : "SpecName";

         
            IQueryable<Specialnocti> spec = db.Specialnocti;

            //Поиск по имени
             if (!String.IsNullOrEmpty(searchByName))
            {
                ViewBag.SearchByname = searchByName;
                searchByName = searchByName.Trim(' ');
                spec = spec.Where(x =>EF.Functions.Like(x.SpecName, $"%{searchByName}%"));
            }   

            //Сортировка
            switch (sortOrder)
            {
                case "Id_Desc":
                   spec=spec.OrderByDescending(x => x.Id);
                   break;
                case "SpecName":
                    spec = spec.OrderBy(x => x.SpecName);
                    break;
                case "SpecName_Desc":
                    spec = spec.OrderByDescending(x => x.SpecName);
                    break;
                default:
                    spec = spec.OrderBy(x => x.Id);
                    break;
            }

            return View(await spec.ToListAsync());
        }

        #endregion

        #region Добовить специальность 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Specialnocti spec)
        {
            db.Specialnocti.Add(spec);
            await db.SaveChangesAsync();
            return RedirectToAction(controllerName: "Specialnoct", actionName: "Index");
        }
        #endregion

        #region Удалить специальность 
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Specialnocti? spec = await db.Specialnocti.FirstOrDefaultAsync(p => p.Id == id);
                if (spec != null)
                {
                    db.Specialnocti.Remove(spec);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        #endregion

        #region Изменить специальность
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Specialnocti? spec = await db.Specialnocti.FirstOrDefaultAsync(p => p.Id == id);
                if (spec != null) return View(spec);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Specialnocti spec)
        {
            db.Specialnocti.Update(spec);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
