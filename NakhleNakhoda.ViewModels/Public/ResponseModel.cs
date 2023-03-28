namespace NakhleNakhoda.ViewModels.Public
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
        public long StatusCode { get; set; } = 200;
        public T? Content { get; set; }
    }
}
