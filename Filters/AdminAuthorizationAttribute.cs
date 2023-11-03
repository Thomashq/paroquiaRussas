using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using paroquiaRussas.Services;
using paroquiaRussas.Utility.Resources;
using System.Security.Claims;

namespace paroquiaRussas.Filters
{
    public class AdminAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly TokenServices _tokenServices;

        public AdminAuthorizationAttribute(TokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string token = GetTokenFromRequest(context);

                ClaimsPrincipal claimsPrincipal = _tokenServices.GetPrincipalFromExpiredToken(token);

                if (claimsPrincipal == null)
                    throw new Exception(Exceptions.EXC28);
            }
            catch (Exception ex)
            {
                ReturnsExceptionUserWithoutAdminAuthorization(context);
            }
        }

        private static string GetTokenFromRequest(AuthorizationFilterContext context)
        {
            var cookies = context.HttpContext.Request.Cookies["token_auth"];

            if (cookies == null)
                throw new Exception(Exceptions.EXC27);

            string? token = cookies.ToString();

            return token;
        }

        private static void ReturnsExceptionUserWithoutAdminAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new RedirectToActionResult("Error", "Home", null);
        }
    }
}
