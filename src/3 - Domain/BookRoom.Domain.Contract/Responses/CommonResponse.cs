namespace BookRoom.Domain.Contract.Responses
{
    public class CommonResponse<T>
    {
        public int Status { get; set; }

        public T Data { get; set; }

        public string Error { get; set; }

        public CommonResponse(int status, T data, string error)
        {
            Status = status;
            Data = data;
            Error = error;
        }

        public static CommonResponse<T> Ok(T data) => new CommonResponse<T>(200, data, string.Empty);

        public static CommonResponse<T> BadRequest(T data, string error) => new CommonResponse<T>(200, data, error);
    }
}
