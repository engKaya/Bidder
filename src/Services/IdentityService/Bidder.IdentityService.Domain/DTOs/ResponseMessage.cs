 

namespace Bidder.IdentityService.Domain.DTOs
{
    


    public class ResponseMessage<T> : ResponseMessageNoContent
    {
        public T Data { get; set; } = default;

        public static ResponseMessage<T> Success(T data, int statusCode = 200)
        {
            return new ResponseMessage<T>
            {
                Data = data,
                StatusCode = statusCode
            };
        }

        public static ResponseMessage<T> SuccessWithErrors(T data, List<string> errors, int statusCode = 200)
        {
            return new ResponseMessage<T>
            {
                Data = data,
                Errors = errors,
                StatusCode = statusCode
            };
        }

        public static ResponseMessage<T> Fail(string message, int statusCode = 500, List<string> errs = null)
        {
            return new ResponseMessage<T>
            {
                Message = message,
                StatusCode = statusCode,
                Errors = errs
            };
        }

        public static ResponseMessage<T> Fail(Exception exception, int statusCode = 500)
        {
            var IsDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            var err = IsDev ? $"{exception.Message} Stack: {exception.StackTrace}" : $"An Error Occured";


            return new ResponseMessage<T>
            {
                Message = "Failed",
                Errors = new List<string> { exception.Message },
                StatusCode = statusCode,
            };
        }

        public static ResponseMessage<T> Fail(string message, int statusCode = 500)
        {
            return new ResponseMessage<T>
            {
                Message = message,
                StatusCode = statusCode
            };
        }

        public void AddErrorr(Exception error)
        {
            Errors?.Add(error.Message);
        }

        // write function that clears the Errors prop of class
        public void ClearErrors()
        {
            Errors?.Clear();
        }
    }

    public class ResponseMessageNoContent
    {
        public List<string>? Errors { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }

        public static ResponseMessageNoContent Success(int statusCode = 200)
        {
            return new ResponseMessageNoContent
            {
                StatusCode = statusCode
            };
        }
        public static ResponseMessageNoContent Success(string message, int statusCode = 200)
        {
            return new ResponseMessageNoContent
            {
                StatusCode = statusCode,
                Message = message
            };
        }

        public static ResponseMessageNoContent SuccessWithErrors(List<string> errors, int statusCode = 200)
        {
            return new ResponseMessageNoContent
            {
                Errors = errors,
                StatusCode = statusCode
            };
        }

        public static ResponseMessageNoContent Fail(string message, int statusCode = 500, List<string> Exception = null)
        {
            return new ResponseMessageNoContent
            {
                Message = message,
                StatusCode = statusCode,
                Errors = Exception
            };
        }

        public static ResponseMessageNoContent Fail(Exception exception, int statusCode = 500)
        {
            return new ResponseMessageNoContent
            {
                Message = exception.Message,
                StatusCode = statusCode
            };
        }

        public static ResponseMessageNoContent Fail(string message, int statusCode = 500)
        {
            return new ResponseMessageNoContent
            {
                Message = message,
                StatusCode = statusCode
            };
        }

        public void AddErrorr(Exception error)
        {

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                Errors?.Add($"{error.Message} Stack: {error.StackTrace}");

            else Errors?.Add("An Error Occured");
        }

        // write function that clears the Errors prop of class
        public void ClearErrors()
        {
            Errors?.Clear();
        } 
    }
}
