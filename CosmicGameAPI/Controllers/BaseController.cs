using CosmicGameAPI.Utility.Constant;
using CosmicGameAPI.Utility.Hashing;
using CosmicGameAPI.Utility.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CosmicGameAPI.Controllers
{
    [ServiceFilter(typeof(JWTAuthentication))]
    public class BaseController : Controller , IAsyncActionFilter
    {
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public int RoleId { get; private set; }
        public string strHash { get; private set; }

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {

            var claimsIdentity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity.Claims.Count() > 0)
            {
                var nameIdentifier = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
                var userLoginInfoToken = claimsIdentity?.FindFirst(ClaimTypes.Hash);
                strHash = userLoginInfoToken?.Value;
                var userId = PasswordHasher.Decrypt(nameIdentifier?.Value, Constants.Privatekey);
                UserId = string.IsNullOrWhiteSpace(userId) ? 0 : int.Parse(userId);
                var userNameIdentifier = claimsIdentity?.FindFirst(ClaimTypes.Name);
                Username = userNameIdentifier?.Value;

            }
        }


    }
}
