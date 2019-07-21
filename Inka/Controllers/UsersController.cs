using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inka.Models;

namespace Inka.Controllers
{
    public class UsersController : Controller
    {
        private Model db = new Model();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.City).Include(u => u.District).Include(u => u.EducationLevel).Include(u => u.Licence).Include(u => u.Nation).Include(u => u.University).Include(u => u.UniversityDepartment);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name");
            ViewBag.DistrictID = new SelectList(db.Districts, "ID", "Name");
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "ID", "Name");
            ViewBag.LicenceID = new SelectList(db.Licences, "ID", "Name");
            ViewBag.NationID = new SelectList(db.Nations, "ID", "Name");
            ViewBag.UniversityID = new SelectList(db.Universities, "ID", "Name");
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartments, "ID", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IdentityNumber,FirstName,LastName,BirthDate,Gender,TelephoneNumber,Email,Height,Weight,Size,ShoeSize,PhotoPath,NationID,CityID,DistrictID,LicenceID,EducationLevelID,UniversityID,UniversityDepartmentID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", user.CityID);
            ViewBag.DistrictID = new SelectList(db.Districts, "ID", "Name", user.DistrictID);
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "ID", "Name", user.EducationLevelID);
            ViewBag.LicenceID = new SelectList(db.Licences, "ID", "Name", user.LicenceID);
            ViewBag.NationID = new SelectList(db.Nations, "ID", "Name", user.NationID);
            ViewBag.UniversityID = new SelectList(db.Universities, "ID", "Name", user.UniversityID);
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartments, "ID", "Name", user.UniversityDepartmentID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", user.CityID);
            ViewBag.DistrictID = new SelectList(db.Districts, "ID", "Name", user.DistrictID);
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "ID", "Name", user.EducationLevelID);
            ViewBag.LicenceID = new SelectList(db.Licences, "ID", "Name", user.LicenceID);
            ViewBag.NationID = new SelectList(db.Nations, "ID", "Name", user.NationID);
            ViewBag.UniversityID = new SelectList(db.Universities, "ID", "Name", user.UniversityID);
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartments, "ID", "Name", user.UniversityDepartmentID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IdentityNumber,FirstName,LastName,BirthDate,Gender,TelephoneNumber,Email,Height,Weight,Size,ShoeSize,PhotoPath,NationID,CityID,DistrictID,LicenceID,EducationLevelID,UniversityID,UniversityDepartmentID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", user.CityID);
            ViewBag.DistrictID = new SelectList(db.Districts, "ID", "Name", user.DistrictID);
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "ID", "Name", user.EducationLevelID);
            ViewBag.LicenceID = new SelectList(db.Licences, "ID", "Name", user.LicenceID);
            ViewBag.NationID = new SelectList(db.Nations, "ID", "Name", user.NationID);
            ViewBag.UniversityID = new SelectList(db.Universities, "ID", "Name", user.UniversityID);
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartments, "ID", "Name", user.UniversityDepartmentID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
