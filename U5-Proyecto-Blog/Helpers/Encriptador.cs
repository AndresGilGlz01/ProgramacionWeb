using System.Security.Cryptography;
using System.Text;

namespace U5_Proyecto_Blog.Helpers;

public static class Encriptador
{
    public static string StringToSHA512(string s)
    {
        using var sha512 = SHA512.Create();

        var bytes = Encoding.UTF8.GetBytes(s);
        var hash = sha512.ComputeHash(bytes);

        return Convert.ToHexString(hash).ToLower();
    }
}
