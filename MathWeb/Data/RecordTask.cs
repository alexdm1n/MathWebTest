using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MathWeb.Models;
using Microsoft.AspNetCore.Authentication;
using MathWeb.Controllers;

namespace MathWeb.Data
{
    public class RecordTask : Controller
    {
        private readonly ConnectionStringClass _cc;
        public RecordTask(ConnectionStringClass cc)
        {
            _cc = cc;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskMath ec)
        {
            _cc.Add(ec);
            _cc.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
