using APP.Business.Models;
using APP.DataAccess;
using CORE.Models;
using CORE.Repositoires;
using CORE.Services;

namespace APP.Business.Services
{
    public class CategoryService : Service<Category, CategoryRequest, CategoryResponse>
    {
        public CategoryService(RepoBase<Category> repo) : base(repo)
        {
        }

        public override Result<List<CategoryResponse>> GetList()
        {
            List<CategoryResponse> list = null;
            try
            {
                list = _repo.Query().OrderBy(categoryEntity => categoryEntity.Name).Select(categoryEntity => new CategoryResponse()
                {
                    CreatedBy = categoryEntity.CreatedBy,
                    Description = categoryEntity.Description,
                    Guid = categoryEntity.Guid,
                    Id = categoryEntity.Id,
                    Name = categoryEntity.Name,
                    UpdatedBy = categoryEntity.UpdatedBy
                }).ToList();
                if (list.Count > 0) // if (list.Any())
                    return Success(list, list.Count + " record(s) found.");
                return Error(list, "No records found.");
            }
            catch (Exception exc) // exc.Message loglama için kullanılır
            {
                return Error(list, "An exception occurred during the operation!");
            }
        }

        public override Result<CategoryResponse> GetItem(int id)
        {
            CategoryResponse item = null;
            var entity = _repo.Query().SingleOrDefault(categoryEntity => categoryEntity.Id == id);
            if (entity is null)
                return Error(item, "Category not found!");
            item = new CategoryResponse()
            {
                CreatedBy = entity.CreatedBy,
                Description = entity.Description,
                Guid = entity.Guid,
                Id = entity.Id,
                Name = entity.Name,
                UpdatedBy = entity.UpdatedBy
            };
            return Success(item);
        }

        public override Result Create(CategoryRequest request)
        {
            // Way 1:
            //var entity = _repo.Query().SingleOrDefault(categoryEntity => categoryEntity.Name == request.Name);
            //if (entity is not null)
            //    return Error("Category with the same name exists!");
            // Way 2:
            if (_repo.Query().Any(categoryEntity => categoryEntity.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Category with the same name exists!");
            var entity = new Category
            {
                Description = request.Description?.Trim(),
                Name = request.Name?.Trim()
            };
            _repo.Create(entity);
            return Success("Category created successfully.", entity.Id);
        }

        public override Result<CategoryRequest> GetItemForEdit(int id)
        {
            CategoryRequest request = null;
            var entity = _repo.Query().SingleOrDefault(categoryEntity => categoryEntity.Id == id);
            if (entity is null)
                return Error(request, "Category not found!");
            request = new CategoryRequest
            {
                Description = entity.Description,
                Guid = entity.Guid,
                Id = entity.Id,
                Name = entity.Name
            };
            return Success(request);
        }

        // 2    A

        // 1    A
        // 2    B
        // 3    C

        public override Result Update(CategoryRequest request)
        {
            if (_repo.Query().Any(c => c.Id != request.Id && c.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Category with the same name exists!");
            var entity = _repo.Query().SingleOrDefault(c => c.Id == request.Id);
            if (entity is null)
                return Error("Category not found!");
            entity.Name = request.Name?.Trim();
            entity.Description = request.Description?.Trim();
            _repo.Update(entity);
            return Success("Category updated successfully.");
        }

        public override Result Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
