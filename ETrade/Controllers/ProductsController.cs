#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.Services;
using APP.DataAccess;
using APP.Business.Models;

// Generated from Custom MVC Template.

namespace ETrade.Controllers
{
    public class ProductsController : Controller
    {
        // Service injections:
        private readonly Service<Product, ProductRequest, ProductResponse> _productService;
        private readonly Service<Category, CategoryRequest, CategoryResponse> _categoryService;

        /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
        //private readonly Service<Entity, EntityRequest, EntityResponse> _EntityService;

        public ProductsController(
			Service<Product, ProductRequest, ProductResponse> productService
            , Service<Category, CategoryRequest, CategoryResponse> categoryService

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //, EntityService EntityService
        )
        {
            _productService = productService;
            _categoryService = categoryService;

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //_EntityService = EntityService;
        }

        private void SetViewData()
        {
            // Related items service logic to set ViewData (Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList().Data, "Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //ViewBag.EntityIds = new MultiSelectList(_EntityService.GetList(), "Id", "Name");
        }

        // GET: Products
        public IActionResult Index()
        {
            // Get collection service logic:
            var result = _productService.GetList();
            return View(result.Data);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var result = _productService.GetItem(id);
            return View(result.Data);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Products/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _productService.Create(product);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var result = _productService.GetItemForEdit(id);
            SetViewData();
            return View(result.Data);
        }

        // POST: Products/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _productService.Update(product);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var result = _productService.GetItem(id);
            return View(result.Data);
        }

        // POST: Products/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _productService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
