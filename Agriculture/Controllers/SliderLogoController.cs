using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AgriculturePresentation.Controllers
{
    public class SliderLogoController : Controller
    {
        private readonly ISliderLogoService _sliderLogoService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5 MB olarak örnek değer, ihtiyacınıza göre değiştirilebilir

        public SliderLogoController(ISliderLogoService sliderLogoService, IWebHostEnvironment hostingEnvironment)
        {
            _sliderLogoService = sliderLogoService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var values = _sliderLogoService.GetListAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddSliderLogo()
        {
            return View();
        }

		[HttpPost]
		public IActionResult AddSliderLogo(SliderLogo sliderLogo)
		{
			SliderLogoValidator validationRules = new SliderLogoValidator();
			ValidationResult result = validationRules.Validate(sliderLogo);

			if (result.IsValid)
			{
				// Save logo without processing
				SaveImage(sliderLogo.ImageFileLogo, "logo");

				// Process and save slider and pop-up images
				sliderLogo.ImageUrlSlider = ProcessAndSaveImage(sliderLogo.ImageFileSlider, "slider", 1680, 800);
				sliderLogo.ImageUrlPopUp = ProcessAndSaveImage(sliderLogo.ImageFilePopUp, "popup", 500, 300);

				_sliderLogoService.Insert(sliderLogo);
				return RedirectToAction("Index");
			}
			else
			{
				HandleValidationErrors(result);
				return View(sliderLogo);
			}
		}

		public IActionResult DeleteSliderLogo(int id)
        {
            var sliderLogo = _sliderLogoService.GetById(id);

            DeleteImageFile(sliderLogo.ImageUrlLogo);

            _sliderLogoService.Delete(sliderLogo);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateSliderLogo(int id)
        {
            var value = _sliderLogoService.GetById(id);
            return View(value);
        }

		[HttpPost]
		public IActionResult UpdateSliderLogo(SliderLogo sliderLogo)
		{
			SliderLogoValidator validationRules = new SliderLogoValidator();
			ValidationResult result = validationRules.Validate(sliderLogo);

			if (result.IsValid)
			{
				var oldSliderLogo = _sliderLogoService.GetById(sliderLogo.Id);

				// Process and save slider and pop-up images
				sliderLogo.ImageUrlSlider = ProcessAndSaveImage(sliderLogo.ImageFileSlider, "slider", 1680, 800);
				sliderLogo.ImageUrlPopUp = ProcessAndSaveImage(sliderLogo.ImageFilePopUp, "popup", 500, 300);

				if (sliderLogo.ImageFileLogo != null)
				{
					sliderLogo.ImageUrlLogo = SaveImage(sliderLogo.ImageFileLogo, "logo");

					DeleteImageFile(oldSliderLogo.ImageUrlLogo);
				}
				else
				{
					sliderLogo.ImageUrlLogo = oldSliderLogo.ImageUrlLogo;
				}

				if (sliderLogo.ImageFileSlider != null)
				{
					DeleteImageFile(oldSliderLogo.ImageUrlSlider);
				}
				else
				{
					sliderLogo.ImageUrlSlider = oldSliderLogo.ImageUrlSlider;
				}

				if (sliderLogo.ImageFilePopUp != null)
				{
					DeleteImageFile(oldSliderLogo.ImageUrlPopUp);
				}
				else
				{
					sliderLogo.ImageUrlPopUp = oldSliderLogo.ImageUrlPopUp;
				}

				_sliderLogoService.Update(sliderLogo);
				return RedirectToAction("Index");
			}
			else
			{
				HandleValidationErrors(result);
				return View(sliderLogo);
			}
		}

		private string SaveImage(IFormFile? imageFile, string prefix)
		{
			if (imageFile != null && imageFile.Length > 0)
			{
				if (imageFile.Length > MAX_FILE_SIZE)
				{
					ModelState.AddModelError($"ImageFile{prefix}", "Resim boyutu çok büyük.");
					return string.Empty;
				}

				var targetFolder = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images");
				if (!Directory.Exists(targetFolder))
				{
					Directory.CreateDirectory(targetFolder);
				}

				var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
				var imagePath = Path.Combine(targetFolder, uniqueFileName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					imageFile.CopyTo(stream);
				}

				// Return the image URL
				return "/web/images/" + uniqueFileName;
			}

			return string.Empty;
		}

		private string ProcessAndSaveImage(IFormFile? imageFile, string prefix, int targetWidth, int targetHeight)
		{
			if (imageFile != null && imageFile.Length > 0)
			{
				if (imageFile.Length > MAX_FILE_SIZE)
				{
					ModelState.AddModelError($"ImageFile{prefix}", "Resim boyutu çok büyük.");
					return string.Empty;
				}

				var targetFolder = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images");
				if (!Directory.Exists(targetFolder))
				{
					Directory.CreateDirectory(targetFolder);
				}

				var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
				var imagePath = Path.Combine(targetFolder, uniqueFileName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					using (var image = Image.Load(imageFile.OpenReadStream()))
					{
						// Resize and pad
						image.Mutate(x => x
							.Resize(new ResizeOptions
							{
								Size = new Size(targetWidth, targetHeight),
								Mode = ResizeMode.Max
							})
							.Pad(targetWidth, targetHeight));

						image.Save(stream, new JpegEncoder());
					}
				}

				return "/web/images/" + uniqueFileName;
			}

			return string.Empty;
		}

		private void DeleteImageFile(string? imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images", imageUrl.Substring(imageUrl.LastIndexOf("/") + 1));

                if (System.IO.File.Exists(imagePath))
                {
                    // Resmi sil
                    System.IO.File.Delete(imagePath);
                }
            }
        }

        private void HandleValidationErrors(ValidationResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
        }
    }
}