using Synercoding.FormsAuthentication.Encryption;
using System;
using System.IO;

namespace Synercoding.FormsAuthentication
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Protect/unprotect logic based upon: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/FormsAuthenticationTicketSerializer.cs
    /// </remarks>
    public class FormsAuthenticationCryptor
    {
        private readonly FormsAuthenticationOptions _options;

        public FormsAuthenticationCryptor(FormsAuthenticationOptions options)
        {
            _options = options;
        }

        public string Protect(FormsAuthenticationCookie cookie)
        {
            if (cookie == null)
                throw new ArgumentNullException(nameof(cookie));

            var unprotectedData = ConvertToBytes(cookie);

            var cryptoProvider = AspNetCryptoServiceProvider.GetCryptoServiceProvider(_options);
            var cryptoService = cryptoProvider.GetCryptoService();
            byte[] protectedData = cryptoService.Protect(unprotectedData);

            return CryptoUtil.BinaryToHex(protectedData);
        }

        public FormsAuthenticationCookie Unprotect(string protectedText)
        {
            if (protectedText == null)
                throw new ArgumentNullException(nameof(protectedText));

            var bBlob = CryptoUtil.HexToBinary(protectedText);

            var cryptoProvider = AspNetCryptoServiceProvider.GetCryptoServiceProvider(_options);
            var cryptoService = cryptoProvider.GetCryptoService();
            byte[] unprotectedData = cryptoService.Unprotect(bBlob);

            return ConvertToAuthenticationTicket(unprotectedData);
        }

        private byte[] ConvertToBytes(FormsAuthenticationCookie data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (var ticketBlobStream = new MemoryStream())
            using (var ticketWriter = new SerializingBinaryWriter(ticketBlobStream))
            {
                ticketWriter.Write((byte)1);
                ticketWriter.Write((byte)1);
                ticketWriter.Write(data.IssuedUtc.Ticks);
                ticketWriter.Write((byte)0xfe);
                ticketWriter.Write(data.ExpiresUtc.Ticks);
                ticketWriter.Write(data.IsPersistent);
                ticketWriter.WriteBinaryString(data.UserName ?? "");
                ticketWriter.WriteBinaryString(data.UserData ?? "");
                ticketWriter.WriteBinaryString(data.CookiePath ?? "");
                ticketWriter.Write((byte)0xff);

                return ticketBlobStream.ToArray();
            }
        }

        private FormsAuthenticationCookie ConvertToAuthenticationTicket(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (var ticketBlobStream = new MemoryStream(data))
            using (SerializingBinaryReader ticketReader = new SerializingBinaryReader(ticketBlobStream))
            {
                byte serializedFormatVersion = ticketReader.ReadByte();
                if (serializedFormatVersion != 0x01)
                    throw new ArgumentException("The data is not in the correct format, first byte must be 0x01.", nameof(data));

                int ticketVersion = ticketReader.ReadByte();

                DateTime ticketIssueDateUtc = new DateTime(ticketReader.ReadInt64(), DateTimeKind.Utc);

                byte spacer = ticketReader.ReadByte();
                if (spacer != 0xFE)
                    throw new ArgumentException("The data is not in the correct format, tenth byte must be 0xFE.", nameof(data));

                DateTime ticketExpirationDateUtc = new DateTime(ticketReader.ReadInt64(), DateTimeKind.Utc);
                bool ticketIsPersistent = ticketReader.ReadByte() == 1;

                string ticketName = ticketReader.ReadBinaryString();
                string ticketUserData = ticketReader.ReadBinaryString();
                string ticketCookiePath = ticketReader.ReadBinaryString();
                byte footer = ticketReader.ReadByte();
                if (footer != 0xFF)
                    throw new ArgumentException("The data is not in the correct format, footer byte must be 0xFF.", nameof(data));
                
                //create ticket
                return new FormsAuthenticationCookie()
                {
                    UserName = ticketName,
                    UserData = ticketUserData,
                    CookiePath = ticketCookiePath,
                    IsPersistent = ticketIsPersistent,
                    IssuedUtc = ticketIssueDateUtc,
                    ExpiresUtc = ticketExpirationDateUtc
                };
            }
        }
    }
}
