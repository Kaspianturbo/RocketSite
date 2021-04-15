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
    public class CargoController : Controller
    {
        ICRUDRepository<Cargo> _repository;
        public CargoController(ICRUDRepository<Cargo> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name, CargoOption type)
        {
            Cargo user = _repository.Get(new Cargo { Name = name, Type = type });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cargo cargo)
        {
            _repository.Create(cargo);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, CargoOption type)
        {
            Cargo user = _repository.Get(new Cargo { Name = name, Type = type });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Cargo @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, CargoOption type)
        {
            _repository.Delete(new Cargo { Name = name, Type = type });
            return RedirectToAction("Index");
        }
    }
}
