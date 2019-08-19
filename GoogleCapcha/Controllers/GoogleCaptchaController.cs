using GoogleCapcha.Attribute;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net;
using System.Web.Mvc;

namespace GoogleCapcha.Controllers
{
    public class GoogleCaptchaController : Controller
    {
        // GET: GoogleCapcha
        //https://www.youtube.com/watch?v=hcGIA7d5cA0
        public ActionResult CaptchaV2()
        {
            return View();
        }

        //need to write post here
        [HttpPost]
        public ActionResult FormSubmitV2()
        {
            //validate Google recaptcha
            var response = Request["g-recaptcha-response"];
            string secretKey = ConfigurationManager.AppSettings["GoogleRecaptchaSecretKeyV2"];
            var client = new WebClient();

            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";

            //validate model
            if (ModelState.IsValid && status)
            {
            }
            return View("CaptchaV2");
        }

        //https://medium.com/@MoienTajik/google-recaptcha-in-asp-net-mvc-cf88b079dde
        [HttpGet]
        public ActionResult CaptchaV2Helper()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateGoogleCaptcha]
        public ActionResult CaptchaV2Helper(string title)
        {
            // If we are here, Captcha is validated.
            return View();
        }

        //https://www.c-sharpcorner.com/article/developing-google-recaptcha-3-html-helper-for-asp-net-mvc/
        [HttpGet]
        public ActionResult CaptchaV3Helper()
        {
            return View();
        }
    }
}