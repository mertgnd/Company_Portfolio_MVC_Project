using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;
using Image = SixLabors.ImageSharp.Image;

namespace AgriculturePresentation.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5 MB olarak örnek değer, ihtiyacınıza göre değiştirilebilir

        public TeamController(ITeamService teamService, IWebHostEnvironment hostingEnvironment)
        {
            _teamService = teamService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var values = _teamService.GetListAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTeam(Team team)
        {
            TeamValidator validationRules = new TeamValidator();
            ValidationResult result = validationRules.Validate(team);

            if (result.IsValid)
            {
                if (team.ImageFile != null && team.ImageFile.Length > 0)
                {
                    // Resim boyutu kontrolü
                    if (team.ImageFile.Length > MAX_FILE_SIZE)
                    {
                        ModelState.AddModelError("ImageFile", "Resim boyutu çok büyük.");
                        return View(team);
                    }

                    // Hedef klasör var mı?
                    var targetFolder = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images");
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    // Resim adını benzersiz hale getir
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + team.ImageFile.FileName;

                    // Hedef klasörün yetkileri kontrolü
                    var imagePath = Path.Combine(targetFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        // ImageSharp ile resmi boyutlandır
                        using (var image = Image.Load(team.ImageFile.OpenReadStream()))
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new Size(300, 350),
                                Mode = ResizeMode.BoxPad
                            }));
                            image.Save(stream, new JpegEncoder());
                        }
                    }

                    // ImageUrl'i burada oluştur
                    team.ImageUrl = "/web/images/" + uniqueFileName;
                }

                _teamService.Insert(team);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View(team);
        }

        public IActionResult DeleteTeam(int id)
        {
            var team = _teamService.GetById(id);

            // İlgili resmi sil
            if (!string.IsNullOrEmpty(team.ImageUrl))
            {
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images", team.ImageUrl.Substring(team.ImageUrl.LastIndexOf("/") + 1));

                if (System.IO.File.Exists(imagePath))
                {
                    // Resmi sil
                    System.IO.File.Delete(imagePath);
                }
            }

            // Takım üyesini sil
            _teamService.Delete(team);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateTeam(int id)
        {
            var value = _teamService.GetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateTeam(Team team)
        {
            TeamValidator validationRules = new TeamValidator();
            ValidationResult result = validationRules.Validate(team);

            if (result.IsValid)
            {
                // Eski resmi sil
                var oldTeam = _teamService.GetById(team.TeamID);
                if (!string.IsNullOrEmpty(oldTeam.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images", oldTeam.ImageUrl.Substring(oldTeam.ImageUrl.LastIndexOf("/") + 1));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                if (team.ImageFile != null && team.ImageFile.Length > 0)
                {
                    // Yeni resim dosyası seçilmiş

                    // Resim boyutu kontrolü
                    if (team.ImageFile.Length > MAX_FILE_SIZE)
                    {
                        ModelState.AddModelError("ImageFile", "Resim boyutu çok büyük.");
                        return View(team);
                    }

                    // Hedef klasör var mı?
                    var targetFolder = Path.Combine(_hostingEnvironment.WebRootPath, "web", "images");
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    // Resim adını benzersiz hale getir
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + team.ImageFile.FileName;

                    // Hedef klasörün yetkileri kontrolü
                    var imagePath = Path.Combine(targetFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        // ImageSharp ile resmi boyutlandır
                        using (var image = Image.Load(team.ImageFile.OpenReadStream()))
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new Size(300, 350),
                                Mode = ResizeMode.BoxPad
                            }));
                            image.Save(stream, new JpegEncoder());
                        }
                    }

                    // ImageUrl'i burada oluştur
                    team.ImageUrl = "/web/images/" + uniqueFileName;
                }
                else
                {
                    // Yeni resim dosyası seçilmemiş, eski resmin bilgilerini kullan
                    team.ImageUrl = oldTeam.ImageUrl;
                }

                _teamService.Update(team);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(team);
        }
    }
}