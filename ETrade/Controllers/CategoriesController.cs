using APP.Business.Models;
using APP.DataAccess;
using CORE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly Service<Category, CategoryRequest, CategoryResponse> _service;

        public CategoriesController(Service<Category, CategoryRequest, CategoryResponse> service)
        {
            _service = service;
        }

        // GET: Categories/Index
        // GET: Categories
        public IActionResult Index()
        {
            var result = _service.GetList();

            // Way 1:
            //ViewData["RecordsCount"] = result.Message;
            // Way 2:
            ViewBag.RecordsCount = result.Message;

            return View(result.Data);
        }

        // GET: Categories/Details/1
        public IActionResult Details(int id)
        {
            var result = _service.GetItem(id);
            ViewBag.Message = result.Message;
            return View(result.Data);
        }

        // GET: Category/Create
        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = _service.Create(request);
            if (!result.IsSuccessful)
            {
                ViewBag.Message = result.Message;
                return View(request);
            }
            TempData["OperationMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
