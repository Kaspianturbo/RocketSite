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
    public class ResourcesController : Controller
    {
        ICRUDRepository<Resources> _repository;
        public ResourcesController(ICRUDRepository<Resources> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name, ResourceOption type)
        {
            Resources user = _repository.Get(new Resources { Name = name, Type = type });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Resources cargo)
        {
            _repository.Create(cargo);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, ResourceOption type)
        {
            Resources user = _repository.Get(new Resources { Name = name, Type = type });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Resources @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, ResourceOption type)
        {
            _repository.Delete(new Resources { Name = name, Type = type });
            return RedirectToAction("Index");
        }
    }
}
