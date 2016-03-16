using System.Web.Mvc;
using AutoMapper;
using KonigLabs.LevisJeans.Models;
using KonigLabs.LevisJeans.Services;

namespace KonigLabs.LevisJeans.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestSrv _testSrv;

        public HomeController(ITestSrv testSrv)
        {
            _testSrv = testSrv;
        }

        public ActionResult Index(CustomerRedirectTermVm model)
        {
            var current = Mapper.Map<CustomerRedirectTermVm, CustomerVm>(model);
            return View(current);
        }

        [HttpPost]
        public ActionResult Index(CustomerVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = _testSrv.SaveCustomer(model);
            return RedirectToAction("Number", id);
        }

        public ActionResult Term(CustomerVm model)
        {
            return View(model);
        }

        public ActionResult Number(NumberVm model)
        {
            return View(model);
        }

        public ActionResult Test(TestVm model)
        {
            if (model.Answer1.HasValue && model.Answer2.HasValue && model.Answer3.HasValue && model.Answer4.HasValue)
            {
                _testSrv.SaveTest(model);
                return Redirect($"/Home/Welldone?customerId={model.CustomerId}");
            }
            else if (model.Answer1.HasValue && model.Answer2.HasValue && model.Answer3.HasValue)
            {
                return View("Test4", model);
            }
            else if (model.Answer1.HasValue && model.Answer2.HasValue)
            {
                return View("Test3", model);
            }
            else if (model.Answer1.HasValue)
            {
                return View("Test2", model);
            }
            else
            {
                return View("Test1", model);
            }
        }

        public ActionResult Welldone(int customerId)
        {
            var model = new ChoosePhraseVm
            {
                CustomerId = customerId,
                Phrases = _testSrv.GetPhrases(customerId)
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult Welldone(ChoosePhraseVm model)
        {
            _testSrv.AddPhrase(model);
            return new JsonResult {Data = new {Success = true}};
        }

        public ActionResult Share(int? customerId)
        {
            return View();
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}