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
    public class EmployeeController : Controller
    {
        ICRUDRepository<Employee> _repository;
        public EmployeeController(ICRUDRepository<Employee> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name,  string country)
        {
            Employee user = _repository.Get(new Employee { Name = name, Country = country });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee @object)
        {
            _repository.Create(@object);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, string country)
        {
            Employee user = _repository.Get(new Employee { Name = name, Country = country });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Employee @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, string country)
        {
            _repository.Delete(new Employee { Name = name, Country = country });
            return RedirectToAction("Index");
        }
    }
}
