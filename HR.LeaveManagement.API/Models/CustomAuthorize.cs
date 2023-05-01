using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using HR.LeaveManagement.Application.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.API.Models;

[AttributeUsage(AttributeTargets.Class)]
public class CustomAuthorize: Attribute, IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        IServiceProvider services = filterContext.HttpContext.RequestServices;

        JwtSettings settings = services.GetService<JwtSettings>();
        
        if (filterContext != null)  
            {  
                Microsoft.Extensions.Primitives.StringValues authTokens;  
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authTokens);  
  
                var _token = authTokens.FirstOrDefault();  
  
                if (_token != null)  
                {    
                    string authToken = _token;  
                    if (authToken != null)  
                    {  
                        if (ValidateJwtToken(authToken, "SECRET_JWT_KEY_HERE"))  
                        {  
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);  
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");  
  
                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");  
      
                            return;  
                        }  
                        else  
                        {  
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);  
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");              
  
                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;  
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";  
                            filterContext.Result = new JsonResult("NotAuthorized")  
                            {  
                                Value = new  
                                {  
                                    Status = "Error",  
                                    Message = "Invalid Token"  
                                },  
                            };  
                        }  
  
                    }  
  
                }  
                else  
                {  
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;  
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";  
                    filterContext.Result = new JsonResult("Please Provide authToken")  
                    {  
                        Value = new  
                        {  
                            Status = "Error",  
                            Message = "Please Provide authToken"  
                        },  
                    };  
                }  
            }  
        }  
  
    public bool ValidateJwtToken(string token, string secretKey)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return true;
        }
        catch (SecurityTokenException)
        {
            // Invalid token
            return false;
        }
        catch (Exception)
        {
            // Other exception occurred
            return false;
        }
    }
    
}