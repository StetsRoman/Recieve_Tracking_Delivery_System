using RTDS.Domain.Concrete;
using RTDS.Domain.Entities;
using RTDS.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDS.WebUI.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {


        public ActionResult Index()
        {
            UnitOfWork uow = new UnitOfWork();
            return View(uow.GetRepository<User>().GetAll());
        }

        [HttpGet]
        public ViewResult Edit(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            InitDropDownListItems();
            return View(uow.GetRepository<User>().GetAll().FirstOrDefault(p => p.UserID == userId));
        }

        private void InitDropDownListItems()
        {
            UnitOfWork uow = new UnitOfWork();
            var rols = from r in uow.GetRepository<Role>().GetAll()
                       select r;
            var cities = from c in uow.GetRepository<City>().GetAll()
                         select c;

            List<SelectListItem> rolsList = new List<SelectListItem>();
            List<SelectListItem> cityIdList = new List<SelectListItem>();
            foreach (var item in rols)
            {
                rolsList.Add(new SelectListItem()
                {
                    Text = item.RoleName,
                    Value = item.RoleID.ToString()
                });
            }

            foreach (var item in cities)
            {
                cityIdList.Add(new SelectListItem()
                {
                    Text = item.CityName,
                    Value = item.CityID.ToString()
                });
            }
            ViewBag.City = cityIdList;
            ViewBag.Rols = rolsList;
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            UnitOfWork uow = new UnitOfWork();
            if (ModelState.IsValid)
            {
                var exist = uow.GetRepository<User>().Find(U => U.UserID == user.UserID).FirstOrDefault();
                if (exist != null)
                {
                    exist.UserID = user.UserID;
                    exist.UserName = user.UserName;
                    exist.BranchID = user.BranchID;                  
                    exist.First_Name = user.First_Name;
                    exist.RoleID = user.RoleID;
                    exist.PasswordHash = Sha1Hash.GetShaHash(user.PasswordHash);
                    exist.Phone_Number = user.Phone_Number;
                    exist.Last_Name = user.Last_Name;
                }
                else
                {
                    uow.GetRepository<User>().Add(user);
                }
                uow.Save();

                TempData["message"] = string.Format("Запис: {0} {1} було збережено", user.Last_Name, user.First_Name);
                return RedirectToAction("Index");
            }
            else
            {
                InitDropDownListItems();
                return View(user);
            }
        }

        public ViewResult Create()
        {
            InitDropDownListItems();
            return View("Edit", new User());
        }

       
        public ActionResult Delete(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.GetRepository<User>().GetById(userId);
            if (uow.GetRepository<User>().Delete(userId))
            {
                
                uow.Save();

                TempData["message"] = string.Format("{0} {1} був видалений", user.Last_Name, user.First_Name);

            }
            return RedirectToAction("Index");
        }

        public JsonResult GetBranches(string id)
        {
            
            UnitOfWork uow = new UnitOfWork();
            List<SelectListItem> branches = new List<SelectListItem>();

            foreach (var item in uow.GetRepository<Branch>().GetAll())
            {
                if (item.CityID==int.Parse(id))
                {
                    branches.Add(new SelectListItem { Text=string.Format("{0}, {1}",item.Street,item.House), Value=item.BranchID.ToString() });
                }
            }
            return Json(new SelectList(branches, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }



    }

}
