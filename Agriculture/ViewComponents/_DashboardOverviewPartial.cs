using DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.ViewComponents
{
    public class _DashboardOverviewPartial : ViewComponent
    {
        AgricultureContext c = new AgricultureContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.teamCount = c.Teams.Count();
            ViewBag.serviceCount = c.Services.Count();
            ViewBag.messageCount = c.Contacts.Count();
            ViewBag.currentMonthMessage = 3;

            ViewBag.announcementTrue = c.Announcements.Where(x => x.Status == true).Count();
            ViewBag.announcementFalse = c.Announcements.Where(x => x.Status == false).Count();

            ViewBag.Aspnet = c.Teams.Where(x => x.Title == "Asp.net Developer").Select(y => y.PersonName).FirstOrDefault();
            ViewBag.PythonDjango = c.Teams.Where(x => x.Title == "Python Django Developer").Select(y => y.PersonName).FirstOrDefault();
            ViewBag.HtmlCss = c.Teams.Where(x => x.Title == "Html&Css Developer").Select(y => y.PersonName).FirstOrDefault();
            ViewBag.Java = c.Teams.Where(x => x.Title == "Java Developer").Select(y => y.PersonName).FirstOrDefault();
            return View();
        }
    }
}
