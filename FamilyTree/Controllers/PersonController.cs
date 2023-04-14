using FamilyTree.Extensions;
using FamilyTree.Helpers;
using FamilyTree.ViewModels;
using FamilyTreeLib.Dtos;
using FamilyTreeLib.Exceptions;
using FamilyTreeLib.Repositories;
using FamilyTreeLib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IPersonRepository _personRepository;
        private readonly IFileUploadHelper _fileUploadHelper;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, IPersonRepository personRepository, IFileUploadHelper fileUploadHelper, ILogger<PersonController> logger)
        {
            _personService = personService;
            _personRepository = personRepository;
            _fileUploadHelper = fileUploadHelper;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            //check if the uploaded file is indeed an image
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError(string.Empty, "The uploaded image is not an image");
                return View(model);
            }

            if (model.WifeImage != null && !model.WifeImage.IsImage())
            {
                ModelState.AddModelError(string.Empty, "The uploaded wife's image is not an image");
                return View(model);
            }
            
            PersonCreateDto dto = new()
            {
                FirstName = model.FirstName,
                FatherId = model.FatherId,
                Mother = model.MotherName,
                Dob = model.Dob.Date,
                WifeName = model.WifeName,
                Image = _fileUploadHelper.ReturnUniqueFileName(model.Image),
                WifeImage = model.WifeImage != null ? _fileUploadHelper.ReturnUniqueFileName(model.WifeImage) : null
            };
            try {
                await _personService.Add(dto);
                await _fileUploadHelper.SaveFile(model.Image, dto.Image);
                if(model.WifeImage != null && dto.WifeImage != null) await _fileUploadHelper.SaveFile(model.WifeImage, dto.WifeImage, "wives");
            }
            catch(Exception ex)
            {
                _fileUploadHelper.RemoveFile(dto.Image);
                if(dto.WifeImage != null) _fileUploadHelper.RemoveFile(dto.WifeImage);
                
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, "Oops! Could not add person. Something went wrong!");
                return View(model);
            }
            
            _logger.LogInformation(@"New person with name {} added", dto.FirstName);
            return RedirectToAction(nameof(Create));
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

        public async Task<IActionResult> GetFatherList()
        {
            var fathers = await _personRepository.GetQueryable().Select(p => new ParentSelectViewModel()
            {
                Id = p.Id,
                FirstName = p.FirstName
            }).ToListAsync();
            
            return Json(fathers);
        }

        public async Task<IActionResult> GetMotherName(int fatherId)
        {
            if (fatherId <= 0)
            {
                return BadRequest();
            }
            
            var father = await _personRepository.GetByIdAsync(fatherId) ?? throw new FatherDoesNotExistException();
            return Json(father.WifeName ?? throw new MotherNotFoundException());

        }
    }
}
