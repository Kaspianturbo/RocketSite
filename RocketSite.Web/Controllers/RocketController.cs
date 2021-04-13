using Microsoft.AspNetCore.Mvc;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Web.Controllers
{
    public class RocketController : Controller
    {
        ICRUDRepository<Rocket> _repository;
        public RocketController(ICRUDRepository<Rocket> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetUsers());
        }

        public ActionResult Details(string name)
        {
            Rocket user = _repository.Get(name);
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Rocket user)
        {
            _repository.Create(user);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name)
        {
            Rocket user = _repository.Get(name);
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Rocket user)
        {
            _repository.Update(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string name)
        {
            Rocket user = _repository.Get(name);
            if (user != null)
                return View(user);
            return NotFound();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
