using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inka.Models;

namespace Inka.Controllers
{
    public class HomeController : Controller
    {
        private Model db = new Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Basvuru()
        {
            Session["UserForeignLanguages"] = null;

            int[] heights = { 150, 151, 152 };
            int[] weights = { 40, 41, 42 };
            int[] sizes = { 32, 33, 34 };
            int[] shoeSizes = { 34, 35, 36 };
            string[] languageLevels = { "Kötü", "Orta", "İyi", "Çok İyi" };

            ViewBag.NationID = new SelectList(db.Nations, "ID", "Name");
            ViewBag.CityID = new SelectList(db.Cities.Where(city => city.NationID == 1), "ID", "Name");
            ViewBag.DistrictID = new SelectList(db.Districts.Where(district => district.CityID == 1), "ID", "Name");
            ViewBag.LicenceID = new SelectList(db.Licences, "ID", "Name");
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "ID", "Name");
            ViewBag.ForeignLanguagesID = new SelectList(db.ForeignLanguages, "ID", "Name");
            ViewBag.Heights = heights;
            ViewBag.Weights = weights;
            ViewBag.Sizes = sizes;
            ViewBag.ShoeSizes = shoeSizes;
            ViewBag.LanguageLevels = new SelectList(languageLevels);
            ViewBag.ForeignLanguages = db.ForeignLanguages.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Basvuru([Bind(Exclude = "PhotoPath")] User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                //var url = file.ToString();
                var fileName = Path.GetFileName(file.FileName); 
                var ext = Path.GetExtension(file.FileName); 

                if (allowedExtensions.Contains(ext))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string myfileName = name + "_" + DateTime.Now.Ticks + ext;
                    var path = Path.Combine(Server.MapPath("~/Images"), myfileName);

                    user.PhotoPath = path;
                    if (Session["UserForeignLanguages"] != null)
                        user.UserForeignLanguages = Session["UserForeignLanguages"] as List<UserForeignLanguage>;

                    file.SaveAs(path);
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Basvuru", "Home");
        }

        [HttpPost]
        public string AddForeignLanguageToSession(int flID, string reading, string writing, string speaking)
        {
            if (flID < 1)
                return "error";

            if (string.IsNullOrWhiteSpace(reading))
                return "error";

            if (string.IsNullOrWhiteSpace(writing))
                return "error";

            if (string.IsNullOrWhiteSpace(speaking))
                return "error";

            var newUserForeignLanguage = new UserForeignLanguage
            {
                ForeignLanguageID = flID,
                Reading = reading,
                Writing = writing,
                Speaking = speaking
            };

            List<UserForeignLanguage> userForeignLanguages;
            int index = -1;

            if (Session["UserForeignLanguages"] == null)
                userForeignLanguages = new List<UserForeignLanguage>();
            else
            {
                userForeignLanguages = Session["UserForeignLanguages"] as List<UserForeignLanguage>;
                index = userForeignLanguages.FindIndex(ufl => ufl.ForeignLanguageID == flID);
            }

            if (index == -1)
                userForeignLanguages.Add(newUserForeignLanguage);
            else
                userForeignLanguages[index] = newUserForeignLanguage;

            Session["UserForeignLanguages"] = userForeignLanguages;
            return "success";
        }

        [HttpGet]
        public JsonResult ForeignLanguages()
        {
            List<ForeignLanguage> foreignLanguages = db.ForeignLanguages.ToList();

            if (Session["UserForeignLanguages"] != null)
            {
                List<UserForeignLanguage> userForeignLanguages = Session["UserForeignLanguages"] as List<UserForeignLanguage>;
                List<int> uflIDs = userForeignLanguages.Select(ufl => ufl.ForeignLanguageID).ToList();

                foreach (var id in uflIDs)
                {
                    var flAddedLanguage = db.ForeignLanguages.Find(id);

                    if (flAddedLanguage != null)
                        foreignLanguages.Remove(db.ForeignLanguages.Find(id));
                    //if (userForeignLanguages.Select(ufl => ufl.ForeignLanguageID).Contains(language.ID))
                    //    foreignLanguages.Remove(language);
                }
            }

            return Json(foreignLanguages.Select(fl => new { fl.ID, fl.Name }), JsonRequestBehavior.AllowGet);
        }
    }
}