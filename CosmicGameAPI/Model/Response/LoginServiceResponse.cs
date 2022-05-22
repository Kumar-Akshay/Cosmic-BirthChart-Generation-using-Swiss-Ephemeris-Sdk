using System.Net;

namespace CosmicGameAPI.Model.Response
{
    public class LoginServiceResponse
    {
        public string access_token { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public decimal isResetPassword { get; set; }
        public string displayMessage { get; set; }
        public string LoginInfoId { get; set; }
        public decimal IsAllowMultiLogin { get; set; }
        public decimal IsApproved { get; set; }
    }
    public class LoginResponse
    {
        public HttpStatusCode status { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public LoginServiceResponse result { get; set; }
    }
}
