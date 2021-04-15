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
    public class TrainingProgramController : Controller
    {
        ICRUDRepository<TrainingProgram> _repository;
        public TrainingProgramController(ICRUDRepository<TrainingProgram> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(string name, string coach)
        {
            TrainingProgram user = _repository.Get(new TrainingProgram { Name = name, Coach = coach });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TrainingProgram cargo)
        {
            _repository.Create(cargo);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name, string coach)
        {
            TrainingProgram user = _repository.Get(new TrainingProgram { Name = name, Coach = coach });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(TrainingProgram @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name, string coach)
        {
            _repository.Delete(new TrainingProgram { Name = name, Coach = coach });
            return RedirectToAction("Index");
        }
    }
}
