using DomainLayer.Interfaces;
using DomainLayer.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using PresentationLayer.Helpers;
using PresentationLayer.Models;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork storage;

        private readonly int pageSize = 5;
        private readonly FilterStudentViewModel filter;

        public StudentController(IUnitOfWork _unitOfWork)
        {
            storage = _unitOfWork; 
            filter = new FilterStudentViewModel();
        }

        // GET: StudentController
        public ActionResult Index(string fio, string gender, string uid, string groupName, int page = 1)
        {
            var dbstudents = storage.GroupStudentRepository.GetStudents().AsQueryable();

            IQueryable<Student> filterStudents = dbstudents.AsQueryable();

            filter.Apply(gender, fio, uid, groupName);

            if (!string.IsNullOrWhiteSpace(filter.FIO))
            {
                filterStudents = filterStudents
                    .ToDictionary(s => $"{s.Surname} {s.Name} {s.Patronomic}", t => t)
                    .Where(s => s.Key.Contains(filter.FIO))
                    .Select(s => s.Value)
                    .AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(filter.Gender))
            {
                filterStudents = filterStudents.Where(s => s.Gender.Contains(filter.Gender));
            }

            if (!string.IsNullOrWhiteSpace(filter.UID))
            {
                filterStudents = filterStudents.Where(s => s.UniqueID != null && s.UniqueID.UID.Contains(filter.UID));
            }

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
            {
                filterStudents = filterStudents.Where(s => s.Groups.Any(g => g.Group.Name == filter.GroupName));
            }

            var count = filterStudents.Count();
            var items = filterStudents
               .Skip((page - 1) * pageSize)
               .Take(pageSize);

            var students = items.Select(x => new StudentsModel()
            {
                ID = x.ID.ToString(),
                Gender = x.Gender,
                FIO = $"{x.Surname} {x.Name} {x.Patronomic}",
                UID = x.UniqueID == null ? string.Empty : x.UniqueID.UID,
                GroupNames = x.Groups.Select(x => x.Group.Name),
            });

            var studentsViewModel = new StudentsViewModel()
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Students = students,
                Filter = filter,
            };

            return View(studentsViewModel);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(Guid id)
        {
            var student = storage.StudentRepository.Get(id);
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                storage.StudentRepository.Create(student);
                storage.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(storage.StudentRepository.Get(id));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student, IFormCollection collection)
        {
            try
            {
                if (collection.TryGetGuidFromCollection(nameof(Student.ID), out Guid id))
                {
                    var uid = student.UniqueID.UID;
                    if (storage.UidRepository.TryCreate(uid, out UniqueID uniqueID))
                    {
                        student.UniqueID = uniqueID;
                    }

                    storage.StudentRepository.Update(id, student);
                    storage.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var student = storage.StudentRepository.Get(id);
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                storage.StudentRepository.Delete(id);
                storage.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
