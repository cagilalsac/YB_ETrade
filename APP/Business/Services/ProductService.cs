using APP.Business.Models;
using APP.DataAccess;
using CORE.Models;
using CORE.Repositoires;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Business.Services
{
    public class ProductService : Service<Product, ProductRequest, ProductResponse>
    {
        public ProductService(RepoBase<Product> repo) : base(repo)
        {
            //CultureInfo = new CultureInfo("tr-TR"); // default culture en-US olacak
        }

        public override Result<List<ProductResponse>> GetList()
        {
            var list = _repo.Query().Include(p => p.Category).OrderByDescending(p => p.IsContinued).ThenBy(p => p.StockAmount).ThenBy(p => p.Name)
                .Select(p => new ProductResponse()
                {
                    CategoryId = p.CategoryId,
                    CreatedBy = p.CreatedBy,
                    Description = p.Description,
                    ExpirationDate = p.ExpirationDate,
                    Guid = p.Guid,
                    Id = p.Id,
                    IsContinued = p.IsContinued,
                    Name = p.Name,
                    StockAmount = p.StockAmount,
                    UnitPrice = p.UnitPrice,
                    UpdatedBy = p.UpdatedBy,

                    //ExpirationDateF = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy HH:mm:ss") : string.Empty,
                    ExpirationDateF = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToShortDateString() : string.Empty,
                    UnitPriceF = p.UnitPrice.ToString("C2"),
                    IsContinuedF = p.IsContinued ? "Continued" : "Discontinued",
                    Category = p.Category.Name
                }).ToList();
            if (list.Any())
                return Success(list, list.Count + " product(s) found.");
            return Error(list, "No products found!");
        }

        public override Result<ProductResponse> GetItem(int id)
        {
            ProductResponse response = null;
            var entity = _repo.Query().Include(p => p.Category).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error(response, "Product not found!");
            response = new ProductResponse()
            {
                CategoryId = entity.CategoryId,
                CreatedBy = entity.CreatedBy,
                Description = entity.Description,
                ExpirationDate = entity.ExpirationDate,
                Guid = entity.Guid,
                Id = entity.Id,
                IsContinued = entity.IsContinued,
                Name = entity.Name,
                StockAmount = entity.StockAmount,
                UnitPrice = entity.UnitPrice,
                UpdatedBy = entity.UpdatedBy,

                //ExpirationDateF = entity.ExpirationDate.HasValue ? entity.ExpirationDate.Value.ToString("MM/dd/yyyy HH:mm:ss") : string.Empty,
                ExpirationDateF = entity.ExpirationDate.HasValue ? entity.ExpirationDate.Value.ToShortDateString() : string.Empty,
                UnitPriceF = entity.UnitPrice.ToString("C2"),
                IsContinuedF = entity.IsContinued ? "Continued" : "Discontinued",
                Category = entity.Category.Name
            };
            return Success(response);
        }

        public override Result Create(ProductRequest request)
        {
            if (_repo.Query().Any(p => p.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");
            var entity = new Product()
            {
                CategoryId = request.CategoryId,
                Description = request.Description?.Trim(),
                ExpirationDate = request.ExpirationDate,
                IsContinued = request.IsContinued,
                Name = request.Name?.Trim(),
                StockAmount = request.StockAmount,
                UnitPrice = request.UnitPrice,
            };
            _repo.Create(entity);
            return Success("Product created successfully.", entity.Id);
        }

        public override Result<ProductRequest> GetItemForEdit(int id)
        {
            ProductRequest request = null;
            var entity = _repo.Query().SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error(request, "Product not found!");
            request = new ProductRequest()
            {
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                ExpirationDate = entity.ExpirationDate,
                IsContinued = entity.IsContinued,
                Name = entity.Name,
                StockAmount = entity.StockAmount,
                UnitPrice = entity.UnitPrice
            };
            return Success(request);
        }

        public override Result Update(ProductRequest request)
        {
            if (_repo.Query().Any(p => p.Id != request.Id && p.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");
            var entity = _repo.Query().SingleOrDefault(p => p.Id == request.Id);
            if (entity is null)
                return Error("Product not found!");
            entity.CategoryId = request.CategoryId;
            entity.Description = request.Description?.Trim();
            entity.ExpirationDate = request.ExpirationDate;
            entity.IsContinued = request.IsContinued;
            entity.Name = request.Name?.Trim();
            entity.StockAmount = request.StockAmount;
            entity.UnitPrice = request.UnitPrice;
            _repo.Update(entity);
            return Success("Product updated successfully.", entity.Id);
        }

        public override Result Delete(int id)
        {
            var entity = _repo.Query().SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Product not found!");
            _repo.Delete(entity);
            return Success("Product deleted successfully.");
        }
    }
}
