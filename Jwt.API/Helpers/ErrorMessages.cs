namespace Jwt.API.Helpers
{
    public class ErrorMessages
    {
        public static string UserAlreadyExists { get; } = "User Already Exists";
        public static string FailedToAddNewUser { get; } = "Failed To Add New User";
        public static string InvalidPayload { get; } = "Invalid Payload!";
        public static string InvalidSigninCredentials { get; } = "Invalid Signin Credentials!";
    }
}
