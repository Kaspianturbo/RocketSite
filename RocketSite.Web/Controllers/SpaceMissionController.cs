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
    public class SpaceMissionController : Controller
    {
        ICRUDRepository<SpaceMission> _repository;
        public SpaceMissionController(ICRUDRepository<SpaceMission> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name)
        {
            SpaceMission user = _repository.Get(new SpaceMission { Name = name });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SpaceMission @object)
        {
            _repository.Create(@object);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name)
        {
            SpaceMission user = _repository.Get(new SpaceMission { Name = name});
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(SpaceMission @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name)
        {
            _repository.Delete(new SpaceMission { Name = name});
            return RedirectToAction("Index");
        }
    }
}
