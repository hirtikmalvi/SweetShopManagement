namespace SweetShop.Api.Shared
{
    public class CustomResult<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public int StatusCode { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public List<string>? Errors { get; private set; }

        private CustomResult() { }

        public static CustomResult<T> Ok(
            T data,
            string message = "Operation completed successfully")
        {
            return new CustomResult<T>
            {
                Success = true,
                Data = data,
                StatusCode = 200,
                Message = message,
                Errors = null
            };
        }

        public static CustomResult<T> Fail(
            string message,
            int statusCode = 400,
            List<string> errors = null)
        {
            return new CustomResult<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }
    }
}
