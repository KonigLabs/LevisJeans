using System;
using System.Web.Mvc;
using KonigLabs.LevisJeans.Services;

namespace KonigLabs.LevisJeans.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminSrv _adminSrv;

        public AdminController(IAdminSrv adminSrv)
        {
            _adminSrv = adminSrv;
        }

        public ActionResult Index(bool check = false)
        {
            ViewBag.Check = check;
            var model = _adminSrv.GetCustomers(check);
            return View(model);
        }

        [HttpPost]
        public JsonResult Check(int id, bool check = true)
        {
            var msg = check ? _adminSrv.Check(id) : _adminSrv.UnCheck(id);
            return new JsonResult {Data = new {Success = msg == string.Empty, Message = msg}};
        }

        [HttpPost]
        public JsonResult TotalErase()
        {
            var msg = _adminSrv.TotalErase();
            return new JsonResult { Data = new { Success = msg == string.Empty, Message = msg } };
        }

        public FileResult BackupToCsv()
        {
            var fileName = $"backup_{DateTime.Now.ToString("yyyy.MM.dd_HH.mm")}.csv";
            var path = Server.MapPath($"~/{fileName}");
            _adminSrv.Serialize(path);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, @"text\csv", fileName);
        }
    }
}