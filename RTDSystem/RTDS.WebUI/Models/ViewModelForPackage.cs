using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RTDS.Domain.Entities;

namespace RTDS.WebUI.Models
{
    public class ViewModelForPackage
    {
        public Package Package { get; set; }
        public Client Sender { get; set; }
        public Client Receiver { get; set; }
    }
}