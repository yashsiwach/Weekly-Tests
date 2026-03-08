using System;
using System.Security.Cryptography;
using NUnit.Framework;

public class PasswordHasher
{
    /// <summary>
    /// Generates a random salt per password
    /// Uses PBKDF2 with many iterations for strong hashing
    /// Stores salt + hash together (no plain text)
    /// </summary>
    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) throw new ArgumentException();

        byte[] salt = RandomNumberGenerator.GetBytes(16);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Extracts salt from stored hash
    /// Recomputes hash using same algorithm
    /// Compares hashes securely
    /// </summary>
    public bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        var newHash = pbkdf2.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(hash, newHash);
    }
}

[TestFixture]
public class PasswordHasherTests
{
    [Test]
    public void HashAndVerify_Works()
    {
        var h = new PasswordHasher();
        var hash = h.HashPassword("secret");

        Assert.IsTrue(h.VerifyPassword("secret", hash));
        Assert.IsFalse(h.VerifyPassword("wrong", hash));
    }
}