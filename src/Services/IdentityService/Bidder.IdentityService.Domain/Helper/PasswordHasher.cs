﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Bidder.IdentityService.Domain.Helper
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; //128 / 8, length in bytes
        private const int KeySize = 32; //256 / 8, length in bytes
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char SaltDelimeter = ';';
        public static string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
            return string.Join(SaltDelimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }
        public static bool VerifyHashedPassword(string passwordHash, string password)
        {
            var pwdElements = passwordHash.Split(SaltDelimeter);
            var salt = Convert.FromBase64String(pwdElements[0]);
            var hash = Convert.FromBase64String(pwdElements[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }


        //public static string HashPassword(string password)
        //{
        //    var prf = KeyDerivationPrf.HMACSHA256;
        //    var rng = RandomNumberGenerator.Create();
        //    const int iterCount = 10000;
        //    const int saltSize = 128 / 8;
        //    const int numBytesRequested = 256 / 8;

        //    // Produce a version 3 (see comment above) text hash.
        //    var salt = new byte[saltSize];
        //    rng.GetBytes(salt);
        //    var subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

        //    var outputBytes = new byte[13 + salt.Length + subkey.Length];
        //    outputBytes[0] = 0x01; // format marker
        //    WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        //    WriteNetworkByteOrder(outputBytes, 5, iterCount);
        //    WriteNetworkByteOrder(outputBytes, 9, saltSize);
        //    Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        //    Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
        //    return Convert.ToBase64String(outputBytes);
        //}

        //public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        //{
        //    var decodedHashedPassword = Convert.FromBase64String(hashedPassword);
        //    if (decodedHashedPassword[0] != 0x01)
        //        return false;

        //    var prf = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
        //    var iterCount = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
        //    var saltLength = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

        //    if (saltLength < 128 / 8)
        //    {
        //        return false;
        //    }
        //    var salt = new byte[saltLength];
        //    Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

        //    var subkeyLength = decodedHashedPassword.Length - 13 - salt.Length;
        //    if (subkeyLength < 128 / 8)
        //    {
        //        return false;
        //    }
        //    var expectedSubkey = new byte[subkeyLength];
        //    Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

        //    // Hash the incoming password and verify it
        //    var actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, prf, iterCount, subkeyLength);
        //    return actualSubkey.SequenceEqual(expectedSubkey);
        //}

        //private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        //{
        //    buffer[offset + 0] = (byte)(value >> 24);
        //    buffer[offset + 1] = (byte)(value >> 16);
        //    buffer[offset + 2] = (byte)(value >> 8);
        //    buffer[offset + 3] = (byte)(value >> 0);
        //}

        //private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        //{
        //    return ((uint)(buffer[offset + 0]) << 24)
        //        | ((uint)(buffer[offset + 1]) << 16)
        //        | ((uint)(buffer[offset + 2]) << 8)
        //        | ((uint)(buffer[offset + 3]));
        //}
    }
}
