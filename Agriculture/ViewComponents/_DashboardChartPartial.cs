using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.ViewComponents
{
    public class _DashboardChartPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.asp_project_count = 4;
            ViewBag.pyhton_django_project_count = 5;
            ViewBag.java_project_count = 1;
            ViewBag.js_project_count = 3;
            ViewBag.html_css_project_count = 17;
            return View();
        }
    }
}