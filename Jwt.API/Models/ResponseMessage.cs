using System.Net;

namespace Jwt.API.Models
{
    public class CoreResponseModel<T>
    {
        

        public CoreResponseModel(T dataObject, HttpStatusCode statusCode)
        {
            this.Data = dataObject;
            this.StatusCode = statusCode;
        }

        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        


    }
}
