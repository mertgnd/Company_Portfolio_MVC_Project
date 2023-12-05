using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.ViewComponents
{
	public class _GalleryPartial : ViewComponent
	{
		private readonly IGalleryService _imageService;

		public _GalleryPartial(IGalleryService imageService)
		{
			_imageService = imageService;
		}

		public IViewComponentResult Invoke()
		{
			var values = _imageService.GetListAll();
			return View(values);
		}
	}
}
