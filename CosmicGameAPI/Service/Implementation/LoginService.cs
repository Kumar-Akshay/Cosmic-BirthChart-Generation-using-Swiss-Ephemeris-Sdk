using CosmicGameAPI.Entities;
using CosmicGameAPI.Model.Request;
using CosmicGameAPI.Model.Response;
using CosmicGameAPI.Service.Interface;
using CosmicGameAPI.Utility;
using CosmicGameAPI.Utility.Constant;
using CosmicGameAPI.Utility.EmailSender;
using CosmicGameAPI.Utility.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CosmicGameAPI.Service.Implementation
{
    public class LoginService : ILoginService, IDisposable
    {
        private CosmicDbContext _cosmicDbContext { get; set; }
        private readonly ICommonService _commonService;
        private readonly IConfiguration _configuration;
        public LoginService(CosmicDbContext cosmicDbContext, ICommonService commonService ,IConfiguration configuration)
        {
            _cosmicDbContext = cosmicDbContext;
            _commonService = commonService;
            _configuration = configuration;
        }
        public LoginService()
        {

        }
        public async Task<LoginServiceResponse> AuthenticateUser(LoginRequest userDetails)
        {
            LoginServiceResponse objLoginResponse = new();
            objLoginResponse.userId = -1;
            objLoginResponse.userName = "";
            objLoginResponse.isResetPassword = 0;
            objLoginResponse.displayMessage = "";
            objLoginResponse.LoginInfoId = "";
            try
            {
                userDetails.Password = PasswordHasher.Encrypt(userDetails.Password, Constants.Privatekey);
                if(string.IsNullOrEmpty(userDetails.UserName) && string.IsNullOrEmpty(userDetails.Password))
                {
                    objLoginResponse.displayMessage = "Username and Password can't be empty";
                    return objLoginResponse;
                }
                var getUsers = await _cosmicDbContext.Users.AsNoTracking().Where(p => p.UserName == userDetails.UserName && p.Password == userDetails.Password).ToListAsync();
                if (getUsers.Count() == 0)
                {
                    objLoginResponse.displayMessage = "Invalid username or password";
                    return objLoginResponse;
                }
                else if (getUsers.Count() > 1)
                {
                    objLoginResponse.displayMessage = "Multiple User found";
                    return objLoginResponse;
                }
                var user = getUsers.FirstOrDefault();
                if (user != null)
                {
                    if (user.IsApproved == 0)
                    {
                        string strToken = PasswordHasher.Encrypt(user.UserId.ToString() + ";" + EmailHelper.GetDateTime(), Constants.Privatekey);
                        strToken = System.Net.WebUtility.UrlEncode(strToken);
                        string Strbody = "";
                        string strSubject = "Email Verification - Cosmic Game";
                        var path = Directory.GetCurrentDirectory();
                        Strbody = EmailHelper.GetTemplateBody(path + "\\Utility\\EmailTemplates\\RegistrationVerification.html");
                        Strbody = Strbody.Replace("`name`", user.Email);
                        Strbody = Strbody.Replace("`mobileno`", user.Mobile);
                        Strbody = Strbody.Replace("`email`", user.Email);
                        Strbody = Strbody.Replace("`Link`", userDetails.URL + "verify?t=" + strToken);
                        EmailHelper.SendEmail(user.Email, "", strSubject, Strbody);

                        objLoginResponse.displayMessage = "Your account is not Approved, please verify Mail ID.";
                        objLoginResponse.userName = user.UserName;
                        objLoginResponse.IsApproved = user.IsApproved;
                        objLoginResponse.Email = user.Email;
                        objLoginResponse.IsAllowMultiLogin = user.IsAllowMultipelLogin;
                        objLoginResponse.userId = user.UserId;
                        objLoginResponse.Mobile = user.Mobile;
                        return objLoginResponse;
                    }

                    objLoginResponse.userId = user.UserId;
                    objLoginResponse.userName = user.UserName;
                    objLoginResponse.Mobile= user.Mobile;
                    objLoginResponse.Email= user.Email;
                    objLoginResponse.displayMessage = "User successfully login.";
                    return objLoginResponse;
                }
            }
            catch (Exception ex)
            {
                await _commonService.SetErorr("LoginService -> AuthenticateUser : " + ex.Message + "::" + ex.StackTrace);
                throw;
            }

            throw new NotImplementedException();
        }

        public async Task<(string, string)> CreateJwtToken(int UserID, string UserName, string Ip, string Browser)
        {

            try
            {
                var loginInfo = new UserLoginInfo
                {
                    UserId = UserID,
                    IpAddress = Ip,
                    IsLogin = 1,
                    Browser = Browser,
                    LastLoginDate = DateTime.Now,
                    Token = Guid.NewGuid().ToString(),
                    DateTime = DateTime.UtcNow.AddHours(5.5),
                };
                _cosmicDbContext.UserLoginInfos.Add(loginInfo);
                await _cosmicDbContext.SaveChangesAsync();


                string strHash = loginInfo.Token;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(GlobalVars.JwtKey);
                double expMinutesConfig = Convert.ToDouble(_configuration["BearerTokens:AccessTokenExpirationMinutes"].ToString());
                string vissuer = _configuration["AuthToken:Issuer"].ToString();
                string vaudience = _configuration["AuthToken:Audience"].ToString();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier,PasswordHasher.Encrypt(UserID.ToString(),Constants.Privatekey)),
                    new Claim(ClaimTypes.Name,UserName),
                    new Claim(ClaimTypes.Hash,strHash),
                }),
                    Expires = DateTime.UtcNow.AddMinutes(expMinutesConfig),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = vissuer,
                    Audience = vaudience,
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string strUserLoginInfoID = PasswordHasher.Encrypt(Convert.ToString(loginInfo.Id), Constants.Privatekey);

                return (tokenHandler.WriteToken(token), strUserLoginInfoID);
            }
            catch (Exception ex)
            {
                await _commonService.SetErorr("LoginService -> CreateJwtToken : " + ex.Message + "::" + ex.StackTrace);
                throw;
            }
        }

        public async Task<ServiceResponse> RegisterUser(RegisterRequest userDetails)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                int isExist = 0;
                userDetails.Password = PasswordHasher.Encrypt(userDetails.Password, Constants.Privatekey);
                var userId = await _cosmicDbContext.Users.AsNoTracking().Where(p => p.Email == userDetails.Email).Select(p=>p.UserId).FirstOrDefaultAsync();
                if(userId == 0)
                {
                    User userRegister = new()
                    {
                        UserName = userDetails.Email,
                        Email = userDetails.Email,
                        Mobile = userDetails.Contact,
                        Password = userDetails.Password,
                        IsActive = 1,
                        IsAllowMultipelLogin = 1,
                        IsApproved = 0,
                        IsDelete = 0,
                        CreatedDate = DateTime.Now
                    };
                    _cosmicDbContext.Users.Add(userRegister);
                    await _cosmicDbContext.SaveChangesAsync();
                    userId = userRegister.UserId;
                    isExist = 0;
                }
                else
                {
                    isExist = 1;
                }
                if (isExist == 1 || userId > 0)
                {
                    if (isExist == 1)
                    {
                        objResponse.success = false;
                        objResponse.message = "Email already exist.<br>please try with different one!";
                        objResponse.status = HttpStatusCode.Conflict;
                    }
                    else
                    {
                        string strToken = PasswordHasher.Encrypt(userId.ToString() + ";" + EmailHelper.GetDateTime(), Constants.Privatekey);
                        strToken = System.Net.WebUtility.UrlEncode(strToken);
                        string Strbody = "";
                        string strSubject = "Email Verification - Cosmic Game";
                        var path = Directory.GetCurrentDirectory();
                        Strbody = EmailHelper.GetTemplateBody(path + "\\Utility\\EmailTemplates\\RegistrationVerification.html");
                        Strbody = Strbody.Replace("`name`", userDetails.Email);
                        Strbody = Strbody.Replace("`mobileno`", userDetails.Contact);
                        Strbody = Strbody.Replace("`email`", userDetails.Email);
                        Strbody = Strbody.Replace("`Link`", userDetails.URL + "verify?t=" + strToken);
                        EmailHelper.SendEmail(userDetails.Email, "", strSubject, Strbody);

                        objResponse.success = true;
                        objResponse.result = userId;
                        objResponse.message = "Register successfully.<br>Please check and verify email";
                        objResponse.status = HttpStatusCode.OK;
                    }
                }
                else
                {
                    objResponse.success = false;
                    objResponse.message = "Registration Failed.<br>Please try after some time!";
                    objResponse.status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.message = "something went wrong." + ex.Message;
                await _commonService.SetErorr("LoginService -> RegisterUser : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> ForgotPassword(string strEmail)
        {
            try
            {
                ServiceResponse objResponse = new ServiceResponse();
                var objUser = await _cosmicDbContext.Users.AsNoTracking().Where(p => p.Email == strEmail).FirstOrDefaultAsync();
                if (objUser != null && objUser.Email != "")
                {
                    string Strbody = "";
                    string strSubject = "Reset Password - Cosmic Game";

                    var path = Directory.GetCurrentDirectory();
                    Strbody = EmailHelper.GetTemplateBody(path + "\\Utility\\EmailTemplates\\Forgotpassword.html");
                    Strbody = Strbody.Replace("`name`", objUser.UserName);
                    //Strbody = Strbody.Replace("`Link`", "");
                    //Strbody = Strbody.Replace("`Message`", "Click Below link to reset your password.");
                    Strbody = Strbody.Replace("`Message`", "Use below login credential to access cosmic game.");
                    Strbody = Strbody.Replace("`Email`", objUser.Email);
                    Strbody = Strbody.Replace("`Password`", PasswordHasher.Decrypt(objUser.Password, Constants.Privatekey));
                    EmailHelper.SendEmail(objUser.Email, "", strSubject, Strbody);

                    objResponse.success = true;
                    objResponse.message = "Reset password successfully.<br>Please check Email.";
                    objResponse.status = HttpStatusCode.OK;
                }
                else
                {
                    objResponse.success = false;
                    objResponse.message = "Email does not exist.<br>please try with registered Email!";
                    objResponse.status = HttpStatusCode.Conflict;
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                await _commonService.SetErorr("LoginService -> ForgotPassword : " + ex.Message + "::" + ex.StackTrace);
                throw;
            }
        }

        public async Task<ServiceResponse> Userverify(string token)
        {
            ServiceResponse objResponse = new ServiceResponse();
            string strId = PasswordHasher.Decrypt(token, Constants.Privatekey);
            string[] arr = strId.Split(';');
            if (Convert.ToDateTime(arr[1].ToString()).AddDays(1) > Convert.ToDateTime(EmailHelper.GetDateTime()))
            {
                try
                {
                    string userId = arr[0];
                    var Response = await _cosmicDbContext.Users.AsNoTracking().Where(p=>p.UserId == Convert.ToInt32(userId)).FirstOrDefaultAsync();
                    objResponse.success = Response != null ? true : false;
                    if (objResponse.success)
                    {
                        objResponse.message = "Email Verification success.!";
                    }
                    else
                    {
                        objResponse.message = "Verification failed due to invalid or expired link!";
                    }
                }
                catch (Exception ex)
                {
                    objResponse.success = false;
                    objResponse.message = "something went wrong." + ex.Message;
                    await _commonService.SetErorr("LoginService -> Userverify : " + ex.Message + "::" + ex.StackTrace);
                }
            }
            else
            {
                objResponse.success = false;
                objResponse.message = "Verification link is invalid or expired!";
            }

            return objResponse;
        }

        public void Dispose()
        {
            if (_cosmicDbContext != null)
            {
                _cosmicDbContext.Dispose();
            }
        }
    }
}
