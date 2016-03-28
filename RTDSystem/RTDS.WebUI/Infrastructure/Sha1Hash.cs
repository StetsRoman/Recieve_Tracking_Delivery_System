using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RTDS.WebUI.Infrastructure
{
    public static class Sha1Hash
    {
       
       public static string GetShaHash(string value)
        {           
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
            byte[] result = sha.Hash;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in result)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}