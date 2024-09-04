namespace ECommerce_app.Models.ResponseModel
{
    public class BaseResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public BaseResponse(int status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
        public static BaseResponse SuccessResponse(int status, string? message)
        {
            return new BaseResponse(status, message);
        }
        public static BaseResponse ErrorResponse(int status, string? errorMessage)
        {
            return new BaseResponse(status, errorMessage);
        }

    }
}

