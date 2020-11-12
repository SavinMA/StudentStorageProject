using DomainLayer.Interfaces;
using DomainLayer.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StorageContext db;

        public StudentRepository(StorageContext storageContext)
        {
            db = storageContext;
        }

        public void Create(Student item)
        {
            db.Students.Add(item);
        }

        public void Delete(Guid id)
        {
            Student student = db.Students.Find(id);
            if (student != null)
                db.Students.Remove(student);
        }

        public Student Get(Guid id)
        {
            return db.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students;
        }

        public IEnumerable<Student> GetStudentsWithUniqueId()
        {
            return db.Students.Include(s => s.UniqueID);
        }

        public void Update(Guid id, Student item)
        {
            var student = db.Students.Find(id);
            if (student != null)
            {
                student.Name = item.Name;
                student.Gender = item.Gender;
                student.Surname = item.Surname;
                student.Patronomic = item.Patronomic;
                if (default(Guid) != item.UniqueID.ID)
                    student.UniqueID = item.UniqueID;
                db.Entry(student).State = EntityState.Modified;
            }
        }
    }
}
