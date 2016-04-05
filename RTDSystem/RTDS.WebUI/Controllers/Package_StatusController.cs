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
    public class Package_StatusController : Controller
    {
        //
        // GET: /Package_Status/
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(_unitOfWork.GetRepository<Package_Statuses>().GetAll());
        }

        public ViewResult Edit(int roleID)
        {

            return View(_unitOfWork.GetRepository<Package_Statuses>().GetAll().FirstOrDefault(p => p.Package_StatusID == roleID));
        }

        [HttpPost]
        public ActionResult Edit(Package_Statuses role)
        {
            if (ModelState.IsValid)
            {
                var existRole = _unitOfWork.GetRepository<Package_Statuses>().Find(b => b.Package_StatusID == role.Package_StatusID).FirstOrDefault();
                if (existRole != null)
                {
                    existRole.Status_Name = role.Status_Name;
                }
                else
                {
                    _unitOfWork.GetRepository<Package_Statuses>().Add(role);
                }
            }
            _unitOfWork.Save();

            TempData["message"] = string.Format("Запис було збережено");
            return RedirectToAction("Index");

        }

        public ViewResult Create()
        {
            return View("Edit", new Package_Statuses());
        }


        public ViewResult Delete(int roleID)
        {
            if (_unitOfWork.GetRepository<Package_Statuses>().Delete(roleID))
            {
                Package_Statuses role = _unitOfWork.GetRepository<Package_Statuses>().GetById(roleID);
                _unitOfWork.Save();

                TempData["message"] = string.Format("Запис було видалено");

            }
            return View("Index", _unitOfWork.GetRepository<Package_Statuses>().GetAll());
        }

        public JsonResult AddNewPkgStatus(string statusName)
        {
            var existCity = _unitOfWork.GetRepository<Package_Statuses>().Find(c => c.Status_Name == statusName);
            if (existCity.Count() != 0)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {                
                _unitOfWork.GetRepository<Package_Statuses>().Add(new Package_Statuses() { Status_Name = statusName });
                _unitOfWork.Save();
            }
            var newStatus = _unitOfWork.GetRepository<Package_Statuses>().Find(c => c.Status_Name == statusName).FirstOrDefault();

            return Json(newStatus, JsonRequestBehavior.AllowGet);
        }




    }

}

