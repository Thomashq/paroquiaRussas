using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using paroquiaRussas.Services;
using System.Security.Claims;
using static paroquiaRussas.Utility.Enum;
using Enum = paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Filters
{
    public class AdminMethodsAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly TokenServices _tokenServices;

        public AdminMethodsAuthorizationAttribute(TokenServices tokenServices)
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

                ValidateClaimAdminRole(claimsPrincipal);
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

        private static void ValidateClaimAdminRole(ClaimsPrincipal claimsPrincipal)
        {
            Claim roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role);

            if (roleClaim == null || roleClaim.Value != Enum.GetEnumDescription(Role.Admin))
                throw new Exception();
        }
    }
}
