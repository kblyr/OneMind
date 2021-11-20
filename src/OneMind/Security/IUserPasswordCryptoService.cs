namespace OneMind.Security;

public interface IUserPasswordCryptoService
{
    byte[] ComputeHash(byte[] passwordData);
}