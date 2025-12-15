using System.Security.Cryptography;

namespace WeblogSample.WebApp.Infra.PasswordHashing;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, 16, 10000, HashAlgorithmName.SHA256);
        var salt = deriveBytes.Salt;
        var key = deriveBytes.GetBytes(32);

        return Convert.ToBase64String(salt.Concat(key).ToArray());
    }

    public static bool Verify(string password, string hash)
    {
        var bytes = Convert.FromBase64String(hash);
        var salt = bytes.Take(16).ToArray();
        var key = bytes.Skip(16).ToArray();

        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        var newKey = deriveBytes.GetBytes(32);

        return newKey.SequenceEqual(key);
    }
}
