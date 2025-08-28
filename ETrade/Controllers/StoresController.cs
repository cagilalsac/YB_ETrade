#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.Services;
using APP.DataAccess;
using APP.Business.Models;

// Generated from Custom MVC Template.

namespace ETrade.Controllers
{
    public class StoresController : Controller
    {
        // Service injections:
        private readonly Service<Store, StoreRequest, StoreResponse> _storeService;

        /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
        //private readonly Service<Entity, EntityRequest, EntityResponse> _EntityService;

        public StoresController(
			Service<Store, StoreRequest, StoreResponse> storeService

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //, EntityService EntityService
        )
        {
            _storeService = storeService;

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //_EntityService = EntityService;
        }

        private void SetViewData()
        {
            // Related items service logic to set ViewData (Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //ViewBag.EntityIds = new MultiSelectList(_EntityService.GetList(), "Id", "Name");
        }

        // GET: Stores
        public IActionResult Index()
        {
            // Get collection service logic:
            var result = _storeService.GetList();
            return View(result.Data);
        }

        // GET: Stores/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var result = _storeService.GetItem(id);
            return View(result.Data);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Stores/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(StoreRequest store)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _storeService.Create(store);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(store);
        }

        // GET: Stores/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var result = _storeService.GetItemForEdit(id);
            SetViewData();
            return View(result.Data);
        }

        // POST: Stores/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(StoreRequest store)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _storeService.Update(store);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(store);
        }

        // GET: Stores/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var result = _storeService.GetItem(id);
            return View(result.Data);
        }

        // POST: Stores/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _storeService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
