using Microsoft.AspNetCore.Mvc;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RocketSite.Common.Responses;

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
    }
}
