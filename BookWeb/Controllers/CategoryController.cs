using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Controllers
{
    //[Authorize]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string search,int pg = 1)
        {
            List<Category> objCategoryList = _db.categories.ToList();
            // поиск
            ViewData["Getnamedetails"] = search;
            if (!string.IsNullOrEmpty(search))
            {
                objCategoryList = objCategoryList.Where(x => x.Name.Contains(search) || x.Progress.Contains(search)).ToList();
            }
            // нумерация страниц
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = objCategoryList.Count();
            
            var pager = new PaginationViewModel(recsCount,pg,pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = objCategoryList.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.PaginationViewModel = pager;

            return View(data);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Порядок отображения такой же какое и имя");
            }
            if(obj.Name == null)
            {
                ModelState.AddModelError("CustomError", "Имя не задано");
            }
            if(obj.DisplayOrder == 0)
            {
                ModelState.AddModelError("CustomError", "Порядок отображения пуст");
            }
            if(ModelState.IsValid)
            { 
            _db.categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Категория успешно создана";
            return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.categories.Find(id);
            //var categoryFromDbFirst = _db.categories.FirstOrDefault(u => u.Id == id)
            //var categoryFromDbSingle = _db.categories.SingleOrDefault(u => u.Id == id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Порядок отображения такой же какое и имя");
            }
            if (obj.Name == null)
            {
                ModelState.AddModelError("CustomError", "Имя не задано");
            }
            if (obj.DisplayOrder == 0)
            {
                ModelState.AddModelError("CustomError", "Порядок отображения пуст");
            }
            if (ModelState.IsValid)
            {
                _db.categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Категория успешно изменена";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.categories.Find(id);
            //var categoryFromDbFirst = _db.categories.FirstOrDefault(u => u.Id == id)
            //var categoryFromDbSingle = _db.categories.SingleOrDefault(u => u.Id == id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }    
            _db.categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Категория успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
