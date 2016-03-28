using RTDS.Domain.Concrete;
using RTDS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDS.WebUI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(_unitOfWork.GetRepository<Role>().GetAll());
        }

        public ViewResult Edit(int roleID)
        {           
            
            return View(_unitOfWork.GetRepository<Role>().GetAll().FirstOrDefault(p => p.RoleID == roleID));
        }

        [HttpPost]
        public ActionResult Edit(Role role)
        {           
            if (ModelState.IsValid)
            {
                var existRole = _unitOfWork.GetRepository<Role>().Find(b=>b.RoleID==role.RoleID).FirstOrDefault();
                if (existRole != null)
                {
                    existRole.RoleName = role.RoleName;                    
                }
                else
                {
                    _unitOfWork.GetRepository<Role>().Add(role);
                }               
            }
            _unitOfWork.Save();

            TempData["message"] = string.Format("Запис було збережено");
            return RedirectToAction("Index");
           
        }

        public ViewResult Create()
        {            
            return View("Edit", new Role());
        }


        public ViewResult Delete(int roleID)
        {
            if (_unitOfWork.GetRepository<Branch>().Delete(roleID))
            {                
                _unitOfWork.Save();

                TempData["message"] = string.Format("Запис було видалено");

            }
            return View("Index", _unitOfWork.GetRepository<Role>().GetAll());
        }

        public JsonResult AddNewRole(string roleName)
        {
            var existCity = _unitOfWork.GetRepository<Role>().Find(c => c.RoleName == roleName);
            if (existCity.Count() != 0)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rols=_unitOfWork.GetRepository<Role>().GetAll();
                int max=0;
                foreach (var item in rols)
                {
                    if (item.RoleID>max)
                    {
                        max = item.RoleID;
                    }
                }
                _unitOfWork.GetRepository<Role>().Add(new Role() { RoleName = roleName, RoleID=max+1  });
                _unitOfWork.Save();
            }
            var newRole = _unitOfWork.GetRepository<Role>().Find(c => c.RoleName == roleName).FirstOrDefault();

            return Json(newRole, JsonRequestBehavior.AllowGet);            
        }


   

    }
}
