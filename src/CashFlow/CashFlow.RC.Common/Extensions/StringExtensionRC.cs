using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.Common.Extensions
{
    public static class StringExtensionRC
    {
        public static Guid ConvertToGuid(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            return new Guid(input.GenerateMD5Hash());
        }

        public static string GenerateMD5Hash(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
