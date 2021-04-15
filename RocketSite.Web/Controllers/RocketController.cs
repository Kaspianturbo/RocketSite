﻿using Microsoft.AspNetCore.Mvc;
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
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name, string version)
        {
            Rocket user = _repository.Get(new Rocket {Name = name, Version = version });
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

        public ActionResult Edit(string name, string version)
        {
            Rocket user = _repository.Get(new Rocket { Name = name, Version = version});
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Rocket user, Key key)
        {
            _repository.Update(user, key);
            return RedirectToAction("Index");
        }


        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(string name, string version)
        {
            _repository.Delete(new Rocket { Name = name, Version = version });
            return RedirectToAction("Index");
        }
    }
}
