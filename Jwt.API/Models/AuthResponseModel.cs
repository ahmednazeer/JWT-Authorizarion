namespace Jwt.API.Models
{
    public class AuthResponseModel
    {
        public string  Toekn { get; set; }
        public bool  Success { get; set; }
        public List<string>  Errors { get; set; }
    }
}
