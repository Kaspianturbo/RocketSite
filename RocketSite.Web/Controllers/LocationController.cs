using Microsoft.AspNetCore.Mvc;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Web.Controllers
{
    public class LocationController : Controller
    {
        ICRUDRepository<Location> _repository;
        public LocationController(ICRUDRepository<Location> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View(_repository.GetObjects());
        }

        public ActionResult Details(double latitude, double longitude)
        {
            Location user = _repository.Get(new Location { Latitude = latitude, Longitude = longitude });
            if (user != null)
                return View(user);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Location location)
        {
            _repository.Create(location);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(double latitude, double longitude)
        {
            Location user = _repository.Get(new Location { Latitude = latitude, Longitude = longitude });
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Location @object, Key key)
        {
            _repository.Update(@object, key);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(double latitude, double longitude)
        {
            _repository.Delete(new Location { Latitude = latitude, Longitude = longitude });
            return RedirectToAction("Index");
        }
    }
}
