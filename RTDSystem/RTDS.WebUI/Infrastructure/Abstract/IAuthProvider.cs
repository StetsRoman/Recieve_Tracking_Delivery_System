using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDS.WebUI.Infrastructure.Abstract
{
    public interface IAuthProvider 
    {
        bool Authenticate(string userName, string password);
    }
}
