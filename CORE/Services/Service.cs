using CORE.Entities;
using CORE.Models;
using CORE.Repositoires;
using System.Globalization;

namespace CORE.Services
{
    public abstract class Service<TEntity, TRequest, TResponse> : IDisposable 
        where TEntity : Entity, new() where TRequest : Request, new() where TResponse : Response, new()
    {
        // Encapsulation
        private CultureInfo _cultureInfo;
        protected CultureInfo CultureInfo // new CultureInfo("tr-TR")
        {
            get
            {
                return _cultureInfo;
            }
            set
            {
                _cultureInfo = value;
                Thread.CurrentThread.CurrentCulture = _cultureInfo;
                Thread.CurrentThread.CurrentUICulture = _cultureInfo;
            }
        }

        protected readonly RepoBase<TEntity> _repo;

        protected Service(RepoBase<TEntity> repo)
        {
            _repo = repo;
            CultureInfo = new CultureInfo("en-US");
        }

        public abstract Result<List<TResponse>> GetList();
        public abstract Result<TResponse> GetItem(int id);
        public abstract Result<TRequest> GetItemForEdit(int id);
        public abstract Result Create(TRequest request);
        public abstract Result Update(TRequest request);
        public abstract Result Delete(int id);

        protected Result Success(string message = "", int id = 0)
        {
            return new Result(true, message, id);
        }

        protected Result Error(string message = "", int id = 0)
        {
            return new Result(false, message, id);
        }

        protected Result<TData> Success<TData>(TData data, string message = "") where TData : class, new()
        {
            return new Result<TData>(true, data, message);
        }

        protected Result<TData> Error<TData>(TData data, string message = "") where TData : class, new()
        {
            return new Result<TData>(false, data, message);
        }

        public void Dispose()
        {
            _repo.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
