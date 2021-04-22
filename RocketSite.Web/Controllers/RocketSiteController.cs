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
    public class RocketSiteController : Controller
    {
        IRocketSiteRepository _repository;
        public RocketSiteController(IRocketSiteRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View1(string name, string type, string startDate, string endDate)
        {
            var res = _repository.Get1(name, type, startDate, endDate);
            return View(res);
        }

        public ActionResult View2(int mass, string eqName, string eqProducer)
        {
            var res = _repository.Get2(mass, eqName, eqProducer);
            return View(res);
        }
        public ActionResult View3(string name, string status)
        {
            var res = _repository.Get3(name, status);
            return View(res);
        }
        public ActionResult View4(string name, string coach, string area)
        {
            var res = _repository.Get4(name, coach, area);
            return View(res);
        }
        public ActionResult View5(string area, string duration)
        {
            var res = _repository.Get5(area, duration);
            return View(res);
        }
        public ActionResult View6(string name, string status)
        {
            var res = _repository.Get6(name, status);
            return View(res);
        }
        public ActionResult View7(string name, string version)
        {
            var res = _repository.Get7(name, version);
            return View(res);
        }
        public ActionResult View8(string name, string country, string status)
        {
            var res = _repository.Get8(name, country, status);
            return View(res);
        }
        public ActionResult View9(string date, string status)
        {
            var res = _repository.Get9(date, status);
            return View(res);
        }
        public ActionResult View10(string name)
        {
            var res = _repository.Get10(name);
            return View(res);
        }
        public ActionResult View11(string name, string status, string timezone)
        {
            var res = _repository.Get11(name, status, timezone);
            return View(res);
        }
        public ActionResult View12(string name, string status, string timezone)
        {
            var res = _repository.Get12(name, status, timezone);
            return View(res);
        }
    }
}
