using RTDS.Domain.Concrete;
using RTDS.Domain.Entities;
using RTDS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDS.WebUI.Controllers
{
    [Authorize]
    public class BranchController : Controller
    {
       private UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(_unitOfWork.GetRepository<Branch>().GetAll());
        }

        public ViewResult Edit(int branchID)
        {           
            InitDropDownListItems();
            return View(_unitOfWork.GetRepository<Branch>().GetAll().FirstOrDefault(p => p.BranchID == branchID));
        }

        [HttpPost]
        public ActionResult Edit(Branch vm)
        {           
            if (ModelState.IsValid)
            {
                var existBranch = _unitOfWork.GetRepository<Branch>().Find(b=>b.BranchID==vm.BranchID).FirstOrDefault();
                if (existBranch != null)
                {
                    existBranch.BranchTypeID = vm.BranchTypeID;
                    existBranch.CityID = vm.CityID;
                    existBranch.House = vm.House;
                    existBranch.Street = vm.Street;
                }
                else
                {
                    _unitOfWork.GetRepository<Branch>().Add(vm);
                }               
            }
            _unitOfWork.Save();

            TempData["message"] = string.Format("Запис було збережено");
            return RedirectToAction("Index");
           
        }

        public ViewResult Create()
        {
            InitDropDownListItems();
            return View("Edit", new Branch());
        }


        public ViewResult Delete(int branchID)
        {
            if (_unitOfWork.GetRepository<Branch>().Delete(branchID))
            {
                Branch branch = _unitOfWork.GetRepository<Branch>().GetById(branchID);
                _unitOfWork.Save();

                TempData["message"] = string.Format("Запис було видалено");

            }
            return View("Index", _unitOfWork.GetRepository<Branch>().GetAll());
        }

        public JsonResult AddNewCity(string cityName)
        {
            var existCity = _unitOfWork.GetRepository<City>().Find(c => c.CityName == cityName);
            if (existCity.Count() != 0)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var cities=_unitOfWork.GetRepository<City>().GetAll();
                int max=0;
                foreach (var item in cities)
                {
                    if (item.CityID>max)
                    {
                        max = item.CityID;
                    }
                }
                _unitOfWork.GetRepository<City>().Add(new City() { CityName = cityName, CityID=max+1  });
                _unitOfWork.Save();
            }
            City newCity = _unitOfWork.GetRepository<City>().Find(c => c.CityName == cityName).FirstOrDefault();

            return Json(newCity, JsonRequestBehavior.AllowGet);            
        }


        private void InitDropDownListItems()
        {
            var cities = from c in _unitOfWork.GetRepository<City>().GetAll()
                         select c;            
            List<SelectListItem> cityIdList = new List<SelectListItem>();       

            foreach (var item in cities)
            {
                cityIdList.Add(new SelectListItem()
                {
                    Text = item.CityName,
                    Value = item.CityID.ToString()
                });
            }
             var types = from c in _unitOfWork.GetRepository<BranchType>().GetAll()
                         select c;            
            List<SelectListItem> typeList = new List<SelectListItem>();       

            foreach (var item in types)
            {
                typeList.Add(new SelectListItem()
                {
                    Text = item.BranchName.ToString(),
                    Value = item.BranchTypeID.ToString()
                });
            }
            ViewBag.City = cityIdList;
            ViewBag.Type = typeList;
        }
    }
}
