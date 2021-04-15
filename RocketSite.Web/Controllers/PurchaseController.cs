using Microsoft.AspNetCore.Mvc;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Web.Controllers
{
    public class PurchaseController : Controller
    {
        ICRUDRepository<Purchase> _repository;
        public PurchaseController(ICRUDRepository<Purchase> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name,  string employeeName)
        {
            Purchase user = _repository.Get(new Purchase { Name = name, Employee = new Employee{Name = employeeName }});
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Purchase @object)
        {
            _repository.Create(@object);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, string employeeName)
        {
            Purchase user = _repository.Get(new Purchase { Name = name, Employee = new Employee { Name = employeeName }});
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Purchase @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, string employeeName)
        {
            _repository.Delete(new Purchase { Name = name, Employee = new Employee { Name = employeeName } });
            return RedirectToAction("Index");
        }
    }
}
