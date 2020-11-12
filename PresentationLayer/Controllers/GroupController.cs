using DomainLayer.Interfaces;
using DomainLayer.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PresentationLayer.Helpers;
using PresentationLayer.Models;
using PresentationLayer.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class GroupController : Controller
    {
        private readonly IUnitOfWork storage;

        private FilterGroup filter;

        public GroupController(IUnitOfWork unitOfWork)
        {
            storage = unitOfWork;
            filter = new FilterGroup();
        }
        // GET: GroupController
        public ActionResult Index(string groupName)
        {
            var groupsDb = storage.GroupStudentRepository.GetGroups();

            var filterGroups = groupsDb.AsQueryable();

            filter.Apply(groupName);

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
            {
                filterGroups = filterGroups.Where(g => g.Name.Contains(filter.GroupName));
            }

            var groups = filterGroups.Select(g => new GroupModel()
            {
                ID = g.ID.ToString(),
                GroupName = g.Name,
                StudentCount = g.Students.Count
            });

            var viewModel = new GroupViewModel()
            {
                Filter = filter,
                Groups = groups
            };

            return View(viewModel);
        }

        // GET: GroupController/Details/5
        public ActionResult Details(Guid id)
        {
            var group = storage.GroupRepository.Get(id);
            return View(group);
        }

        // GET: GroupController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group, IFormCollection collection)
        {
            try
            {
                storage.GroupRepository.Create(group);
                storage.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var group = storage.GroupRepository.Get(id);
            return View(group);
        }

        // POST: GroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group group, IFormCollection collection)
        {
            try
            {
                if (collection.TryGetGuidFromCollection(nameof(Group.ID), out Guid id))
                {
                    storage.GroupRepository.Update(id, group);
                    storage.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var group = storage.GroupRepository.Get(id);
            return View(group);
        }

        // POST: GroupController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                storage.GroupRepository.Delete(id);
                storage.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        // GET: GroupController/AddStudent/5
        public ActionResult AddStudent(Guid id)
        {
            var group = storage.GroupRepository.Get(id);
            var students = storage.StudentRepository.GetStudentsWithUniqueId();
            ViewBag.ListStudents = new SelectList(students, "ID", "Name");
            ViewBag.Students = students;

            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(Guid id, Guid[] selectedStudents)
        {
            var group = storage.GroupRepository.Get(id);
            var students = storage.StudentRepository.GetAll().Where(x => selectedStudents.Contains(x.ID));
            storage.GroupStudentRepository.AddStudentToGroup(group.ID, students);
            storage.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
