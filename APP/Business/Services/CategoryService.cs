﻿using APP.Business.Models;
using APP.DataAccess;
using CORE.Models;
using CORE.Repositoires;
using CORE.Services;

namespace APP.Business.Services
{
    public class CategoryService : Service<Category, CategoryRequest, CategoryResponse>
    {
        public CategoryService(Repo<Category> repo) : base(repo)
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
            throw new NotImplementedException();
        }

        public override Result Create(CategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public override Result Update(CategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public override Result Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
