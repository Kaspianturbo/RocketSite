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
    public class CosmodromeController : Controller
    {
        ICRUDRepository<Cosmodrome> _repository;
        public CosmodromeController(ICRUDRepository<Cosmodrome> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name)
        {
            Cosmodrome user = _repository.Get(new Cosmodrome { Name = name });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cosmodrome @object)
        {
            _repository.Create(@object);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, string producer)
        {
            Cosmodrome user = _repository.Get(new Cosmodrome { Name = name });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Cosmodrome @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, string producer)
        {
            _repository.Delete(new Cosmodrome { Name = name });
            return RedirectToAction("Index");
        }
    }
}
