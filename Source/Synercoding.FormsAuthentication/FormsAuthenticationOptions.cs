namespace Synercoding.FormsAuthentication
{
    public class FormsAuthenticationOptions
    {
        public EncryptionMethod EncryptionMethod { get; set; } = EncryptionMethod.AES;
        public ValidationMethod ValidationMethod { get; set; } = ValidationMethod.HMACSHA256;

        public string DecryptionKey { get; set; }
        public string ValidationKey { get; set; }
    }
}