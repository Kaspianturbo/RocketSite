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

        public ActionResult View1(string param1, string param2, string param3, string param4)
        {
            Response1 res = _repository.Get1(param1, param2, param3, param4);
            return View(res);
        }
    }
}
