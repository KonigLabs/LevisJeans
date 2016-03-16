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
                return Redirect($"/Home/Share?customerId={model.CustomerId}");
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

        //public ActionResult Welldone(int customerId)
        //{
        //    var model = new ChoosePhraseVm
        //    {
        //        CustomerId = customerId,
        //        Phrases = _testSrv.GetPhrases(customerId)
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public JsonResult Welldone(ChoosePhraseVm model)
        //{
        //    _testSrv.AddPhrase(model);
        //    return new JsonResult {Data = new {Success = true}};
        //}

        public ActionResult Share(int customerId)
        {
            var model = new ShareVm
            {
                CustomerId = customerId,
                Img = _testSrv.GetStrAns(customerId)
            };
            model.Phrase = GetPhrase(model.Img);
            return View(model);
        }

        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult Image(string id)
        {
            var phrase = GetPhrase(id);
            if (phrase == null)
                return null;

            ViewBag.Phrase = phrase;
            ViewBag.Img = id + ".png";
            return View();
        }

        private static string GetPhrase(string img)
        {
            string phrase;
            switch (img)
            {
                case "dddd":
                    phrase = "Сам подворот не подвернётся, и глажу все, что под руку попадётся.";
                    break;
                case "dddn":
                    phrase = "Сам подворот не подвернётся, и заправляю даже кровать.";
                    break;
                case "ddnd":
                    phrase = "Не глажу даже котиков, и ремень пристёгиваю даже в машине.";
                    break;
                case "ddnn":
                    phrase = "Ремней боюсь ещё с детства, и заправляю даже кровать.";
                    break;
                case "dndd":
                    phrase = "Сам подворот не подвернётся, и ремень пристёгиваю даже в машине.";
                    break;
                case "dndn":
                    phrase = "Заправляю салаты, а не рубашки, и глажу все, что под руку попадётся.";
                    break;
                case "dnnd":
                    phrase = "Сам подворот не подвернётся, и заправляю салаты, а не рубашки.";
                    break;
                case "dnnn":
                    phrase = "Заправляю салаты, а не рубашки, и не глажу даже котиков.";
                    break;
                case "nddd":
                    phrase = "Глажу все, что под руку попадётся, и заправляю даже кровать.";
                    break;
                case "nddn":
                    phrase = "Лучше ногу подверну, чем джинсы, и заправляю даже кровать.";
                    break;
                case "ndnd":
                    phrase = "Заправляю даже кровать, и лучше ногу подверну, чем джинсы.";
                    break;
                case "ndnn":
                    phrase = "Не глажу даже котиков, и ремней боюсь еще с детства.";
                    break;
                case "nndd":
                    phrase = "Глажу все, что под руку попадётся, и заправляю салаты, а не рубашки.";
                    break;
                case "nndn":
                    phrase = "Лучше ногу подверну, чем джинсы, и ремней боюсь еще с детства.";
                    break;
                case "nnnd":
                    phrase = "Не глажу даже котиков, и лучше ногу подверну, чем джинсы.";
                    break;
                case "nnnn":
                    phrase = "Лучше ногу подверну, чем джинсы, и не глажу даже котиков.";
                    break;
                default:
                    return null;
            }
            return phrase;
        }
    }
}