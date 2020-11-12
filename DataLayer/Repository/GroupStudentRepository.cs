using DomainLayer.Interfaces;
using DomainLayer.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataLayer.Repository
{
    public class GroupStudentRepository : IGroupStudentRepository
    {
        private readonly StorageContext db;

        public GroupStudentRepository(StorageContext context)
        {
            db = context;
        }

        public void AddStudentToGroup(Guid groupId, IEnumerable<Student> students)
        {
            var group = db.Groups.Find(groupId);
            foreach(var student in students)
            {
                group.Students.Add(new StudentGroup() { GroupGuid = group.ID, StudentGuid = student.ID });
            }
        }

        public IEnumerable<Group> GetGroups()
        {
            return db.Groups.Include(d => d.Students).ThenInclude(d => d.Student);
        }

        public IEnumerable<Group> GetGroupsByStudent(Student student)
        {
            var groups = db.Groups.Include(d => d.Students);
            return groups.Where(x => x.Students.Any(x => x.Student == student));
        }

        public IEnumerable<Student> GetStudents()
        {
            return db.Students
                .Include(s => s.UniqueID)
                .Include(d => d.Groups)
                .ThenInclude(d => d.Group);
        }

        public IEnumerable<Student> GetStudentsByGroup(Group group)
        {
            var st = db.Students.Include(d => d.Groups);
            var stt = db.Students.Include(d => d.Groups).ThenInclude(d => d.Group);
            return st;
        }
    }
}
