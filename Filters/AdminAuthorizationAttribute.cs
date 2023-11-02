using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using paroquiaRussas.Services;
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
                    throw new Exception();
            }
            catch (Exception ex)
            {
                ReturnsExceptionUserWithoutAdminAuthorization(context);
            }
        }

        private static string GetTokenFromRequest(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Cookies["token_auth"].ToString();

            if (string.IsNullOrEmpty(token))
                throw new Exception();

            return token;
        }

        private static void ReturnsExceptionUserWithoutAdminAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new RedirectToActionResult("Error", "Home", null);
        }
    }
}
