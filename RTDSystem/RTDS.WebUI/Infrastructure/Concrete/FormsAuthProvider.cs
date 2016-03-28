using RTDS.Domain.Concrete;
using RTDS.Domain.Entities;
using RTDS.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace RTDS.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {

        public bool Authenticate(string userName, string password)
        {
            UnitOfWork uow = new UnitOfWork();
            string realPassword = Sha1Hash.GetShaHash(password);
            bool result = false;
            User existUser = uow.GetRepository<User>().Find(u => u.UserName == userName && u.PasswordHash == realPassword).FirstOrDefault();

            if (existUser != null)
            {
                result = true;
                //bool result = FormsAuthentication.Authenticate(userName, password);

                FormsAuthentication.SetAuthCookie(userName, false);
            }
            return result;
        }

        private string GetsSHA1HashData(string data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashData = sha1.ComputeHash(System.Text.Encoding.Default.GetBytes(data));
            System.Text.StringBuilder returnValue = new System.Text.StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }
            return returnValue.ToString();
        }

    }

}