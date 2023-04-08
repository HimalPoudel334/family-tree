using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTree.ViewModels;
using FamilyTreeLib.Dtos;
using FamilyTreeLib.Repositories;
using FamilyTreeLib.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, IPersonRepository personRepository, ILogger<PersonController> logger)
        {
            _personService = personService;
            _personRepository = personRepository;
            _logger = logger;
        }
        /*// GET: Person
        public ActionResult Index()
        {
            return View();
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    */
        // GET: Person/Create
        [HttpGet]
        public ActionResult Create()
        {
            PersonCreateViewModel model = new();
            return View(model);
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                PersonCreateDto dto = new()
                {
                    FirstName = model.FirstName,
                    FatherId = model.FatherId,
                    Mother = model.MotherName,
                    Dob = model.Dob,
                    WifeName = model.WifeName,
                    Image = "",
                    WifeImage = ""
                };

                await _personService.Add(dto);
                _logger.LogInformation(@"New person with name {} added", dto.FirstName);
                
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Oops! Could not add person. Something went wrong!");
                return View(model);
            }
        }
    /*
        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}