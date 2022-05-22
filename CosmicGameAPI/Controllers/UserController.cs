using CosmicGameAPI.Model.Request;
using CosmicGameAPI.Model.Response;
using CosmicGameAPI.Service.Helper;
using CosmicGameAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CosmicGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private ILoginService _ILoginService;

        public UserController(ILoginService LoginService)
        {
            _ILoginService = LoginService;
        }

        [AllowAnonymous]
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginRequest userRequest)
        {
            LoginResponse objResponse = new LoginResponse();
            try
            {
                string strReqIP = "";
                strReqIP = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                if (Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    strReqIP = Request.Headers["X-Forwarded-For"];
                }
                if (Request.Headers.ContainsKey("CF-CONNECTING-IP"))
                {
                    strReqIP = Request.Headers["CF-CONNECTING-IP"].ToString();
                }
                string IpInfo = strReqIP;
                string BroserInfo = Request.Headers["User-Agent"].ToString();
                var result = await _ILoginService.AuthenticateUser(userRequest);
                if (result.userId > 0)
                {
                    var Response = await _ILoginService.CreateJwtToken(Convert.ToInt32(result.userId), result.userName, IpInfo, BroserInfo);
                    LoginServiceResponse objLoginResponse = new()
                    {
                        access_token = Response.Item1,
                        userId = result.userId,
                        LoginInfoId = Response.Item2,
                        userName = result.userName,
                        Email = result.Email,
                        Mobile = result.Mobile,
                        isResetPassword = result.isResetPassword,
                        displayMessage = result.displayMessage,
                    };
                    objResponse.result = objLoginResponse;
                    objResponse.success = true;
                    objResponse.message = result.displayMessage;
                    objResponse.status = HttpStatusCode.OK;
                    return Ok(objResponse);
                }
                else
                {
                    objResponse.message = result.displayMessage;
                    objResponse.success = false;
                    objResponse.status = HttpStatusCode.Unauthorized;
                    return Ok(objResponse);
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.Message;
            }
            objResponse.status = HttpStatusCode.Unauthorized;
            objResponse.result = null;
            objResponse.success = false;
            return Ok(objResponse);
        }

        [AllowAnonymous]
        [HttpPost("UserRegister")]
        public async Task<IActionResult> UserRegister(RegisterRequest userRequest)
        {
            var result = await _ILoginService.RegisterUser(userRequest);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("Userverify")]
        public async Task<IActionResult> Userverify(string token)
        {
            string strToken = System.Net.WebUtility.UrlDecode(token);
            var result = await _ILoginService.Userverify(token);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(RegisterRequest userRequest)
        {
            var result = await _ILoginService.ForgotPassword(userRequest.Email);
            return Ok(result);
        }

    }
}
