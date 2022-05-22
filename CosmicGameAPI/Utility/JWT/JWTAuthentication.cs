using CosmicGameAPI.Utility.Constant;
using CosmicGameAPI.Utility.Hashing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CosmicGameAPI.Utility.JWT
{
    public class JWTAuthentication : IAsyncActionFilter
    {
        public int UserId { get; private set; }
        public string strHash { get; private set; }
        public int LoginType { get; private set; }
        public string path { get; private set; }

        public JWTAuthentication(IHttpContextAccessor httpContextAccessor)
        {
            var claimsIdentity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity.Claims.Count() > 0)
            {
                var nameIdentifier = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
                var userLoginInfoToken = claimsIdentity?.FindFirst(ClaimTypes.Hash);
                strHash = userLoginInfoToken?.Value;
                var userId = PasswordHasher.Decrypt(nameIdentifier?.Value, Constants.Privatekey);
                UserId = string.IsNullOrWhiteSpace(userId) ? 0 : int.Parse(userId);
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity.Claims.Count() > 0)
            {
                //#region Check user login info token is valid
                //bool isValid = await _ICommonService.isValidUser(UserId, LoginType, strHash);
                //if (!isValid)
                //{
                //    context.Result = new UnauthorizedResult();
                //    return;
                //}
                //#endregion

                await next();
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
