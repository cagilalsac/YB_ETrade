using CORE.Models;

namespace CORE.Services
{
    public interface IService<TRequest, TResponse> : IDisposable 
        where TRequest : Request, new() where TResponse : Response, new()
    {
        // Result data'sı dönen GetList ve GetItem methodları da yazılabilir
        public List<TResponse> GetList();
        public TResponse GetItem(int id);

        public Result Create(TRequest request);
        public Result Update(TRequest request);
        public Result Delete(int id);
    }
}
