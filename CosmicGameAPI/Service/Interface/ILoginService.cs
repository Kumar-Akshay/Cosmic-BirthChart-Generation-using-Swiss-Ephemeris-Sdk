using CosmicGameAPI.Model.Request;
using CosmicGameAPI.Model.Response;

namespace CosmicGameAPI.Service.Interface
{
    public interface ILoginService
    {
        Task<LoginServiceResponse> AuthenticateUser(LoginRequest userDetails);
        Task<(string, string)> CreateJwtToken(int UserID, string UserName, string Ip, string Browser);
        Task<ServiceResponse> RegisterUser(RegisterRequest userDetails);
        Task<ServiceResponse> Userverify(string userId);
        Task<ServiceResponse> ForgotPassword(string strEmail);
    }
}
