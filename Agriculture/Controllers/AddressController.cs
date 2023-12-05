using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _adressService;

        public AddressController(IAddressService adressService)
        {
            _adressService = adressService;
        }

        public IActionResult Index()
        {
            var values = _adressService.GetListAll();
            return View(values);
        }  

        [HttpGet]
        public IActionResult UpdateAddress(int id)
        {
            var value = _adressService.GetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAddress(Address address)
        {
            AddressValidator validationRules = new AddressValidator();
            ValidationResult result = validationRules.Validate(address);
            if (result.IsValid)
            {
                _adressService.Update(address);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}