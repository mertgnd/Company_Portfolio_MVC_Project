using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AgriculturePresentation.Controllers
{
    public class GalleryController : Controller
    {
        private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5 MB olarak örnek değer, ihtiyacınıza göre değiştirilebilir
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService, IWebHostEnvironment hostingEnvironment)
        {
            _galleryService = galleryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var values = _galleryService.GetListAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddGallery()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGallery(Gallery gallery)
        {

            GalleryValidator validationRules = new GalleryValidator();
            ValidationResult result = validationRules.Validate(gallery);

            if (result.IsValid)
            {
                if (gallery.ImageFile != null && gallery.ImageFile.Length > 0)
                {
                    // Resim boyutu kontrolü
                    if (gallery.ImageFile.Length > MAX_FILE_SIZE)
                    {
                        ModelState.AddModelError("ImageFile", "Resim boyutu çok büyük.");
                        return View(gallery);
                    }

                    // Hedef klasör var mı?
                    var targetFolder = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images");
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    // Resim adını benzersiz hale getir
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + gallery.ImageFile.FileName;

                    // Hedef klasörün yetkileri kontrolü
                    var imagePath = Path.Combine(targetFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        // ImageSharp ile resmi boyutlandır
                        using (var image = Image.Load(gallery.ImageFile.OpenReadStream()))
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new Size(200, 200),
                                Mode = ResizeMode.BoxPad
                            }));
                            image.Save(stream, new JpegEncoder());
                        }
                    }

                    // ImageUrl'i burada oluştur
                    gallery.ImageUrl = "/web/images/" + uniqueFileName;
                }

                _galleryService.Insert(gallery);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View(gallery);
        }

        public IActionResult DeleteGallery(int id)
        {
            var gallery = _galleryService.GetById(id);

            // İlgili resmi sil
            if (!string.IsNullOrEmpty(gallery.ImageUrl))
            {
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images", gallery.ImageUrl.Substring(gallery.ImageUrl.LastIndexOf("/") + 1));

                if (System.IO.File.Exists(imagePath))
                {
                    // Resmi sil
                    System.IO.File.Delete(imagePath);
                }
            }

            // Takım üyesini sil
            _galleryService.Delete(gallery);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateGallery(int id)
        {
            var value = _galleryService.GetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateGallery(Gallery gallery)
        {
            GalleryValidator validationRules = new GalleryValidator();
            ValidationResult result = validationRules.Validate(gallery);

            if (result.IsValid)
            {
                // Eski resmi sil
                var oldTeam = _galleryService.GetById(gallery.GalleryID);
                if (!string.IsNullOrEmpty(oldTeam.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images", oldTeam.ImageUrl.Substring(oldTeam.ImageUrl.LastIndexOf("/") + 1));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                if (gallery.ImageFile != null && gallery.ImageFile.Length > 0)
                {
                    // Yeni resim dosyası seçilmiş

                    // Resim boyutu kontrolü
                    if (gallery.ImageFile.Length > MAX_FILE_SIZE)
                    {
                        ModelState.AddModelError("ImageFile", "Resim boyutu çok büyük.");
                        return View(gallery);
                    }

                    // Hedef klasör var mı?
                    var targetFolder = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images");
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    // Resim adını benzersiz hale getir
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + gallery.ImageFile.FileName;

                    // Hedef klasörün yetkileri kontrolü
                    var imagePath = Path.Combine(targetFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        // ImageSharp ile resmi boyutlandır
                        using (var image = Image.Load(gallery.ImageFile.OpenReadStream()))
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new Size(200, 200),
                                Mode = ResizeMode.BoxPad
                            }));
                            image.Save(stream, new JpegEncoder());
                        }
                    }

                    // ImageUrl'i burada oluştur
                    gallery.ImageUrl = "/web/images/" + uniqueFileName;
                }
                else
                {
                    // Yeni resim dosyası seçilmemiş, eski resmin bilgilerini kullan
                    gallery.ImageUrl = oldTeam.ImageUrl;
                }

                _galleryService.Update(gallery);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(gallery);
        }
    }
}
