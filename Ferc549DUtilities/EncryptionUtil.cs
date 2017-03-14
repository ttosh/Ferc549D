using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using Ferc549DAttributes;

namespace Ferc549DUtilities {
  [Author("Timothy Tosh", version = 1.0)]
  public class EncryptionUtil {

    private const string EncryptionKey = "097D19DDC6D101C2E0530A0A3FD801C2"; //DO NOT ALTER THE VALUE

    private static Rijndael GetRijndael() {
      var derived = KeyFromString(EncryptionKey);
      var rijndael = Rijndael.Create();
      rijndael.Key = derived.GetBytes(256 / 8);
      rijndael.IV = derived.GetBytes(rijndael.BlockSize / 8);
      return rijndael;
    }

    private static Rfc2898DeriveBytes KeyFromString(string password) {
      var salt = new byte[] { 10, 12, 133, 55, 61, 11, 186, 52 }; //DO NOT ALTER THE VALUES
      const int iterations = 1000; //DO NOT ALTER THE VALUE
      return new Rfc2898DeriveBytes(password, salt, iterations);
    }

    public static string Encrypt(string valueToEncrypt) {
      var encoding = new UTF8Encoding();
      var bytes = encoding.GetBytes(valueToEncrypt);
      using (var rijndael = GetRijndael())
      using (var encryptor = rijndael.CreateEncryptor())
      using (var stream = new MemoryStream())
      using (var crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write)) {
        crypto.Write(bytes, 0, bytes.Length);
        crypto.FlushFinalBlock();
        stream.Position = 0;
        var encrypted = new byte[stream.Length];
        stream.Read(encrypted, 0, encrypted.Length);
        return Convert.ToBase64String(encrypted);
      }
    }

    public static string Decrypt(string encrypted) {
      if (!IsBase64String(encrypted))
        return string.Empty;
      var encryptedValue = Convert.FromBase64String(encrypted);

      var encoding = new UTF8Encoding();
      using (var rijndael = GetRijndael())
      using (var decryptor = rijndael.CreateDecryptor())
      using (var stream = new MemoryStream())
      using (var crypto = new CryptoStream(stream, decryptor, CryptoStreamMode.Write)) {
        crypto.Write(encryptedValue, 0, encryptedValue.Length);
        crypto.FlushFinalBlock();
        stream.Position = 0;
        var decryptedBytes = new Byte[stream.Length];
        stream.Read(decryptedBytes, 0, decryptedBytes.Length);
        return encoding.GetString(decryptedBytes);
      }
    }

    private static bool IsBase64String(string s) {
      s = s.Trim();
      return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }
  }
}
