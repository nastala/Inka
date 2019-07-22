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
            bool latterCheck = true;
            bool photoCheck = true;
            bool universityCheck = true;
            bool universityDepartmentCheck = true;
            bool identityNumberLengthCheck = true;
            bool identityNumberNumericCheck = true;

            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
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
                }
                else
                {
                    latterCheck = false;
                    photoCheck = false;
                }
            }

            if (user.EducationLevelID >= 4)
            {
                if (user.UniversityID == null)
                {
                    latterCheck = false;
                    universityCheck = false;
                }

                if (user.UniversityDepartmentID == null)
                {
                    latterCheck = false;
                    universityDepartmentCheck = false;
                }
            }
            else
            {
                user.UniversityID = null;
                user.UniversityDepartmentID = null;
            }

            if (user.IdentityNumber != null)
            {
                if (user.IdentityNumber.Length != 11)
                {
                    identityNumberLengthCheck = false;
                    latterCheck = false;
                }
                else if (!IsNumeric(user.IdentityNumber))
                {
                    identityNumberNumericCheck = false;
                    latterCheck = false;
                }
            }

            if (ModelState.IsValid && latterCheck)
            {
                if (latterCheck)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Basvuru", "Home");
                }
            }

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

            if (!photoCheck)
                ViewBag.PhotoErrorMessage = "Geçersiz fotoğraf seçtiniz";

            if (!universityCheck)
                ViewBag.UniversityErrorMessage = "Üniversite boş geçilemez";

            if (!universityDepartmentCheck)
                ViewBag.UniversityDepartmentErrorMessage = "Bölüm boş geçilemez";

            if (!identityNumberLengthCheck)
                ViewBag.IdentityNumberLengthErrorMessage = "Kimlik numarası 11 hane olmalıdır";
            else if (!identityNumberNumericCheck)
                ViewBag.IdentityNumberNumericErrorMessage = "Kimlik numarası sayılardan oluşmalıdır";

            return View();
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

        [HttpGet]
        public JsonResult Universities()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<University> universities = db.Universities.Where(university => university.NationID == 1).ToList();
            return Json(universities, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UniversityDepartments(int? universityID = null)
        {
            if (universityID == null)
                return null;

            db.Configuration.ProxyCreationEnabled = false;
            List<UniversityDepartment> universityDepartments = db.UniversityDepartments.Where(universityDepartment => universityDepartment.UniversityID == universityID).ToList();
            return Json(universityDepartments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Districts(int? cityID)
        {
            if (cityID == null)
                return null;

            db.Configuration.ProxyCreationEnabled = false;
            List<District> districts = db.Districts.Where(district => district.CityID == cityID).ToList();
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
    }
}