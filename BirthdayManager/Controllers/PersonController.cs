using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayManager.Models;
using BirthdayManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayManager.Controllers
{
    public class PersonController : Controller
    {
        private PersonRepository PersonRepository { get; set; }

        public PersonController(PersonRepository personRepository)
        {
            this.PersonRepository = personRepository;
        }

        // GET: PersonController
        public ActionResult Index()
        {
            var model = PersonRepository.GetAll();
            return View(model);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            var model = this.PersonRepository.GetById(id);
            return View(model);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            try
            {
                this.PersonRepository.Save(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Search([FromQuery] string query)
        {
            var model = this.PersonRepository.Search(query);

            return View("Index", model);
        }

        //GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = this.PersonRepository.GetById(id);
            return View(model);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Person model)
        {
            try
            {
                this.PersonRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = this.PersonRepository.GetById(id);
            return View(model);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Person model)
        {
            try
            {
                model.Id = id;
                this.PersonRepository.Delete(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
