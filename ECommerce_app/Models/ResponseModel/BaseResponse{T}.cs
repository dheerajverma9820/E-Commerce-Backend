namespace ECommerce_app.Models.ResponseModel
{
    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
        public BaseResponse(int status, string? message, T data): base(status, message)
        {
            this.Data = data;
        }

        public static BaseResponse<T> SuccessResponse(int status, string? message, T? data)
        {
            return new BaseResponse<T>(status, message, data);
        }

        public static BaseResponse<T> ErrorResponse(int status, string? errorMessage)
        {
            return new BaseResponse<T>(status, errorMessage, default);
        }
    }
}
