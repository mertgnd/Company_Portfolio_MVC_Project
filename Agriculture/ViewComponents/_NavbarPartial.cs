using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.ViewComponents
{
	public class _NavbarPartial : ViewComponent
	{
        private readonly ISliderLogoService _sliderLogoService;

        public _NavbarPartial(ISliderLogoService sliderLogoService)
        {
            _sliderLogoService = sliderLogoService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _sliderLogoService.GetListAll();
            return View(values);
        }
    }
}
