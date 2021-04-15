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
    public class CustomerController : Controller
    {
        ICRUDRepository<Customer> _repository;
        public CustomerController(ICRUDRepository<Customer> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name,  string country)
        {
            Customer user = _repository.Get(new Customer { Name = name, Country = country });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer @object)
        {
            _repository.Create(@object);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, string country)
        {
            Customer user = _repository.Get(new Customer { Name = name, Country = country });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Customer @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, string country)
        {
            _repository.Delete(new Customer { Name = name, Country = country });
            return RedirectToAction("Index");
        }
    }
}
