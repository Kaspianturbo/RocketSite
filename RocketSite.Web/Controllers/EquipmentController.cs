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
    public class EquipmentController : Controller
    {
        ICRUDRepository<Equipment> _repository;
        public EquipmentController(ICRUDRepository<Equipment> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name,  string producer)
        {
            Equipment user = _repository.Get(new Equipment { Name = name, Producer = producer });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Equipment @object)
        {
            _repository.Create(@object);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, string producer)
        {
            Equipment user = _repository.Get(new Equipment { Name = name, Producer = producer });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Equipment @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, string producer)
        {
            _repository.Delete(new Equipment { Name = name, Producer = producer });
            return RedirectToAction("Index");
        }
    }
}
