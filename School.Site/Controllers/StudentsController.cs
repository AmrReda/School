using System;
using System.Web.Mvc;
using School.Data;
using School.Entities;

namespace School.Site.Controllers
{
    public class StudentsController : Controller
    {
        //
        // GET: /Students/

        public ActionResult Index()
        {
            return View(DataRepository.StudentsProvider.GetAll());
        }

        public ActionResult Create()
        {
            ViewBag.Classes = DataRepository.ClassesProvider.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, string address, string classId, string birthDate, string gender)
        {
            if (ModelState.IsValid)
            {
                var student = new Students
                                  {
                                      Name = name,
                                      Address = address,
                                      ClassId = Convert.ToInt32(classId),
                                      Birthdate = Convert.ToDateTime(birthDate),
                                      Gender = gender
                                  };
                DataRepository.StudentsProvider.Insert(student);
                return RedirectToAction("Index");
            }
            return View(new Students());
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Classes = DataRepository.ClassesProvider.GetAll();
            return View(DataRepository.StudentsProvider.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, string name, string address, string classId, string birthDate, string gender)
        {
            var student = new Students
                            {
                                Id = Convert.ToInt32(id),
                                Name = name,
                                Address = address,
                                ClassId = Convert.ToInt32(classId),
                                Birthdate = Convert.ToDateTime(birthDate),
                                Gender = gender
                            };
            DataRepository.StudentsProvider.Update(student);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(DataRepository.StudentsProvider.GetById(id));
        }

        public ActionResult Delete(int id)
        {
            DataRepository.StudentsProvider.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
