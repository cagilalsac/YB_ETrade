using APP.Business.Models;
using APP.DataAccess;
using CORE.Models;
using CORE.Repositoires;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Business.Services
{
    public class StoreService : Service<Store, StoreRequest, StoreResponse>
    {
        private readonly RepoBase<ProductStore> _productStoreRepo;

        public StoreService(RepoBase<Store> repo, RepoBase<ProductStore> productStoreRepo) : base(repo)
        {
            _productStoreRepo = productStoreRepo;
        }

        public override Result Create(StoreRequest request)
        {
            //string errorMessage = "A " + (request.IsVirtual ? "Virtual" : "Physical") + " store with the same name already exists!";
            string errorMessage = $"A {(request.IsVirtual ? "Virtual" : "Physical")} store with the same name already exists!";

            if (_repo.Query().Any(s => s.Name.ToLower() == request.Name.ToLower().Trim() && s.IsVirtual == request.IsVirtual))
                return Error(errorMessage);

            var entity = new Store
            {
                Name = request.Name.Trim(),
                IsVirtual = request.IsVirtual
            };

            _repo.Create(entity);

            return Success("Store created successfully.", entity.Id);
        }

        public override Result Delete(int id)
        {
            var entity = _repo.Query()
                .Include(store => store.ProductStores)
                .SingleOrDefault(store => store.Id == id);

            if (entity is null)
                return Error("Store not found!");

            foreach (var productStore in entity.ProductStores)
            {
                _productStoreRepo.Delete(productStore, false, false);
            }

            _repo.Delete(entity);

            return Success("Store deleted successfully.");
        }

        public override Result<StoreResponse> GetItem(int id)
        {
            StoreResponse response = null;

            var entity = _repo.Query()
                .Include(store => store.ProductStores).ThenInclude(productStore => productStore.Product)
                .SingleOrDefault(store => store.Id == id);

            if (entity is null)
                return Error(response, "Store not found!");

            response = new StoreResponse
            {
                CreatedBy = entity.CreatedBy,
                Guid = entity.Guid,
                Id = entity.Id,
                IsVirtual = entity.IsVirtual,
                Name = entity.Name,
                UpdatedBy = entity.UpdatedBy,

                IsVirtualF = entity.IsVirtual ? "Virtual" : "Physical",
                ProductCount = entity.ProductStores.Count,
                Products = string.Join("<br>", entity.ProductStores.Select(productStore => productStore.Product.Name))
            };

            return Success(response);
        }

        public override Result<StoreRequest> GetItemForEdit(int id)
        {
            StoreRequest request = null;

            var entity = _repo.Query().SingleOrDefault(store => store.Id == id);

            if (entity is null)
                return Error(request, "Store not found!");

            request = new StoreRequest
            {
                Id = entity.Id,
                IsVirtual = entity.IsVirtual,
                Name = entity.Name
            };

            return Success(request);
        }

        public override Result<List<StoreResponse>> GetList()
        {
            var list = _repo.Query()
                .Include(store => store.ProductStores).ThenInclude(productStore => productStore.Product)
                .OrderByDescending(s => s.IsVirtual).ThenBy(s => s.Name).Select(s => new StoreResponse
            {
                CreatedBy = s.CreatedBy,
                Guid = s.Guid,
                Id = s.Id,
                IsVirtual = s.IsVirtual,
                Name = s.Name,
                UpdatedBy = s.UpdatedBy,

                IsVirtualF = s.IsVirtual ? "Virtual" : "Physical",
                ProductCount = s.ProductStores.Count,
                Products = string.Join("<br>", s.ProductStores.Select(productStore => productStore.Product.Name))
            }).ToList();
            if(list.Any())
                return Success(list, list.Count + " store(s) found.");
            return Error(list, "No stores found!");
        }

        public override Result Update(StoreRequest request)
        {
            //string errorMessage = "A " + (request.IsVirtual ? "Virtual" : "Physical") + " store with the same name already exists!";
            string errorMessage = $"A {(request.IsVirtual ? "Virtual" : "Physical")} store with the same name already exists!";

            if (_repo.Query().Any(s => s.Id != request.Id && s.Name.ToLower() == request.Name.ToLower().Trim() && s.IsVirtual == request.IsVirtual))
                return Error(errorMessage);

            var entity = _repo.Query().SingleOrDefault(s => s.Id == request.Id);
            if (entity is null)
                return Error("Store not found!");

            entity.Name = request.Name.Trim();
            entity.IsVirtual = request.IsVirtual;

            _repo.Update(entity);

            return Success("Store updated successfully.", entity.Id);
        }
    }
}
