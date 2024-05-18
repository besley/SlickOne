using Microsoft.AspNetCore.Authentication;
using Synercoding.FormsAuthentication;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SlickOne.WebUtility.Authentication
{
    /// <summary>
    /// Form Authentication Helper
    /// Copied from https://github.com/synercoder/FormsAuthentication
    /// MIT License
    /// </summary>
    public static class FormsAuthHelper
    {
        #region basic method
        public static Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context, string baseUrl)
        {
            var request = context.Request;
            var absoluteUri = string.Concat(
                request.Scheme,
                "://",
                request.Host.ToUriComponent(),
                request.PathBase.ToUriComponent(),
                request.Path.ToUriComponent(),
                request.QueryString.ToUriComponent());

            if (string.IsNullOrWhiteSpace(baseUrl))
                baseUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

            context.RedirectUri = baseUrl.TrimEnd('/') + context.Options.AccessDeniedPath + "?" + context.Options.ReturnUrlParameter + "=" + absoluteUri;

            if (IsAjaxRequest(context.Request))
            {
                context.Response.Headers["Location"] = context.RedirectUri;
                context.Response.StatusCode = 403;
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }

            return Task.CompletedTask;
        }

        public static Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context, string baseUrl)
        {
            var request = context.Request;
            var absoluteUri = string.Concat(
                request.Scheme,
                "://",
                request.Host.ToUriComponent(),
                request.PathBase.ToUriComponent(),
                request.Path.ToUriComponent(),
                request.QueryString.ToUriComponent());

            if (string.IsNullOrWhiteSpace(baseUrl))
                baseUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

            context.RedirectUri = baseUrl + context.Options.LoginPath + "?" + context.Options.ReturnUrlParameter + "=" + absoluteUri;

            if (IsAjaxRequest(context.Request))
            {
                context.Response.Headers["Location"] = context.RedirectUri;
                context.Response.StatusCode = 401;
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }

            return Task.CompletedTask;
        }

        public static AuthenticationTicket ConvertCookieToTicket(FormsAuthenticationCookie cookie)
        {
            var authenticationProperties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = cookie.ExpiresUtc,
                IsPersistent = cookie.IsPersistent,
                IssuedUtc = cookie.IssuedUtc
            };

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, cookie.UserName));

            // Connect to database to get the roles and add them to the identity.
            // Or if the roles are stored in the cookie read the cookie.UserData and parse that into role claims

            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationTicket(principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static FormsAuthenticationCookie ConvertTicketToCookie(AuthenticationTicket ticket)
        {
            var claimsIdentity = (ClaimsIdentity)ticket.Principal.Identity;

            var cookie = new FormsAuthenticationCookie()
            {
                CookiePath = "",
                ExpiresUtc = (ticket.Properties.ExpiresUtc ?? DateTime.Now).DateTime.ToUniversalTime(),
                IsPersistent = ticket.Properties.IsPersistent,
                IssuedUtc = (ticket.Properties.IssuedUtc ?? DateTime.Now).DateTime.ToUniversalTime(),
                UserName = ticket.Principal.Identity.Name,
                UserData = null
            };

            return cookie;
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                   string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }
        #endregion

        #region extension method
        /// <summary>
        /// protect cookie with encrypt method
        /// </summary>
        /// <param name="cookie">Cookie</param>
        /// <param name="options">Options</param>
        /// <returns>encrypted text</returns>
        public static string Protect(FormsAuthenticationCookie cookie, 
            FormsAuthenticationOptions options)
        {
            var cryptor = new FormsAuthenticationCryptor(options);
            string text = cryptor.Protect(cookie);
            return text;
        }

        /// <summary>
        /// unprotect cookie with encrypt method
        /// </summary>
        /// <param name="text">encrypted text</param>
        /// <param name="options">Options</param>
        /// <returns>Cookie</returns>
        public static FormsAuthenticationCookie UnProtect(string text, FormsAuthenticationOptions options)
        {
            var cryptor = new FormsAuthenticationCryptor(options);
            var cookie = cryptor.Unprotect(text);
            return cookie;
        }
        #endregion
    }
}
