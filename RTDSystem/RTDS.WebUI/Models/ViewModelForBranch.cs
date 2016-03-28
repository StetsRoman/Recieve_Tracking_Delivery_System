using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RTDS.Domain.Entities;

namespace RTDS.WebUI.Models
{
    public class ViewModelForBranch
    {
        public Branch Branch { get; set; }
        public City City { get; set; }
    }
}