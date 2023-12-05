using AgriculturePresentation.Models;
using ClosedXML.Excel;
using DataAccessLayer.Contexts;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace AgriculturePresentation.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StaticReport()
        {
            ExcelPackage excelPackage = new ExcelPackage();
            var workBook = excelPackage.Workbook.Worksheets.Add("Folder1");

            workBook.Cells[1, 1].Value = "Project Name";
            workBook.Cells[1, 2].Value = "Project Content";

            workBook.Cells[2, 1].Value = "Asp.Net Core";
            workBook.Cells[2, 2].Value = "Company Portfolio";

            workBook.Cells[3, 1].Value = "Asp.Net Core";
            workBook.Cells[3, 2].Value = "LMS Onboarding Project";

            workBook.Cells[4, 1].Value = "Asp.Net Core MVC";
            workBook.Cells[4, 2].Value = "Rent A Car Project";

            var bytes = excelPackage.GetAsByteArray();
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ,"ProjectReport.xlsx");
        }

        public List<ContactModel> ContactList()
        {
            List<ContactModel> contactModels = new List<ContactModel>();
            using(var context = new AgricultureContext())
            {
                contactModels = context.Contacts.Select(x => new ContactModel
                {
                    ContactID = x.ContactID,
                    ContactName = x.Name,
                    ContactDate = x.Date,
                    ContactMail = x.Mail,
                    ContactMessage = x.Message
                }).ToList();
            }

            return contactModels;
        }
        public IActionResult ContactReport() 
        {
            using(var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Message List");
                workSheet.Cell(1, 1).Value = "Message Id";
                workSheet.Cell(1, 2).Value = "Message Sender";
                workSheet.Cell(1, 3).Value = "Message Address";
                workSheet.Cell(1, 4).Value = "Message Content";
                workSheet.Cell(1, 5).Value = "Message Date";

                int contactRowCount = 2;
                foreach (var item in ContactList())
                {
                    workSheet.Cell(contactRowCount, 1).Value = item.ContactID;
                    workSheet.Cell(contactRowCount, 2).Value = item.ContactName;
                    workSheet.Cell(contactRowCount, 3).Value = item.ContactMail;
                    workSheet.Cell(contactRowCount, 4).Value = item.ContactMessage;
                    workSheet.Cell(contactRowCount, 5).Value = item.ContactDate;
                    contactRowCount++;
                }

                using(var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MessageReports.xlsx");
                }
            }
        }

        public List<AnnouncementModel> AnnouncementList()
        {
            List<AnnouncementModel> announcementModels = new List<AnnouncementModel>();
            using (var context = new AgricultureContext())
            {
                announcementModels = context.Announcements.Select(x => new AnnouncementModel
                {
                    Id = x.AnnouncementID,
                    Title = x.Title,
                    Description = x.Description,
                    Date = x.Date,
                    Status = x.Status
                }).ToList();
            }

            return announcementModels;
        }

        public IActionResult AnnouncementReport()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Announcement List");
                workSheet.Cell(1, 1).Value = "Announcement Id";
                workSheet.Cell(1, 2).Value = "Announcement Title";
                workSheet.Cell(1, 3).Value = "Announcement Description";
                workSheet.Cell(1, 4).Value = "Announcement Date";
                workSheet.Cell(1, 5).Value = "Announcement Status";

                int announcementRowCount = 2;
                foreach (var item in AnnouncementList())
                {
                    workSheet.Cell(announcementRowCount, 1).Value = item.Id;
                    workSheet.Cell(announcementRowCount, 2).Value = item.Title;
                    workSheet.Cell(announcementRowCount, 3).Value = item.Description;
                    workSheet.Cell(announcementRowCount, 4).Value = item.Date;
                    workSheet.Cell(announcementRowCount, 5).Value = item.Status;
                    announcementRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AnnouncementReports.xlsx");
                }
            }
        }
    }
}