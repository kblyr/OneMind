using System.Security.Cryptography;

namespace OneMind.Security;

sealed class UserPasswordCryptoService : IUserPasswordCryptoService
{
    public byte[] ComputeHash(byte[] passwordData)
    {
        if (passwordData.Length == 0)
            return Array.Empty<byte>();

        using var algorithm = SHA512.Create();
        return algorithm.ComputeHash(passwordData);
    }
}