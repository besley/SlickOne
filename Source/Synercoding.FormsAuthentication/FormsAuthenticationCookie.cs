using System;

namespace Synercoding.FormsAuthentication
{
    /// <summary>
    /// Form Authentication Cookie
    /// Copied from https://github.com/synercoder/FormsAuthentication
    /// MIT License
    /// </summary>
    public class FormsAuthenticationCookie
    {
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public bool IsPersistent { get; set; }
        public string UserName { get; set; }
        public string UserData { get; set; }
        public string CookiePath { get; set; }
    }
}
