using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class PrepodavatelController : Controller
    {
        private readonly U_NetContext db;
        public PrepodavatelController(U_NetContext db)
        {
            this.db = db;
        }

        #region Интерфейс Специальность + Сортировка + Фильтр 
        public async Task<IActionResult> Index(string? sortOrder,string? searchByName)
        {
            ViewBag.IdSortParam =(String.IsNullOrEmpty(sortOrder))?"Id_Desc" : "";
            ViewBag.NameSortParam = (sortOrder == "SortName") ? "SortName_Desc" : "SortName";

            IQueryable<Prepodavateli> prep = db.Prepodavateli;

            //Поиск по имени
            if (!String.IsNullOrEmpty(searchByName))
            {
                ViewBag.SearchByName=searchByName;
                searchByName = searchByName.Trim(' ');
                prep = prep.Where(x => EF.Functions.Like(x.PrepName, $"%{searchByName}%"));
            }
            //Сортировка
            switch (sortOrder)
            {
                case "Id_Desc":
                    prep = prep.OrderByDescending(x => x.Id);
                    break;

                case "SortName":
                    prep = prep.OrderBy(x => x.PrepName);
                    break;

                case "SortName_Desc":
                    prep = prep.OrderByDescending(x => x.PrepName);
                    break;

                default:
                    prep = prep.OrderBy(x => x.Id);
                    break;

            }

            return View( await prep.ToListAsync());
        }

        #endregion
        
        #region Добовить преподаватель

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Prepodavateli prep)
        {
            await db.Prepodavateli.AddAsync(prep);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Изменить преподватель

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var prep = await db.Prepodavateli.FirstOrDefaultAsync(p => p.Id == id);
                if (prep != null)
                    return View(prep);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Prepodavateli prep)
        {
            db.Prepodavateli.Update(prep);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удалить преподаватель 
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var prep= await db.Prepodavateli.FirstOrDefaultAsync(p => p.Id == id);

                if (prep != null)
                {
                    db.Prepodavateli.Remove(prep);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return NotFound();
        }
        #endregion

    }
}
