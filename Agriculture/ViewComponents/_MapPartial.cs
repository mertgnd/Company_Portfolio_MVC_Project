using BusinessLayer.Abstract;
using DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.ViewComponents
{
	public class _MapPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			AgricultureContext agricultureContext = new();
			var values = agricultureContext.Addresses.Select(x => x.Mapinfo).FirstOrDefault();
			ViewBag.mapurl = values;
			return View();
		}
	}
}
