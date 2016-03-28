using RTDS.Domain.Concrete;
using RTDS.Domain.Entities;
using RTDS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDS.WebUI.Controllers
{
    [Authorize]
    public class PackageController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {

            List<Package> list = _unitOfWork.GetRepository<Package>().GetAll().ToList();
            GetPackagesStatusData(list);
            return View(list);
        }

        public ViewResult Edit(int packageId)
        {

            ViewModelForPackage model = new ViewModelForPackage();
            model.Package = _unitOfWork.GetRepository<Package>().GetAll().Where(p => p.PackagID == packageId).FirstOrDefault();
            model.Receiver = _unitOfWork.GetRepository<Client>().Find(r => r.ClientID == model.Package.ReceiverID).FirstOrDefault();
            model.Sender = _unitOfWork.GetRepository<Client>().Find(r => r.ClientID == model.Package.SenderID).FirstOrDefault();
            InitDropDownListItems();
            return View(model);
        }
        private void InitDropDownListItems()
        {
            UnitOfWork uow = new UnitOfWork();
            var pkges = from r in uow.GetRepository<Package_Statuses>().GetAll()
                        select r;
            List<SelectListItem> pkgStatusList = new List<SelectListItem>();

            foreach (var item in pkges)
            {
                pkgStatusList.Add(new SelectListItem()
                {
                    Text = item.Status_Name,
                    Value = item.Package_StatusID.ToString()
                });
            }

            var cities = from c in uow.GetRepository<City>().GetAll()
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
            ViewBag.City = cityIdList;
            ViewBag.PkgStatus = pkgStatusList;
        }
        [HttpPost]
        public ActionResult Edit(ViewModelForPackage vm)
        {
            if (ModelState.IsValid)
            {
                GenerateClients(vm);
                var existPackage = _unitOfWork.GetRepository<Package>().Find(p => p.Package_Number == vm.Package.Package_Number).FirstOrDefault();
                if (existPackage != null)
                {
                    UpdatePackage(vm, existPackage);
                    _unitOfWork.Save();
                }
                else
                {

                    var pkgs = _unitOfWork.GetRepository<Package>().GetAll();
                    int max = 0;
                    foreach (var item in pkgs)
                    {
                        if (item.PackagID > max)
                        {
                            max = item.PackagID;
                        }
                    }
                    Package newPkg = new Package();
                    UpdatePackage(vm, newPkg);
                    newPkg.PackagID = max + 1;
                    newPkg.Send_Date = DateTime.Today;
                    newPkg.Branch = _unitOfWork.GetRepository<Branch>().Find(b => b.BranchID == vm.Package.CurrentLocation).FirstOrDefault();
                    newPkg.Branch1 = _unitOfWork.GetRepository<Branch>().Find(b => b.BranchID == vm.Package.DestinationLocation).FirstOrDefault();
                    newPkg.Branch2 = _unitOfWork.GetRepository<Branch>().Find(b => b.BranchID == vm.Package.SourceLocation).FirstOrDefault();
                    newPkg.Package_Statuses = _unitOfWork.GetRepository<Package_Statuses>().Find(b => b.Package_StatusID== vm.Package.StatusID).FirstOrDefault();

                    _unitOfWork.GetRepository<Package>().Add(newPkg);
                    _unitOfWork.Save();
                    var pk = _unitOfWork.GetRepository<Package>().Find(p => p.PackagID == newPkg.PackagID).FirstOrDefault();
                    return View("PackageOrder", pk);
                }
            }

            TempData["message"] = string.Format("Запис №: {0} було збережено ", vm.Package.Package_Number);
            return RedirectToAction("Index");
        }


        private void UpdatePackage(ViewModelForPackage vm, Package existPackage)
        {
            existPackage.PackagID = vm.Package.PackagID;
            existPackage.Package_Number = vm.Package.Package_Number;
            existPackage.Delivery_Price = vm.Package.Delivery_Price;
            existPackage.Weith = vm.Package.Weith;
            existPackage.Size = vm.Package.Size;
            existPackage.Send_Date = vm.Package.Send_Date;
            existPackage.StatusID = vm.Package.StatusID;
            existPackage.CurrentLocation = vm.Package.CurrentLocation;
            existPackage.SourceLocation = vm.Package.SourceLocation;
            existPackage.DestinationLocation = vm.Package.DestinationLocation;

            existPackage.SenderID = _unitOfWork.GetRepository<Client>()
            .Find
            (
            c => c.First_Name == vm.Sender.First_Name &&
                c.Last_Name == vm.Sender.Last_Name &&
                c.Phone_Number == vm.Sender.Phone_Number
            ).FirstOrDefault().ClientID;
            existPackage.ReceiverID = _unitOfWork.GetRepository<Client>()
            .Find
            (
            c => c.First_Name == vm.Receiver.First_Name &&
                c.Last_Name == vm.Receiver.Last_Name &&
                c.Phone_Number == vm.Receiver.Phone_Number
            ).FirstOrDefault().ClientID;
        }

        private void GenerateClients(ViewModelForPackage vm)
        {
            var existSender = _unitOfWork.GetRepository<Client>()
                .Find
                (
                c => c.First_Name == vm.Sender.First_Name &&
                    c.Last_Name == vm.Sender.Last_Name &&
                    c.Phone_Number == vm.Sender.Phone_Number
                ).FirstOrDefault();
            if (existSender == null)
            {
                _unitOfWork.GetRepository<Client>().Add(vm.Sender);
            }


            var existReceiver = _unitOfWork.GetRepository<Client>()
                .Find
                (
                c => c.First_Name == vm.Receiver.First_Name &&
                    c.Last_Name == vm.Receiver.Last_Name &&
                    c.Phone_Number == vm.Receiver.Phone_Number
                ).FirstOrDefault();
            if (existReceiver == null)
            {
                _unitOfWork.GetRepository<Client>().Add(vm.Receiver);
            }

            _unitOfWork.Save();
        }

        public ViewResult Create()
        {
            InitDropDownListItems();
            return View("Edit", new ViewModelForPackage() { Package = new Package(), Receiver = new Client(), Sender = new Client() });
        }

        public ViewResult Delete(int packageId)
        {

            if (_unitOfWork.GetRepository<Package>().Delete(packageId))
            {
                Package pkg = _unitOfWork.GetRepository<Package>().GetById(packageId);
                _unitOfWork.Save();

                TempData["message"] = string.Format("Запис № {0} був видалений", pkg.Package_Number);

            }
            List<Package> list = _unitOfWork.GetRepository<Package>().GetAll().ToList();
            GetPackagesStatusData(list);

            return View("Index", list);
        }

        private void GetPackagesStatusData(List<Package> lst)
        {
            ViewBag.NumOfAllPkg = lst.Count;
            var sendPkg = from Package p in lst
                          where p.Package_Statuses.Status_Name == "Відправлено"
                          select p;
            ViewBag.NumOfSendPkg = sendPkg.Count();

            var delPkg = from Package p in lst
                         where p.Package_Statuses.Status_Name == "Доставлено"
                         select p;
            ViewBag.NumOfSDelPkg = delPkg.Count();

            var inRoadPkg = from Package p in lst
                            where p.Package_Statuses.Status_Name == "В дорозі"
                            select p;
            ViewBag.NumOfPkgInRoad = inRoadPkg.Count();
        }
        public JsonResult GetBranches(string id)
        {

            List<SelectListItem> branches = new List<SelectListItem>();

            foreach (var item in _unitOfWork.GetRepository<Branch>().GetAll())
            {
                if (item.CityID == int.Parse(id))
                {
                    branches.Add(new SelectListItem { Text = string.Format("{0}, {1}", item.Street, item.House), Value = item.BranchID.ToString() });
                }
            }

            return Json(new SelectList(branches, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
    }
}
