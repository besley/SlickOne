using Microsoft.AspNetCore.Authentication;
using System;

namespace Synercoding.FormsAuthentication
{
    /// <summary>
    /// Form Authentication Data Format
    /// Copied from https://github.com/synercoder/FormsAuthentication
    /// MIT License
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class FormsAuthenticationDataFormat<TData> : ISecureDataFormat<TData>
        where TData : AuthenticationTicket
    {
        private readonly Func<FormsAuthenticationCookie, TData> _convertFrom;
        private readonly Func<TData, FormsAuthenticationCookie> _convertTo;
        private readonly FormsAuthenticationCryptor _cryptor;

        public FormsAuthenticationDataFormat(FormsAuthenticationOptions options, 
            Func<FormsAuthenticationCookie, TData> from, 
            Func<TData, FormsAuthenticationCookie> to)
        {
            _cryptor = new FormsAuthenticationCryptor(options);
            _convertFrom = from;
            _convertTo = to;
        }

        public string Protect(TData data)
        {
            return Protect(data, null);
        }

        public string Protect(TData data, string purpose)
        {
            var cookie = _convertTo(data);
            return _cryptor.Protect(cookie);
        }

        public TData Unprotect(string protectedText)
        {
            return Unprotect(protectedText, null);
        }

        public TData Unprotect(string protectedText, string purpose)
        {
            try
            {
                var cookie = _cryptor.Unprotect(protectedText);
                return _convertFrom(cookie);
            }
            catch
            {
                return default(TData);
            }
        }
    }
}
