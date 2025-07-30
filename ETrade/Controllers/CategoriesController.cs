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
            //ViewData["Message"] = result.Message;
            // Way 2:
            ViewBag.Message = result.Message;

            return View(result.Data);
        }

        public IActionResult Details(int id)
        {
            var result = _service.GetItem(id);
            ViewBag.Message = result.Message;
            return View(result.Data);
        }
    }
}
