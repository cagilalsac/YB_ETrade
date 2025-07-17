namespace CORE.Models
{
    public class Result
    {
        public bool IsSuccessful { get; }
        public string Message { get; }
        public int Id { get; }

        public Result(bool isSuccessful, string message = "", int id = 0)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Id = id;
        }
    }

    public class Result<TData> : Result where TData : class, new()
    {
        public TData Data { get; }

        public Result(bool isSuccessful, TData data, string message = "") : base(isSuccessful, message)
        {
            Data = data;
        }
    }
}
