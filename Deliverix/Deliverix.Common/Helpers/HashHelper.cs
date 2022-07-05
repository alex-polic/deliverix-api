using System.Security.Cryptography;
using System.Text;
using Deliverix.Common.Exceptions;

namespace Deliverix.Common.Helpers;

public static class HashHelper
{
    public static string Hash(string raw)
    {
        if (string.IsNullOrEmpty(raw)) throw new BusinessException("Password cannot be empty.", 400);

        StringBuilder hash = new StringBuilder(string.Empty);

        using (var crypt = new SHA256Managed())
        {
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(raw));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
        }

        return hash.ToString();
    }
}