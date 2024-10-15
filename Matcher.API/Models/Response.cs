namespace Matcher.API.Models
{
    public class Response
    {
        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }

    public class Response<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
