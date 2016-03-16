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

        public FilePathResult BackupToXml()
        {
            var path = Server.MapPath($"~/backup_{DateTime.Now.Ticks}.xml");
            _adminSrv.Serialize(path);
            return new FilePathResult(path, "text/xml");
        }
    }
}