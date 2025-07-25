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
            return View(result.Data);
        }
    }
}
