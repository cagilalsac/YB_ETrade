namespace CORE.Models
{
    public class Result
    {
        public bool IsSuccessful { get; }
        public string Message { get; }

        public Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
